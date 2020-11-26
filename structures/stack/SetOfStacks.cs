namespace DataStructures.structures.stack
{
    public class SetOfStacks
    {
        private readonly Stack<Stack<int>> stack = new Stack<Stack<int>>();

        public SetOfStacks(int threshold) {
            Threshold = threshold;
            stack.Push(new Stack<int>());
        }

        public int Threshold { get; }

        public void Push(int val) {
            if(stack.Peek() == null || stack.Peek().Size >= Threshold) {
                stack.Push(new Stack<int>());
            }
            stack.Peek().Push(val);
        }

        public int Pop() {
            if(stack.Peek().Size == 0) {
                stack.Pop();
                Pop();
            }
            return stack.Peek().Pop();
        }

        public int Peek() {
            return stack.Peek().Peek();
        }
    }
}