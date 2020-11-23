namespace DataStructures.structures.heap
{
    public class EmptyHeapException : System.Exception
    {
        public EmptyHeapException() : base() {}
        public EmptyHeapException(string message) : base(message) { }
        public EmptyHeapException(string message, System.Exception inner) : base(message, inner) { }
        
        protected EmptyHeapException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}