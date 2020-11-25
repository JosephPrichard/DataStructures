namespace DataStructures.structures
{
    public class ListOutOfRange : System.Exception
    {
        
        public ListOutOfRange() : base() {}
        public ListOutOfRange(string message) : base(message) { }
        public ListOutOfRange(string message, System.Exception inner) : base(message, inner) { }
        
        protected ListOutOfRange(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}