﻿using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using log4net;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SteemAPI.CS
{
	public class CJson
	{
        private static readonly ILog Log = log4net.LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        #region const
        private const string CRLF = "\r\n";
		private const int HTTP_SERVER_OK = 200;
		#endregion

		#region Enums
		public enum EHTTPMethod
		{
			GET = 1,
			POST = 2,
			PUT = 3
		}
		#endregion

		#region Structures
		public struct SJsonRPC
		{
			public string Version;
			public int Port;
			public string Api;
		}
		#endregion

		#region Variables
		private int m_nRequestID;
		private string m_strHostname;
		private string m_strURL;
		private SJsonRPC m_oJsonRpc;
		#endregion

		#region Constructors
		public CJson(string strHostname, ushort nPort, string strApi, string strVersion = "2.0")
		{

			m_strHostname = strHostname;
			m_oJsonRpc.Port = nPort;
			m_oJsonRpc.Api = strApi;
			m_oJsonRpc.Version = strVersion;
            m_strURL = strHostname;

			// Bypass SSL
			ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}
		#endregion

		#region private methods
		private int getRequestID()
		{
			return m_nRequestID++;
		}

		private string GetHttpRequest(string strData, EHTTPMethod eMethod)
		{
			string strBodyRequest = string.Empty;
			string strResponse = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_strURL);

                switch (eMethod)
                {
                    case EHTTPMethod.POST:
                        oRequest.Method = "POST";
                        oRequest.Accept = "application/json-rpc";
                        oRequest.ContentType = "application/json-rpc; charset=UTF-8";
                        strBodyRequest = strData;
                        break;

                    case EHTTPMethod.GET:
                        oRequest.Method = "GET";
                        oRequest.Headers.Add(string.Format("GET:{0} HTTP/1.1", strData));
                        oRequest.Accept = "*/*";
                        strBodyRequest = strData;
                        break;

                    case EHTTPMethod.PUT:
                        string strBoundary = DateTime.Now.Ticks.ToString().Substring(0, 10);
                        oRequest.Headers.Add(string.Format("POST:{0}{1} HTTP/1.1", m_oJsonRpc.Api, "upload/"));
                        oRequest.Method = "POST";
                        oRequest.Accept = "*/*";
                        oRequest.ContentType = string.Format("multipart/form-data; boundary=%s", strBoundary);
                        strBodyRequest = string.Format("--{0}{1}", strBoundary, CJson.CRLF);
                        strBodyRequest += string.Format("Content-Disposition: form-data; name=\"unknown\"; filename=\"newFile.bin\"{0}", CJson.CRLF);
                        strBodyRequest += string.Format("{0}", CJson.CRLF);
                        strBodyRequest += string.Format("{0}{1}", strData, CJson.CRLF);
                        strBodyRequest += string.Format("--{0}--{1}", strBoundary, CJson.CRLF);
                        break;

                    default:
                        throw new Exception("Method not found : supported : GET/POST/PUT");
                }

                if (eMethod != EHTTPMethod.GET)
                {
                    // Setting DATA
                    StringBuilder oStringBuilder = new StringBuilder().Append(strBodyRequest);
                    byte[] aData = Encoding.UTF8.GetBytes(oStringBuilder.ToString());

                    oRequest.Headers.Add(string.Format("ContentLength: {0}", aData.Length));

                    using (System.IO.Stream oStream = oRequest.GetRequestStream())
                    {
                        oStream.Write(aData, 0, aData.Length);
                    }
                }

                // Execute request
                using (HttpWebResponse oResponseWeb = (HttpWebResponse)(oRequest.GetResponse()))
                {
                    if (oResponseWeb.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("Error request response : {0} - {1}", oResponseWeb.StatusCode, oResponseWeb.StatusDescription));
                    }
                    // Read response
                    using (StreamReader oStream = new StreamReader(oResponseWeb.GetResponseStream()))
                    {
                        strResponse = oStream.ReadToEnd();
                        oStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
			
			return strResponse;
		}
		#endregion

		#region public methods
	    public string SendRequest(string strMethod, ArrayList strParams = null, bool postDictionaryAsString = false)
		{
			int nRetry = 0;
			string strResult = string.Empty;
			Hashtable arrRequest = new Hashtable();
            JObject arrResponse = new JObject();

            arrRequest["jsonrpc"] = m_oJsonRpc.Version;
			arrRequest["id"] = getRequestID();
			arrRequest["method"] = strMethod;
            if (strParams != null)
			{
                if(strParams.Count == 1 && postDictionaryAsString)
                {
                    Type t = strParams[0].GetType();
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        arrRequest["params"] = strParams[0];
                    else
                        arrRequest["params"] = strParams;
                }                    
                else
                {
                    arrRequest["params"] = strParams;
                }
                    
            }

			string strJson = JsonConvert.SerializeObject(arrRequest);
			for(nRetry = 1; nRetry < 2; nRetry++)
			{
				try
				{
					strResult = GetHttpRequest(strJson, EHTTPMethod.POST);
                    arrResponse = (JObject)JsonConvert.DeserializeObject(strResult);
                    var error = arrResponse["error"];
                    if (error != null && error.HasValues)
                        Log.Error(error.ToString());
                    
					break;
				}
				catch(Exception ex)
				{
					// Opps, might be a timeout
					if (nRetry == 2)
					{
						// second timeout - give up and re-throw exception
						throw ex;
					}

				}
				// wait 1 sec to give a seconde chance to a busy server
				System.Threading.Thread.Sleep(1000);
			}
			return strResult;
		}

		#endregion
	}
}


