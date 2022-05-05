using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace vn.gate.sdk.http
{
    public class Adapter : IAdapterInterface
    {
        public static String TAG = "Adapter";

        protected Client client;
        protected Dictionary<String, String> opts;

        public Adapter(Client client)
        {
            this.client = client;
        }

       
        public Client getClient()
        {
            return this.client;
        }

        
        public String getCaBundlePath()
        {
            return this.getClient().getCaBundlePath();
        }

        
        public Dictionary<String, String> getOpts()
        {
            if (this.opts == null)
            {
                //init default 
                this.opts = new Dictionary<String, String>();
            }
            return this.opts;
        }

        
        public void setOpts(Dictionary<String, String> opts)
        {
            this.opts = opts;
        }

        private HttpWebRequest createConnection(Uri url)
        {
            HttpWebRequest connection;
            connection = (HttpWebRequest)WebRequest.Create(url);
            connection.Method = Request.METHOD_GET;

            Dictionary<String, String> headers = this.getClient().getDefaultRequestHeaderds();
            foreach (KeyValuePair<String, String> entry in headers)
            {
                switch (entry.Key)
                {
                    case "User-Agent":
                        connection.UserAgent = entry.Value;
                        break;
                    case "Content-Type":
                        connection.ContentType = entry.Value;
                        break;
                    case "Accept":
                        connection.Accept = entry.Value;
                        break;
                    default:
                        connection.Headers.Add(entry.Key, entry.Value);
                        break;
                }
                
            }
            
            //connection.setChunkedStreamingMode(0);
            return connection;
        }

        /**
         * @param IRequestInterface request
         * @return IResponseInterface
         * @throws Exception
         */
        public IResponseInterface sendRequest(IRequestInterface request)
        {
            IResponseInterface response = this.getClient().createResponse();
            //$this->getCurl()->reset();
            Uri url = request.getUri();
            HttpWebRequest connection = null;
            try
            {
                String method = request.getMethod();
                connection = createConnection(url);
                connection.Method = method;
                connection.Timeout = (60 * 1000);
                connection.ReadWriteTimeout = (60 * 1000);

                Dictionary<String, Object> bodyParams = request.getBodyParams();
                String jsonBatchEncode = JsonConvert.SerializeObject(bodyParams);

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(jsonBatchEncode);

                using (var stream = connection.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    closeQuietly(stream);
                }

                var responseStream = (HttpWebResponse)connection.GetResponse();

                var strResponse = new StreamReader(responseStream.GetResponseStream()).ReadToEnd();

                response.setStatusCode((int)responseStream.StatusCode);
                response.setRequest(request);
                Dictionary<String, String> headers = new Dictionary<string, string>();
                foreach (String headerKey in responseStream.Headers.AllKeys)
                {
                    headers.Add(headerKey, responseStream.Headers[headerKey]);
                }
                response.setHeaders(headers);
                response.setBody(strResponse);

                closeQuietly(responseStream);

            }
            catch (Exception ex)
            {
                //Logger.getLogger(Adapter.class.getName()).log(Level.SEVERE, null, ex);
                System.Console.WriteLine(ex);
            }
            finally
            {
                if (connection != null)
                {
                    disconnectQuietly(connection);
                }
            }
            return response;
        }

        public static void closeQuietly(Object closeable)
        {
            try
            {
                if (closeable != null)
                {
                    if (closeable is Stream)
                    {
                        ((Stream)closeable).Close();
                    }
                    else if (closeable is HttpWebResponse)
                    {
                        ((HttpWebResponse)closeable).Close();
                    }

                }
            }
            catch (IOException ioe)
            {
                // ignore
            }
        }

        public void disconnectQuietly(HttpWebRequest connection)
        {
            if (connection is HttpWebRequest)
            {
                ((HttpWebRequest)connection).Abort();
            }
        }
    }
}
