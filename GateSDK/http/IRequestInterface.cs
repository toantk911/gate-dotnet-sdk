using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.http
{
    public interface IRequestInterface
    {
        
        /**
         * @return Client
         */
        Client getClient() ;

      /**
       * @return Headers
       */
      Dictionary<String, String> getHeaders();

      /**
       * @param Dictionary<String, String> headers
       */
      void setHeaders(Dictionary<String, String> headers);

      /**
       * @return string
       */
      String getProtocol();

      /**
       * @param String protocol
       */
      void setProtocol(String protocol);

      /**
       * @return string
       */
      String getDomain();

      /**
       * @param String domain
       */
      void setDomain(String domain);

      /**
       * @param String last_level_domain
       */
      void setLastLevelDomain(String last_level_domain);

      /**
       * @return int
       */
      int getPort();
  
      /**
       * 
       * @param Integer listenerPort
       */
      void setPort(int listenerPort);
  
      /**
       * @return string
       */
      String getMethod();

      /**
       * @param string $method
       */
      void setMethod(String method);

      /**
       * @return String
       */
      String getPath();

      /**
       * @param String version
       */
      void setGraphVersion(String version);

      /**
       * @return String
       */
      String getGraphVersion();

      /**
       * @param String path
       */
      void setPath(String path);

      /**
       * @return Dictionary<String, Object>
       */
      Dictionary<String, Object> getQueryParams();

      /**
       * @param Dictionary<String, Object> params
       */
      void setQueryParams(Dictionary<String, Object> parameters);

      /**
       * @return Uri
       */
      Uri getUri();

      /**
       * @return Dictionary<String, Object>
       */
      Dictionary<String, Object> getBodyParams();

      /**
       * @param Dictionary<String, Object> params
       */
      void setBodyParams(Dictionary<String, Object> parameters);
  
      /**
       * @return ResponseInterface
       */
      IResponseInterface execute();

      /**
       * Required for Mocking request/response chaining
       * @return RequestInterface
       */
      IRequestInterface createClone() ;
    }
}