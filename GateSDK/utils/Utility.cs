using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace vn.gate.sdk.utils
{
    public class Utility
    {
        public static String TAG = "Utility";
        private static string intranetIp;

        public static Uri appendParametersToBaseUrl(String baseUrl, Dictionary<String, Object> parameters)
        {

            String query = "";
            foreach (KeyValuePair<String, Object> entry in parameters)
            {
                String key = entry.Key;
                Object value = entry.Value;
                if (isSupportedParameterType(value))
                {
                    //uriBuilder.addParameter(key, value.toString());
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += "&";
                    }
                    query += String.Format("{0}={1}", key, Uri.EscapeDataString(value.ToString()));
                }
                else
                {
                    String jValue = Uri.EscapeDataString(JsonConvert.SerializeObject(value));
                    //uriBuilder.addParameter(key, jValue.toString());
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += "&";
                    }
                    query += String.Format("{0}={1}", key, Uri.EscapeDataString(jValue.ToString()));
                }
            }
            if (!String.IsNullOrEmpty(query))
            {
                baseUrl = baseUrl + "?" + query;
            }
            UriBuilder uriBuilder = new UriBuilder(baseUrl);
            return uriBuilder.Uri;
        }

        public static String md5(String input)
        {
            MD5 md5Hash = MD5.Create();

            Byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            int i;
            for (i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static Boolean isSupportedParameterType(Object instance)
        {
            return instance is String
                    || instance is int
                    || instance is Double
                    || instance is float
                    || instance is long
                    || instance is DateTime
                    || instance is Boolean
                    || instance is Decimal;

        }

        public static String getTimestamp()
        {
            DateTime date1 = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - date1.Ticks);
            return ((long)ts.TotalSeconds).ToString();
        }

        public static Boolean isJSONValid(String jsonInString)
        {
            try
            {
                JsonConvert.DeserializeObject<Object>(jsonInString);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        /// <summary>
        /// get local ip
        /// </summary>
        public static string GetIntranetIp()
        {
            if (intranetIp == null)
            {
                NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in nis)
                {
                    if (OperationalStatus.Up == ni.OperationalStatus && (NetworkInterfaceType.Ethernet == ni.NetworkInterfaceType || NetworkInterfaceType.Wireless80211 == ni.NetworkInterfaceType))
                    {
                        foreach (UnicastIPAddressInformation info in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (AddressFamily.InterNetwork == info.Address.AddressFamily)
                            {
                                intranetIp = info.Address.ToString();
                                break;
                            }
                        }
                        if (intranetIp != null) break;
                    }
                }
            }
            if (intranetIp == null)
            {
                intranetIp = "127.0.0.1";
            }
            return intranetIp;
        }
    }
}

