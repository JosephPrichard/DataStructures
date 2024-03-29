using System;
using System.Runtime.Serialization;

namespace DataStructures.structures
{
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException()
        { }

        public OutOfRangeException(string message) : base(message)
        { }

        public OutOfRangeException(string message, Exception inner) : base(message, inner)
        { }

        protected OutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}