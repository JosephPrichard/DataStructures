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
        
        private static int RandomNumber(int min, int max) {
            var random = new Random(); 
            return random.Next(min, max);
        }

        private static void TestBinaryHeap() {
            int[] arr = {52,116,-2,-5,3,-1};
            var heap = new BinaryHeap<int>(HeapType.Min,arr);
            heap.Push(22);
            heap.Push(3);
            heap.Push(62);
            heap.Push(-701);
            heap.Push(5);
            heap.Push(-21);
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            Console.WriteLine("Push More ");
            for(var i = 0; i < 100; i++) {
                heap.Push(RandomNumber(-1000,1000));
            }
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            Console.WriteLine("PopPush ");
            for(var i = 0; i < 5; i++) {
                Console.WriteLine("PopPush: " + heap.PopPush(RandomNumber(-100,100)));
                Console.WriteLine("Peek: " + heap.Peek());
            }
            Console.WriteLine("Complete.");
        }

        private static void TestArrayHeap() {
            int[] arr = {52,116,-2,-5,3,-1};
            var heap = new ArrayHeap<int>(HeapType.Min,arr);
            heap.Push(22);
            heap.Push(3);
            heap.Push(62);
            heap.Push(-701);
            heap.Push(5);
            heap.Push(-21);
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            Console.WriteLine("Push More ");
            for(var i = 0; i < 100; i++) {
                heap.Push(RandomNumber(-1000,1000));
            }
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: " + heap.Pop());
            }
            Console.WriteLine("PopPush ");
            for(var i = 0; i < 5; i++) {
                Console.WriteLine("PopPush: " + heap.PopPush(RandomNumber(-100,100)));
                Console.WriteLine("Peek: " + heap.Peek());
            }
            Console.WriteLine("Complete.");
        }

        private static void TestSortStack() {
            var stack = new Stack<int>();
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            stack.Push(5);
            stack.Push(10);
            Output(StackUtils.SortStack(stack));
            Console.WriteLine("Complete.");
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
            Console.WriteLine("Complete.");
        }

        private static void TestVec() {
            var vector = new ArrayList<int>(5);
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

            var vector1 = new ArrayList<int>();
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
                vector.PushBack(i);
            }
            Console.WriteLine("Complete.");
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
            
            Console.WriteLine("Complete.");
        }
    }
}