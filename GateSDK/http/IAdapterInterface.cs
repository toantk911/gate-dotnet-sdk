using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.http
{
    public interface IAdapterInterface
    {
          /**
           * @return Client
           */
          Client getClient();

          /**
           * @return string
           */
          String getCaBundlePath();

          /**
           * @return \Dictionary<String,String>
           */
          Dictionary<String,String> getOpts();

          /**
           * @param Dictionary<String,String> opts
           * @return void
           */
          void setOpts(Dictionary<String,String> opts);

          /**
           * @param RequestInterface $request
           * @return ResponseInterface
           */
          IResponseInterface sendRequest(IRequestInterface request);
    }
}
