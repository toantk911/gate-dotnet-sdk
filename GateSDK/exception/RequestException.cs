using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.exception
{
    [Serializable()]
    public class RequestException : Exception
    {
        Dictionary<String, Object> error = new Dictionary<string, object>();
        public RequestException(string message) : base(message) { }
        public RequestException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected RequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }

        public RequestException setError(Dictionary<String, Object> error)
        {
            this.error = error;
            return this;
        }
    }
}
