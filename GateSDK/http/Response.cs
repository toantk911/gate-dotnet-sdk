using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using vn.gate.sdk.utils;

namespace vn.gate.sdk.http
{
    public class Response : IResponseInterface
    {
        public static String TAG = "Response";

        /**
         * @var RequestInterface
         */
        protected IRequestInterface request;

        /**
         * @var int
         */
        protected int statusCode;

        /**
         * @var Dictionary<String, String>
         */
        protected Dictionary<String, String> headers;

        /**
         * @var String
         */
        protected String body;

        /**
         * @var Dictionary<String, Object>
         */
        protected Dictionary<String, Object> content;

        
        public IRequestInterface getRequest()
        {
            return this.request;
        }

        
        public void setRequest(IRequestInterface request)
        {
            this.request = request;
        }

        
        public int getStatusCode()
        {
            return this.statusCode;
        }

        
        public void setStatusCode(int status_code)
        {
            this.statusCode = status_code;
        }

        
        public Dictionary<String, String> getHeaders()
        {
            if (this.headers == null)
            {
                this.headers = new Dictionary<String, String>();
            }
            return this.headers;
        }

        
        public void setHeaders(Dictionary<String, String> headers)
        {
            this.headers = headers;
        }

        
        public String getBody()
        {
            return this.body;
        }

        
        public void setBody(String body)
        {
            if (!String.IsNullOrEmpty(body))
            {
                if (Utility.isJSONValid(body))
                {
                    Object respObj = JsonConvert.DeserializeObject<Dictionary<String, Object>>(body);
                    if (respObj is Dictionary<String, Object>)
                    {
                        this.content = (Dictionary<String, Object>)respObj;
                    }
                }
            }
            this.body = body;
        }

        
        public Dictionary<String, Object> getContent()
        {
            if (this.content == null && !String.IsNullOrEmpty(this.body))
            {
                if (Utility.isJSONValid(this.body))
                {
                    Object respObj = JsonConvert.DeserializeObject<Dictionary<String, Object>>(this.body);
                    if (respObj is Dictionary<String, Object>)
                    {
                        this.content = (Dictionary<String, Object>)respObj;
                    }
                }
            }
            return this.content;
        }
    }
}
