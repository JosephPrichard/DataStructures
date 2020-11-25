using System;
using DataStructures.structures;
using DataStructures.structures.list;
using DataStructures.structures.queue;
using DataStructures.structures.stack;
using DataStructures.structures.tree;

namespace DataStructures
{
    public class Program 
    {
        private static void Main(string[] args) {
            TestBinaryTree();
        }

        private static void Output<E>(ICollection<E> list) {
            foreach(var item in list.GetEnumerable()) 
                Console.Write(item + ", ");
            Console.WriteLine();
        }
        
        private static int RandomNumber(int min, int max) {
            var random = new Random(); 
            return random.Next(min, max);
        }

        private static void TestPq() {
            var heap = new PriorityQueue<int>(PriorityType.Min);
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
            
            new Sorter<int>(SortType.Asc).MergeSort(list);
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

        public static void TestBinaryTree() {
            var tree = new BinarySearchTree<char>();
            tree.Push(1,'F');
            tree.Push(2,'G');
            tree.Push(4,'I');
            tree.Push(3,'H');
            tree.Push(-6,'B');
            tree.Push(-7,'A');
            tree.Push(-3,'D');
            tree.Push(-4,'C');
            tree.Push(-2,'E');
            tree.PrintConsole();
            tree.Remove(-3);
            tree.PrintConsole();
            tree.Remove(2);
            tree.PrintConsole();
            tree.Push(10,'J');
            tree.Push(11,'K');
            tree.Remove(-6);
            tree.PrintConsole();
            Console.WriteLine("Min: " + tree.Min());
            Console.WriteLine("Max: " + tree.Max());
            Console.WriteLine("Val: " + tree.Retrieve(-4));
            Console.WriteLine("LHeight: " + tree.LeftHeight());
            Console.WriteLine("RHeight: " + tree.RightHeight());
            foreach(var v in tree.PreOrder()) {
                Console.Write(v + ", ");
            }
            Console.WriteLine();
            foreach(var v in tree.PostOrder()) {
                Console.Write(v + ", ");
            }
            Console.WriteLine();
            foreach(var v in tree.InOrder()) {
                Console.Write(v + ", ");
            }
            Console.WriteLine();
            Console.WriteLine(BtUtils<char>.IsBst(tree.Root));
            BtUtils<char>.Invert(tree.Root);
            tree.PrintConsole();
            Console.WriteLine(BtUtils<char>.IsBst(tree.Root));
        }

        private static void TestMergeSort() {
            int[] arr = {8,9,10,5,7,90,2,11};
            new Sorter<int>(SortType.Asc).MergeSort(arr);
            foreach(var i in arr) {
                Console.Write(i+", ");
            }
            Console.WriteLine();
        }
    }
}