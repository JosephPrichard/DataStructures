namespace DStruct.stack
{
    public class SortStack
    {
        public static Stack<int> Sort(Stack<int> input)
        {
            var output = new Stack<int>();
            while (!input.IsEmpty())
            {
                Insert(output, input.PopFront());
            }

            return output;
        }

        public static void Insert(Stack<int> stack, int element)
        {
            if (stack.IsEmpty() || stack.PeekFront() > element)
            {
                stack.PushFront(element);
            }
            else
            {
                var popped = stack.PopFront();
                Insert(stack, element);
                stack.PushFront(popped);
            }
        }
    }
}