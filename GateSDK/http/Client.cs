using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using vn.gate.sdk.exception;
using vn.gate.sdk.utils;

namespace vn.gate.sdk.http
{
    public class Client
    {
        public static String TAG = "Client";

        public static String DEFAULT_GRAPH_BASE_DOMAIN = "gate.vn";
        public static String DEFAULT_LAST_LEVEL_DOMAIN = "ops";

        /**
         * @var IRequestInterface
         */
        protected IRequestInterface requestPrototype;

        /**
         * @var IResponseInterface
         */
        protected IResponseInterface responsePrototype;

        /**
         * @var Dictionary<String, String>
         */
        protected Dictionary<String, String> defaultRequestHeaders;

        /**
         * @var AdapterInterface
         */
        protected IAdapterInterface adapter;
        /**
         * @var string
         */
        protected String caBundlePath;

        /**
         * @var string
         */
        protected String defaultGraphBaseDomain = DEFAULT_GRAPH_BASE_DOMAIN;

        /**
         * @return string
         */
        public String getDefaultGraphBaseDomain()
        {
            return this.defaultGraphBaseDomain;
        }

        /**
         * @param string domain
         */
        public void setDefaultGraphBaseDomain(String domain)
        {
            this.defaultGraphBaseDomain = domain;
        }

        /**
         * @return IRequestInterface
         */
        public IRequestInterface getRequestPrototype()
        {
            if (this.requestPrototype == null)
            {
                this.requestPrototype = new Request(this);
            }

            return this.requestPrototype;
        }

        /**
         * @param IRequestInterface prototype
         */
        public void setRequestPrototype(IRequestInterface prototype)
        {
            this.requestPrototype = prototype;
        }

        /**
         * @return IRequestInterface
         */
        public IRequestInterface createRequest()
        {
            return this.getRequestPrototype().createClone();
        }

        /**
         * @return IResponseInterface
         */
        public IResponseInterface getResponsePrototype()
        {
            if (this.responsePrototype == null)
            {
                this.responsePrototype = new Response();
            }

            return this.responsePrototype;
        }

        /**
         * @param IResponseInterface prototype
         */
        public void setResponsePrototype(IResponseInterface prototype)
        {
            this.responsePrototype = prototype;
        }

        /**
         * @return IResponseInterface
         */
        public IResponseInterface createResponse()
        {
            return this.getResponsePrototype();
        }

        /**
         * @return Dictionary<String, String>
         */
        public Dictionary<String, String> getDefaultRequestHeaderds()
        {
            if (this.defaultRequestHeaders == null)
            {
                this.defaultRequestHeaders = new Dictionary<String, String>();
                this.defaultRequestHeaders.Add("User-Agent", "gate-aspnet-" + SDKCoreKit.VERSION);
                this.defaultRequestHeaders.Add("Accept-Encoding", "*");
                this.defaultRequestHeaders.Add("Accept", "application/json");
                this.defaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");
            }

            return this.defaultRequestHeaders;
        }

        /**
         * @param Dictionary<String, String> headers
         */
        public void setDefaultRequestHeaders(Dictionary<String, String> headers)
        {
            this.defaultRequestHeaders = headers;
        }

        /**
         * @return AdapterInterface
         */
        public IAdapterInterface getAdapter()
        {
            if (this.adapter == null)
            {
                this.adapter = new Adapter(this);
            }

            return this.adapter;
        }

        /**
         * @param AdapterInterface adapter
         */
        public void setAdapter(IAdapterInterface adapter)
        {
            this.adapter = adapter;
        }

        /**
         * @return string
         */
        public String getCaBundlePath()
        {
            return this.caBundlePath;
        }

        /**
         * @param string path
         */
        public void setCaBundlePath(String _path)
        {
            //Path pathFile = Paths.get(_path);
            if (File.Exists(_path))
            {
                throw new Exception("Ca bundle cert not found.");
            }
            this.caBundlePath = _path;
        }

        /**
         * @param IRequestInterface request
         * @return IResponseInterface
         * @throws RequestException
         */
        public IResponseInterface sendRequest(IRequestInterface request)
        {

            IResponseInterface response = null;
            response = this.getAdapter().sendRequest(request);
            response.setRequest(request);
            Dictionary<String, Object> response_content = response.getContent();
            //Dictionary<String, Object> response_body = response.getBody();

            if (response_content == null)
            {
                throw new EmptyResponseException(response.getStatusCode().ToString());
            }
            if (response_content.ContainsKey("error"))
            {
                Dictionary<String, Object> error = Utility.ToDictionary(response_content["error"]);
                throw (new RequestException($"{error["code"]}-{error["message"]}").setError(response_content));
            }

            return response;
        }
    }
}
