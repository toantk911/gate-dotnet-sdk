using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.http
{
    public interface IResponseInterface
    {
         /**
         * @return IRequestInterface
         */
        IRequestInterface getRequest();

        /**
         * @param IRequestInterface request
         */
        void setRequest(IRequestInterface request);

        /**
         * @return Integer
         */
        int getStatusCode();

        /**
         * @param int status_code
         */
        void setStatusCode(int status_code);

        /**
         * @return Dictionary<String, String>
         */
        Dictionary<String, String> getHeaders();

        /**
         * @param Dictionary<String, String> headers
         */
        void setHeaders(Dictionary<String, String> headers);

        /**
         * @return string
         */
        String getBody();

        /**
         * @param String
         */
        void setBody(String body);

        /**
         * @return Dictionary<String, Object>
         */
        Dictionary<String, Object> getContent();
    }
}
