using System;
using DataStructures.linear.stack;
using DataStructures.linear.list; 
using DataStructures.linear;
using DataStructures.heap;

namespace DataStructures
{
    public class Program 
    {
        private static void Main(string[] args) {
            TestArrayHeap();
        }

        private static void Output<E>(ICollection<E> list) {
            foreach(var item in list.GetEnumerable()) 
                Console.Write(" Item: "+item);
            Console.WriteLine();
        }

        private static void TestBinaryHeap() {
            var heap = new BinaryHeap<int>((child,parent) => child < parent);
            heap.Push(-10);
            Console.WriteLine("Pop: " + heap.Pop());
            heap.Push(-2);
            Console.WriteLine("Pop: " + heap.Pop());
            heap.Push(15);
            Console.WriteLine("Pop: " + heap.Pop());
            heap.Push(17);
            Console.WriteLine("Pop: " + heap.Pop());
            Console.WriteLine("Pop: " + heap.Pop());
            heap.Push(900);
            heap.Push(15);
            heap.Push(-2);
            heap.Push(12);
            heap.Push(13);
            heap.Push(-1);
            heap.Push(9);
            heap.Push(10);
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            heap.Push(2);
            heap.Push(10);
            Console.WriteLine("PopPush: " + heap.PopPush(4));
            Console.WriteLine("PopPush: " + heap.PopPush(901));
            Console.WriteLine("PopPush: " + heap.PopPush(-3));
            Console.WriteLine("PopPush: " + heap.PopPush(5));
            Console.WriteLine("PopPush: " + heap.PopPush(-5));
            Console.WriteLine("PopPush: " + heap.PopPush(1));
            Console.WriteLine("PopPush: " + heap.PopPush(2));
            Console.WriteLine("Pop: " + heap.Pop());
            Console.WriteLine("Pop: " + heap.Pop());

            var heap2 = new BinaryHeap<int>((child,parent) => child < parent);
            heap2.Push(900);
            heap2.Push(15);
            heap2.Push(-2);
            heap2.Push(12);
            heap2.Push(13);
            heap2.Push(-1);
            while(!heap2.IsEmpty()) {
                Console.WriteLine("Pop2: " + heap2.Pop());
            }

            int[] arr = {52,116,-2,-5,3,-1};
            var heap4 = new BinaryHeap<int>(arr,(child,parent) => child > parent);
            heap4.Push(22);
            heap4.Push(3);
            heap4.Push(62);
            heap4.Push(-701);
            heap4.Push(5);
            heap4.Push(-21);
            heap4.PrintConsole();
            while(!heap4.IsEmpty()) {
                Console.WriteLine("Pop4: " + heap4.Pop());
            }
            for(var i = 0; i < 100000; i++) {
                heap4.Push(i);
            }
            
        }

        private static void TestArrayHeap() {
            var heap = new ArrayHeap<int>((child,parent) => child < parent);
            heap.Push(5);
            Console.WriteLine("Peek: " + heap.Peek());
            heap.Push(4);
            Console.WriteLine("Peek: " + heap.Peek());
            heap.Push(3);
            Console.WriteLine("Peek: " + heap.Peek());
            heap.Push(2);
            Console.WriteLine("Peek: " + heap.Peek());
            heap.Push(6);
            Console.WriteLine("Peek: " + heap.Peek());
            heap.PrintConsole();
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            heap.Push(5);
            heap.Push(4);
            heap.Push(3);
            heap.Push(2);
            heap.Push(6);
            heap.PrintConsole();
            for (var i = 1; i < 10; i++) {
                Console.WriteLine("PopPush: " + heap.PopPush(i));
                heap.PrintConsole();
            }
        }

        private static void TestSortStack() {
            var stack = new Stack<int>();
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            stack.Push(5);
            stack.Push(10);
            Output(StackUtils.SortStack(stack));
        }

        private static void TestStack() {
            var stack = new Stack<int>();
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            
            var stack1 = new Stack<int>();
            stack1.Push(3);
            stack1.Push(2);
            stack1.Push(1);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek());

            stack.AddAll(stack1);
            
            Output(stack);
        }

        private static void TestVec() {
            var vector = new Vector<int>(5);
            vector.Push(1);
            vector.PushBack(2);
            Output(vector);
            vector.Push(5);
            Output(vector);
            vector.Push(6);
            Output(vector);
            vector.Push(7);
            Output(vector);
            vector.Insert(2, 10);
            Output(vector);
            vector.Remove(3);
            Output(vector);
            
            vector.PushBack(2);
            vector.PushBack(4);
            vector.PushBack(5);
            vector.PushBack(3);
            vector.PushBack(11);
            vector.PushBack(6);
            vector.PushBack(4);
            vector.PushBack(51);
            vector.PushBack(5);
            vector.PushBack(-4);
            vector.PushBack(1);
            vector.PushBack(21);
            vector.PushBack(2);

            var vector1 = new Vector<int>();
            vector1.Push(5);
            vector1.PushBack(6);
            vector1.Push(4);
            vector1.AddAll(vector1);
            Output(vector1);

            vector.AddAll(vector1);
            Output(vector);
            
            vector.Sort((left, right) => left > right);
            Output(vector);
            
            for(var i = 0; i < 100000; i++) {
                vector.Push(i);
            }

        }

        public static void TestLl() {
            var list = new LinkedList<int>();
            list.PushBack(5);
            list.PushBack(4);
            list.Push(11);
            list.PushBack(7);
            list.PushBack(1);
            list.Insert(5, 6);

            Output(list);
            Console.WriteLine(list.Size);
            list.Sort((left, right) => left < right);

            Output(list);

            var list2 = new LinkedList<int>();
            list2.PushBack(11);
            list2.PushBack(12);
            list2.Push(19);

            list.AddAll(list2);

            list.Sort((left, right) => left > right);

            Output(list);
        }
    }
}