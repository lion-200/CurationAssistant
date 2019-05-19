﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CurationAssistant.Helpers
{
    public static class ConfigurationHelper
    {
        public static string HostName
        {
            get
            {
                return ConfigurationManager.AppSettings["HostName"];
            }
        }

        public static int PostsMinDaysUntil
        {
            get
            {
                var val = ConfigurationManager.AppSettings["PostsMinDaysUntil"];
                return Int32.Parse(val);
            }
        }

        public static int CommentsMinDaysUntil
        {
            get
            {
                var val = ConfigurationManager.AppSettings["CommentsMinDaysUntil"];
                return Int32.Parse(val);
            }
        }

        public static int HistoryTransactionLimit
        {
            get
            {
                var val = ConfigurationManager.AppSettings["HistoryTransactionLimit"];
                return Int32.Parse(val);
            }
        }

        public static int CommentTransactionCount
        {
            get
            {
                var val = ConfigurationManager.AppSettings["CommentTransactionCount"];
                return Int32.Parse(val);
            }
        }

        public static int PostTransactionCount
        {
            get
            {
                var val = ConfigurationManager.AppSettings["PostTransactionCount"];
                return Int32.Parse(val);
            }
        }

        public static int VoteTransactionCount
        {
            get
            {
                var val = ConfigurationManager.AppSettings["VoteTransactionCount"];
                return Int32.Parse(val);
            }
        }
    }
}