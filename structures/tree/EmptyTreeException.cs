namespace DataStructures.structures.tree
{
    public class EmptyTreeException : System.Exception
    {
        public EmptyTreeException() : base() {}
        public EmptyTreeException(string message) : base(message) { }
        public EmptyTreeException(string message, System.Exception inner) : base(message, inner) { }
        
        protected EmptyTreeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}