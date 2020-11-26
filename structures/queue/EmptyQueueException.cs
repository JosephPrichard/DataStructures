using System;
using System.Runtime.Serialization;

namespace DataStructures.structures.queue
{
    public class EmptyQueueException : Exception
    {
        public EmptyQueueException() { }

        public EmptyQueueException(string message) : base(message) { }

        public EmptyQueueException(string message, Exception inner) : base(message, inner) { }

        protected EmptyQueueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}