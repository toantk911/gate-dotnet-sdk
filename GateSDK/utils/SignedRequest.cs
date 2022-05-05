using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace vn.gate.sdk.utils
{
    public class SignedRequest
    {
        public static String TAG = "SignedRequest";

        protected Session session;
        protected String encodeRawSignedRequest;

        public SignedRequest(Session session)
        {
            this.session = session;
        }

        public String sign(String payload)
        {
            String KEY_PASS = this.getSession().getAppPassword();
            String KEY_PATH = this.getSession().getAppKeyFile();
            
            try
            {
                X509Certificate2 x509Cert = new X509Certificate2(KEY_PATH, KEY_PASS, X509KeyStorageFlags.MachineKeySet);
                RSA rsaCryptoIPT = (RSA)x509Cert.PrivateKey;
                Byte[] data = UTF8Encoding.UTF8.GetBytes(payload);
                return Convert.ToBase64String(rsaCryptoIPT.SignData(data, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public String make(Dictionary<String, String> parameters)
        {
            String rawSignRequest = JsonConvert.SerializeObject(parameters);
            this.encodeRawSignedRequest = Utility.Base64Encode(rawSignRequest);
            return this.encodeRawSignedRequest;
        }

        public String getEncodeRawSignedRequest()
        {
            return this.encodeRawSignedRequest;
        }

        public String getToken()
        {
            return Utility.md5(this.encodeRawSignedRequest);
        }

        public Session getSession()
        {
            return session;
        }
    }
}
