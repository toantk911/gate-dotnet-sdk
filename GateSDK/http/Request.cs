using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vn.gate.sdk.utils;

namespace vn.gate.sdk.http
{
    public class Request : IRequestInterface
    {
        public static String TAG = "Request";

        /**
         * @var string
         */
        public static String METHOD_DELETE = "DELETE";

        /**
         * @var string
         */
        public static String METHOD_GET = "GET";

        /**
         * @var string
         */
        public static String METHOD_POST = "POST";

        /**
         * @var string
         */
        public static String METHOD_PUT = "PUT";

        public static String PROTOCOL_HTTP = "http";
        public static String PROTOCOL_HTTPS = "https";
        public static int PORT = 8888;

        /**
         * @var Headers
         */
        protected Dictionary<String, String> headers;

        /**
         * @var Client
         */
        protected Client client;
        /**
         * @var string
         */
        protected String method = METHOD_GET;

        /**
         * @var string
         */
        protected String protocol = PROTOCOL_HTTPS;

        /**
         * @var string
         */
        protected String domain;

        /**
         * @var String
         */
        protected String last_level_domain;

        /**
         * @var string
         */
        protected String path;

        /**
         *
         * @var string
         */
        protected int listenerPort = PORT;

        /**
         * @var string
         */
        protected String graphVersion;

        /**
         * @var Parameters
         */
        protected Dictionary<String, Object> queryParams;

        /**
         * @var Parameters
         */
        protected Dictionary<String, Object> bodyParams;

        public Request(Client client) {
            this.client = client;
        }

        /**
         * @return Client
         */
        
        public Client getClient() {
            return this.client;
        }

        
        public Dictionary<String, String> getHeaders() {
            if (this.headers == null) {
                this.headers = new Dictionary<String, String>();
            }
            return this.headers;
        }

        
        public void setHeaders(Dictionary<String, String> headers) {
            this.headers = headers;
        }

        
        public String getProtocol() {
            return this.protocol;
        }

        
        public void setProtocol(String protocol) {
            this.protocol = protocol;
        }

        
        public String getDomain() {
            if (String.IsNullOrEmpty(domain)) {
                if (!String.IsNullOrEmpty(this.last_level_domain)) {
                    return String.Format("{0}.{1}", this.last_level_domain, Client.DEFAULT_GRAPH_BASE_DOMAIN);
                } else if (!String.IsNullOrEmpty(this.getClient().getDefaultGraphBaseDomain())) {
                    return String.Format("{0}.{1}", Client.DEFAULT_LAST_LEVEL_DOMAIN, this.getClient().getDefaultGraphBaseDomain());
                } else {
                    return String.Format("{0}.{1}", Client.DEFAULT_LAST_LEVEL_DOMAIN, Client.DEFAULT_GRAPH_BASE_DOMAIN);
                }
            }
            return this.domain;
        }

        
        public void setDomain(String domain) {
            this.domain = domain;
        }

        
        public void setLastLevelDomain(String last_level_domain) {
            this.last_level_domain = last_level_domain;
            this.domain = String.Format("{0}.{1}", last_level_domain, this.getClient().getDefaultGraphBaseDomain());
        }

        
        public int getPort() {
            return this.listenerPort;
        }

        
        public void setPort(int listenerPort) {
            this.listenerPort = listenerPort;
        }

        
        public String getMethod() {
            return this.method;
        }

        
        public void setMethod(String method) {
            this.method = method;
        }

        
        public String getPath() {
            return this.path;
        }

        
        public void setGraphVersion(String version) {
            this.graphVersion = version;
        }

        
        public String getGraphVersion() {
            return this.graphVersion;
        }

        
        public void setPath(String path) {
            this.path = path;
        }

        
        public Dictionary<String, Object> getQueryParams() {
            if (this.queryParams == null) {
                this.queryParams = new Dictionary<String, Object>();
            }
            return this.queryParams;
        }

        
        public void setQueryParams(Dictionary<String, Object> parameters) {
            this.queryParams = parameters;
        }

        public String getBaseUrl() {
            return String.Format("{0}://{1}{2}/Igate_WS/v{3}{4}", this.getProtocol(), this.getDomain(), (this.getPort() != 80 && this.getPort() != 443) ? ":" + this.getPort().ToString() : "", this.getGraphVersion(), this.getPath());
        }

        
        public Uri getUri() {
            try {

                Dictionary<String, Object> appendQuerys = this.getQueryParams();
                if (!appendQuerys.ContainsKey("sdk_version"))
                {
                    appendQuerys.Add("sdk_version", SDKCoreKit.SDK_VERSION);
                }
                if (!appendQuerys.ContainsKey("api_version"))
                {
                    appendQuerys.Add("api_version", SDKCoreKit.VERSION);
                }
                if (!appendQuerys.ContainsKey("timestamp"))
                {
                    appendQuerys.Add("timestamp", Utility.getTimestamp());
                }
                if (!appendQuerys.ContainsKey("client_ip"))
                {
                    appendQuerys.Add("client_ip", Utility.GetIntranetIp());
                }
                return Utility.appendParametersToBaseUrl(this.getBaseUrl(), this.getQueryParams());
            } catch (Exception ex) {
                //Logger.getLogger(Request.class.getName()).log(Level.SEVERE, null, ex);
            } 
            return null;
        }

        
        public Dictionary<String, Object> getBodyParams() {
            if (this.bodyParams == null) {
                this.bodyParams = new Dictionary<String, Object>();
            }
            return this.bodyParams;
        }

        
        public void setBodyParams(Dictionary<String, Object> parameters) {
            this.bodyParams = parameters;
        }

        
        public IResponseInterface execute() {
            return this.getClient().sendRequest(this);
        }

        
        public IRequestInterface createClone() {
            return this.getClient().getRequestPrototype();
        }
    }
}
