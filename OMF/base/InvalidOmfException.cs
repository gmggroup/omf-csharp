using System;
using System.Runtime.Serialization;

namespace OMF.Base
{
    [Serializable]
    internal class InvalidOmfException : Exception
    {
        public InvalidOmfException():base() 
        {
        }

        public InvalidOmfException(string message) : base(message)
        {
        }

        public InvalidOmfException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidOmfException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}