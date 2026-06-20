using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Sandstone_Launcher
{
    static class WebHelper
    {
        static public Dictionary<string, string> GetQueries(string QueryString)
        {
            Dictionary<string, string> QueryList = new Dictionary<string, string>();
            string Query = QueryString;
            if (Query.Length > 0)
            {
                if (Query.StartsWith("?")) Query = QueryString.Substring(1);
                string[] AllKeyValuePairs = Query.Split('&');
                foreach (string KeyValuePairString in AllKeyValuePairs) {
                    string[] KeyValuePair = KeyValuePairString.Split('=');
                    if (KeyValuePair.Length > 1)
                        QueryList.Add(KeyValuePair[0], Uri.UnescapeDataString(KeyValuePair[1]));
                }
            }
            return QueryList;
        }
        static public bool IsAvailable(string HostName)
        {
            try
            {
                using (var ping = new Ping())
                {
                    PingReply reply = ping.Send(HostName, 2000);
                    return reply.Status == IPStatus.Success;
                }
            } catch {}
            return false;
        }
    }
}
