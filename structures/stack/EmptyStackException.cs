namespace DataStructures.structures.stack
{
    public class EmptyStackException : System.Exception
    {
        public EmptyStackException() : base() {}
        public EmptyStackException(string message) : base(message) { }
        public EmptyStackException(string message, System.Exception inner) : base(message, inner) { }
        
        protected EmptyStackException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}