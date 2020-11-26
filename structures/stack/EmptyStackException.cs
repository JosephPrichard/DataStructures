using System;
using System.Runtime.Serialization;

namespace DataStructures.structures.stack
{
    public class EmptyStackException : Exception
    {
        public EmptyStackException() { }

        public EmptyStackException(string message) : base(message) { }

        public EmptyStackException(string message, Exception inner) : base(message, inner) { }

        protected EmptyStackException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}