namespace DataStructures.structures.queue
{
    public class EmptyQueueException : System.Exception
    {
        public EmptyQueueException() : base() {}
        public EmptyQueueException(string message) : base(message) { }
        public EmptyQueueException(string message, System.Exception inner) : base(message, inner) { }
        
        protected EmptyQueueException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}