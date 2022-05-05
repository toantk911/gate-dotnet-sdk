using System;
using System.Collections.Generic;
using vn.gate.sdk;
using vn.gate.sdk.exception;
using vn.gate.sdk.http;
using vn.gate.sdk.http.fields;
using vn.gate.sdk.http.values;
using vn.gate.sdk.utils;

namespace Sample
{
    class Program
    {
        private static String merchant_id = "1328";
        private static String secret = "3fbf7b1d2c373bb02422e5165dd32b8f";
        private static String password = "279020dc";
        private static String pathKeyFile = "1328.p12";

        static void Main(string[] args)
        {
            SDKCoreKit.init(merchant_id, secret, password, pathKeyFile);

            SDKCoreKit.instance().getHttpClient().getRequestPrototype().setProtocol(Request.PROTOCOL_HTTPS);
            SDKCoreKit.instance().getHttpClient().getRequestPrototype().setLastLevelDomain("sandbox-ops");
            SDKCoreKit.instance().getHttpClient().getRequestPrototype().setPort(7001);
            try
            {
                PayCard pay = new PayCard();
                Dictionary<String, Object> data = new Dictionary<String, Object>();
                String request_id = Utility.getTimestamp();

                data.Add(ScratchCardFields.PROVIDER, Provider.GATE);
                data.Add(ScratchCardFields.USERNAME, "saunghia");
                data.Add(ScratchCardFields.REQUEST_ID, request_id);
                data.Add(ScratchCardFields.SERIAL, "TD00000000");
                data.Add(ScratchCardFields.PIN, "1234567890");
                data.Add(ScratchCardFields.TELCO_SERVICE_CODE, "10010080001");

                IResponseInterface response = pay.claimScratchCard(data);
                Dictionary<String, Object> content = response.getContent();
                if (content != null && content.ContainsKey("response"))
                {
                    Dictionary<String, Object> objResponse = (Dictionary<String, Object>)content["response"];
                    if (objResponse.ContainsKey("code"))
                    {
                        object code = -1;
                        objResponse.TryGetValue("code", out code);
                        System.Console.WriteLine("Status: {0}", (int)code == 0 ? "Success" : "Failed");
                        System.Console.WriteLine("RequestID: {0}", objResponse.ContainsKey("request_id") ? objResponse["request_id"] : "null");
                        System.Console.WriteLine("ResponseID: {0}", objResponse.ContainsKey("response_id") ? objResponse["response_id"] : "null");
                        System.Console.WriteLine("Description: {0}", objResponse.ContainsKey("description") ? objResponse["description"] : "null");
                        System.Console.WriteLine("Amount: {0}", objResponse.ContainsKey("amount") ? objResponse["amount"] : "0");
                    }
                    else
                    {
                        System.Console.WriteLine("Error");
                    }
                }
                else if (content != null && content.ContainsKey("error"))
                {
                    System.Console.WriteLine("Error");
                }
            }
            catch (RequestException ex)
            {
                //Logger.getLogger(SDKsSample.class.getName()).log(Level.SEVERE, null, ex);
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
