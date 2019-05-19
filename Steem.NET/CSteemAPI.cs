using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

namespace SteemAPI.CS
{
    public class CSteemAPI : IDisposable
	{
        private static readonly ILog Log = log4net.LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        #region Enums
        public enum EType
		{
			RPC,
			WS
		};
		#endregion

		#region Variables
		private EType m_eType;
		private CJson m_oJson;
		private CWebsocket m_oSocket;
        #endregion

        #region Constructors
        public CSteemAPI(string strHostname) : this(strHostname, EType.RPC)
        {
            
        }

        public CSteemAPI(string strHostname, EType type) : this(strHostname, EType.RPC, 8090)
        {

        }

        public CSteemAPI(string strHostname, EType type, ushort nPort)
		{
            if (type == EType.RPC)
            {
                m_oJson = new CJson(strHostname, nPort, "/rpc");
            }

            if (type == EType.WS)
            {
                m_oSocket = new CWebsocket(strHostname);
            }

            m_eType = type;
		}
		#endregion

		#region Private methods
		private string SendRequest(string strMethod, ArrayList strParams = null)
		{            
		    if ( m_eType == EType.RPC)
			{
				return m_oJson.SendRequest(strMethod, strParams);
			}
			else
			{
				using (Task<string> t = m_oSocket.SendRequest(strMethod, strParams))
				{
					t.Wait();
						return t.Result;
				}
			}
		}
		#endregion

		#region protected methods
		protected JObject call_api(string strMethod)
		{            
			return (JObject)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod))["result"];
		}

		protected JObject call_api(string strMethod, ArrayList arrParams)
		{ 
			return (JObject)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod, arrParams))["result"];
		}

		protected JArray call_api_array(string strMethod, ArrayList arrParams)
		{
            return (JArray)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod, arrParams))["result"];
		}

		protected JArray call_api_array(string strMethod)
		{ 
			return (JArray)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod))["result"];
		}

		protected JValue call_api_value(string strMethod)
		{
			return (JValue)JsonConvert.DeserializeObject<Dictionary<string, JValue>>(SendRequest(strMethod))["result"];
		}

		protected JValue call_api_value(string strMethod, ArrayList arrParams)
		{
			return (JValue)JsonConvert.DeserializeObject<Dictionary<string, JValue>>(SendRequest(strMethod, arrParams))["result"];
		}

		protected JToken call_api_token(string strMethod, ArrayList arrParams)
		{
			return (JToken)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod, arrParams))["result"];
		}

		protected JToken call_api_token(string strMethod)
		{
			return (JToken)JsonConvert.DeserializeObject<Dictionary<string, JToken>>(SendRequest(strMethod))["result"];
		}

		public void Dispose()
		{
			if( null != m_oSocket)
			{
				((IDisposable)m_oSocket).Dispose();
			}
		}
		#endregion
	}
}
