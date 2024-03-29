﻿using System;
using System.Runtime.Serialization;

namespace DStruct.tree
{
    public class EmptyTreeException : Exception
    {
        public EmptyTreeException()
        { }

        public EmptyTreeException(string message) : base(message)
        { }

        public EmptyTreeException(string message, Exception inner) : base(message, inner)
        { }

        protected EmptyTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}