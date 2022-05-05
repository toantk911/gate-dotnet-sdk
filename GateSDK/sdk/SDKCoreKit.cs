using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vn.gate.sdk.http;
using vn.gate.sdk.http.fields;
using vn.gate.sdk.utils;

namespace vn.gate.sdk
{
    public class SDKCoreKit
    {
        public static String TAG = "SDKCoreKit";
        public static String VERSION = "1.0";
        public static String SDK_VERSION = "GateopSdkDotnet-1.0";

        /**
         * @var SDKCoreKit
         */
        protected static SDKCoreKit _intanse;

        /**
         * @var Session
         */
        private Session session;

        /**
         * @var Client
         */
        protected Client httpClient;

        /**
         * @var string
         */
        protected String defaultGraphVersion;

        /**
         * @param Client http_client
         * @param Session session A Gate API session
         */
        public SDKCoreKit(Client http_client, Session session)
        {

            this.httpClient = http_client;
            this.session = session;
        }

        /**
         * @param String merchant_id
         * @param String app_secret
         * @param String access_token
         * @return static
         */
        public static SDKCoreKit init(String merchant_id, String app_secret, String app_password, String app_keyfile)
        {
            Session session = new Session(merchant_id, app_secret, app_password, app_keyfile);
            SDKCoreKit api = new SDKCoreKit(new Client(), session);
            SDKCoreKit.setInstance(api);
            return api;
        }

        /**
         * @return SDKCoreKit|null
         */
        public static SDKCoreKit instance()
        {
            return SDKCoreKit._intanse;
        }

        /**
         * @param SDKCoreKit instance
         */
        public static void setInstance(SDKCoreKit instance)
        {
            SDKCoreKit._intanse = instance;
        }

        /**
         * @param string path
         * @param string method
         * @param array params
         * @return RequestInterface
         */
        public IRequestInterface prepareRequest(String path, String method, Dictionary<String, Object> parameters)
        {

            if (String.IsNullOrEmpty(method))
            {
                method = Request.METHOD_GET;
            }

            IRequestInterface request = this.getHttpClient().createRequest();
            request.setMethod(method);
            request.setGraphVersion(this.getDefaultGraphVersion());
            request.setPath(path);
            Dictionary<String, Object> params_ref = null;
            if (method.Equals(Request.METHOD_GET))
            {
                params_ref = request.getQueryParams();
            }
            else if (method.Equals(Request.METHOD_POST))
            {
                params_ref = request.getBodyParams();
                //            //encrypt data 
                Session session = SDKCoreKit.instance().getSession();
                EncryptRequest encrypted = new EncryptRequest(session);
                String encryptText = encrypted.make(parameters);

                SignedRequest signRequest = new SignedRequest(SDKCoreKit.instance().getSession());
                //            
                Dictionary<String, String> origins = new Dictionary<String, String>();

                origins.Add(RequestFields.MERCHANT_ID, SDKCoreKit.instance().getSession().getMerchantId());
                origins.Add(RequestFields.PAYLOAD_ENCRYPT, encryptText);
                origins.Add(RequestFields.SIGNATURE, signRequest.sign(encryptText));
                //            
                //            //var_dump(encryptText);
                signRequest.make(origins);
                //            
                //            
                params_ref.Add(RequestFields.SIGNED_REQUEST, signRequest.getEncodeRawSignedRequest());
                params_ref.Add(RequestFields.TOKEN_PROOF, signRequest.getToken());

                request.setBodyParams(params_ref);

                return request;
            }
            else
            {
                params_ref = request.getBodyParams();
            }
            request.setBodyParams(params_ref);
            return request;
        }

        /**
         * @param RequestInterface request
         * @return ResponseInterface
         */
        public IResponseInterface executeRequest(IRequestInterface request)
        {
            //this->getLogger()->logRequest('debug', request);
            IResponseInterface response = request.execute();
            //this->getLogger()->logResponse('debug', response);

            return response;
        }

        /**
         * @return string
         */
        public String getDefaultGraphVersion()
        {
            if (this.defaultGraphVersion == null)
            {
                //this.defaultGraphVersion("")
                //            match = array();
                //            if (preg_match("/^\d+\.\d+/", static::VERSION, match)) {
                //                this->defaultGraphVersion = match[0];
                //            }
                this.defaultGraphVersion = VERSION;
            }

            return this.defaultGraphVersion;
        }

        /**
         * @param String version
         */
        public void setDefaultGraphVersion(String version)
        {
            this.defaultGraphVersion = version;
        }

        /**
         * Make graph api calls
         *
         * @param string path Ads API endpoint
         * @param string method Ads API request type
         * @param array params Assoc of request parameters
         * @return IResponseInterface Graph API responses
         */
        public IResponseInterface call(String path, String method, Dictionary<String, Object> parameters)
        {
            if (String.IsNullOrEmpty(method))
            {
                method = Request.METHOD_GET;
            }
            IRequestInterface request = this.prepareRequest(path, method, parameters);

            return this.executeRequest(request);
        }

        /**
         * @return Session
         */
        public Session getSession()
        {
            return this.session;
        }

        public Client getHttpClient()
        {
            return this.httpClient;
        }
    }
}
