namespace DStruct.Stack
{
    public class SetOfStacks
    {
        private readonly Stack<Stack<int>> stack = new Stack<Stack<int>>();

        public SetOfStacks(int threshold)
        {
            Threshold = threshold;
            stack.PushFront(new Stack<int>());
        }

        public int Threshold { get; }

        public void Push(int val)
        {
            if (stack.PeekFront() == null || stack.PeekFront().Size >= Threshold)
            {
                stack.PushFront(new Stack<int>());
            }

            stack.PeekFront().PushFront(val);
        }

        public int Pop()
        {
            if (stack.PeekFront().Size == 0)
            {
                stack.PopFront();
                Pop();
            }

            return stack.PeekFront().PopFront();
        }

        public int Peek()
        {
            return stack.PeekFront().PeekFront();
        }
    }
}