using System;
using System.Runtime.Serialization;

namespace DataStructures.structures.tree
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException() { }

        public DuplicateKeyException(string message) : base(message) { }

        public DuplicateKeyException(string message, Exception inner) : base(message, inner) { }

        protected DuplicateKeyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}