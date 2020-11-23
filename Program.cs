using System;
using DataStructures.interview.chapter3;
using DataStructures.structures;
using DataStructures.structures.heap;
using DataStructures.structures.list;
using DataStructures.structures.stack;

namespace DataStructures
{
    public class Program 
    {
        private static void Main(string[] args) {
            TestPq();
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

        private static void TestPq() {
            var heap = new Heap<int>(HeapType.Min);
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
            heap.Push(1);
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
            stack.Push(3);
            stack.Push(10);
            stack.Push(-1);
            Output(SortStack.Sort(stack));
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

        private static void TestAl() {
            var list = new ArrayList<int>(5);
            list.Push(1);
            list.PushBack(2);
            list.Push(5);
            list.Push(6);
            list.Push(7);
            Output(list);
            list.Insert(2, 10);
            list.Insert(4, 11);
            Output(list);
            list.Remove(3);
            Output(list);
            list.Remove(1);
            list.Remove(0);
            Output(list);
            
            list.PushBack(2);
            list.PushBack(4);
            list.PushBack(5);
            list.PushBack(3);
            list.PushBack(11);
            list.PushBack(6);
            list.PushBack(4);
            list.PushBack(51);
            list.PushBack(5);
            list.PushBack(-4);
            list.PushBack(1);
            list.PushBack(21);
            list.PushBack(2);

            var list1 = new ArrayList<int>();
            list1.Push(5);
            list1.PushBack(6);
            list1.Push(4);
            list1.AddAll(list1);
            Output(list1);

            list.AddAll(list1);
            Output(list);
            
            new Sorter<int>(SortType.Asc).HeapSort(list);
            Output(list);
            
            Console.WriteLine("Complete.");
        }

        public static void TestLl() {
            var list = new LinkedList<int>();
            list.PushBack(5);
            list.PushBack(4);
            list.Push(11);
            list.PushBack(7);
            list.PushBack(1);
            Output(list);
            list.Insert(4, 6);

            Output(list);

            var list2 = new LinkedList<int>();
            list2.PushBack(11);
            list2.PushBack(12);
            list2.Push(19);

            list.AddAll(list2);
            
            Output(list);
            
            new Sorter<int>(SortType.Dsc).MergeSort(list);

            Output(list);

            Console.WriteLine("Complete.");
        }

        public static void TestHeapSort() {
            int[] elements = {10, 5, -4, 4, 91, -1, 6, 1, 16, 15, 60};
            new Sorter<int>(SortType.Asc).HeapSort(elements);
            foreach(var e in elements) {
                Console.Write(e + ", ");
            }
            Console.WriteLine();
        }
        
    }
}