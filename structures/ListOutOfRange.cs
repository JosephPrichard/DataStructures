using System;
using System.Runtime.Serialization;

namespace DataStructures.structures
{
    public class ListOutOfRange : Exception
    {
        public ListOutOfRange() { }

        public ListOutOfRange(string message) : base(message) { }

        public ListOutOfRange(string message, Exception inner) : base(message, inner) { }

        protected ListOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}