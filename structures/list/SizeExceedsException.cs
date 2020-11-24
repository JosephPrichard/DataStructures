namespace DataStructures.structures.list
{
    public class SizeExceedsException : System.Exception
    {
        
        public SizeExceedsException() : base() {}
        public SizeExceedsException(string message) : base(message) { }
        public SizeExceedsException(string message, System.Exception inner) : base(message, inner) { }
        
        protected SizeExceedsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}