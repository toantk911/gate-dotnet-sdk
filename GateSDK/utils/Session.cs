using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.utils
{
    public class Session
    {
        public static String TAG = "Session";

        private String _merchantId;
        private String _appSecret;
        private String _appPassword;
        private String _appKeyFile;

        /**
         * construct Session
         * 
         * @param merchantId
         * @param appSecret
         * @param appPassword
         * @param appPathKeyFile 
         */
        public Session(String merchantId, String appSecret, String appPassword, String appPathKeyFile)
        {
            this._merchantId = merchantId;
            this._appSecret = appSecret;
            this._appPassword = appPassword;
            this._appKeyFile = appPathKeyFile;
        }

        public String getMerchantId()
        {
            return _merchantId;
        }

        public String getAppSecret()
        {
            return _appSecret;
        }

        public String getAppPassword()
        {
            return _appPassword;
        }

        public String getAppKeyFile()
        {
            return _appKeyFile;
        }
    }
}
