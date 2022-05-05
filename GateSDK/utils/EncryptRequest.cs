using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace vn.gate.sdk.utils
{
    public class EncryptRequest
    {
        public static String TAG = "EncryptRequest";
        protected Session session;
        protected Dictionary<String, Object> payload;
        protected String encodeRawPlainText;

        public EncryptRequest(Session session)
        {
            this.session = session;
        }

        public EncryptRequest(Session session, String encodeRawPlainText)
        {
            this.session = session;
            if (String.IsNullOrEmpty(encodeRawPlainText))
            {
                return;
            }
            this.encodeRawPlainText = encodeRawPlainText;
            this.parse();
        }

        public void parse()
        {
            if (String.IsNullOrEmpty(this.encodeRawPlainText))
            {
                return;
            }
            String decodeText = Uri.EscapeDataString(this.encodeRawPlainText);
            String decodeJson = TripleDES.Decrypt(decodeText, this.getSession().getAppSecret());

            Dictionary<String, Object> clas = new Dictionary<String, Object>();
            this.payload = (Dictionary<String, Object>)JsonConvert.DeserializeObject<Dictionary<String, Object>>(decodeJson);
        }

        public String make(Dictionary<String, Object> payload)
        {
            if (!payload.ContainsKey("issued_at"))
            {
                payload.Add("issued_at", Utility.getTimestamp());
            }
            String text = JsonConvert.SerializeObject(payload);
            this.encodeRawPlainText = TripleDES.Encrypt(text, this.getSession().getAppSecret());
            return this.encodeRawPlainText;
        }

        public Session getSession()
        {
            return session;
        }

        public Dictionary<String, Object> getPayload()
        {
            return payload;
        }

        public String getEncodeRawPlainText()
        {
            return encodeRawPlainText;
        }
    }
}
