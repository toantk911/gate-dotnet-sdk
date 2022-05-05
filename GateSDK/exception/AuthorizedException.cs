using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.exception
{
    [Serializable()]
    public class AuthorizedException : Exception
    {
        public AuthorizedException(string message) : base(message) { }
        public AuthorizedException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected AuthorizedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
