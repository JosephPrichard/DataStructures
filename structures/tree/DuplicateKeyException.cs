namespace DataStructures.structures.tree
{
    public class DuplicateKeyException : System.Exception
    {
        public DuplicateKeyException() : base() {}
        public DuplicateKeyException(string message) : base(message) { }
        public DuplicateKeyException(string message, System.Exception inner) : base(message, inner) { }
        
        protected DuplicateKeyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}