using DataStructures.linear.list;
using DataStructures.linear.stack;

namespace DataStructures.ctc
{
    public class SetOfStacks
    {
        public int Threshold { get; }
        private readonly Stack<Stack<int>> Stack = new Stack<Stack<int>>();

        public SetOfStacks(int threshold) {
            Threshold = threshold;
            Stack.Push(new Stack<int>());
        }

        public void Push(int val) {
            if(Stack.Peek() == null)
                return;
            if(Stack.Peek().Size >= Threshold)
                Stack.Push(new Stack<int>());
            Stack.Peek().Push(val);
        }
        
        public int? Pop() {
            if(Stack.Peek() == null)
                return null;
            if(Stack.Peek().Size == 0) {
                Stack.Pop();
                Pop();
            }
            return Stack.Peek().Pop();
        }

        public int? Peek() {
            return Stack.Peek()?.Peek();
        }
    }
}