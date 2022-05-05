using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.exception
{
    [Serializable()]
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidArgumentException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
