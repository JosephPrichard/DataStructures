using DataStructures.structures.stack;

namespace DataStructures.interview.chapter3
{
    public class SortStack
    {
        public static Stack<int> Sort(Stack<int> input) {
            Stack<int> output = new Stack<int>();
            while(!input.IsEmpty()) {
                Insert(output,input.Pop());
            }
            return output;
        }

        public static void Insert(Stack<int> stack, int element) {
            if(stack.IsEmpty() || stack.Peek() > element) {
                stack.Push(element);
            }
            else {
                var popped = stack.Pop();
                Insert(stack,element);
                stack.Push(popped);
            }
        }
    }
}