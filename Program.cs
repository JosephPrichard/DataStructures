using System;
using DataStructures.structures;
using DataStructures.structures.hash;
using DataStructures.structures.list;
using DataStructures.structures.queue;
using DataStructures.structures.stack;
using DataStructures.structures.tree.bst;

namespace DataStructures
{
    public class Program
    {
        private static void Main(string[] args) {
            TestBinaryTree();
        }

        private static void Output<E>(ICollection<E> list) {
            foreach(var item in list.GetEnumerable()) {
                Console.Write(item+", ");
            }
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
                Console.WriteLine("Pop: "+heap.Pop());
            }
            Console.WriteLine("Push More ");
            for(var i = 0; i < 100; i++) {
                heap.Push(RandomNumber(-1000, 1000));
            }
            while(!heap.IsEmpty()) {
                Console.WriteLine("Pop: "+heap.Pop());
            }
            Console.WriteLine("PopPush ");
            heap.Push(1);
            for(var i = 0; i < 5; i++) {
                Console.WriteLine("PopPush: "+heap.PopPush(RandomNumber(-100, 100)));
                Console.WriteLine("Peek: "+heap.Peek());
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

            new Sorter<int>(SortType.Asc).QuickSort(list);
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
            var tree = new BinarySearchTree<int,char>();
            tree.Put(1, 'F');
            tree.Put(2, 'G');
            tree.Put(4, 'I');
            tree.Put(3, 'H');
            tree.Put(-6, 'B');
            tree.Put(-7, 'A');
            tree.Put(-3, 'D');
            tree.Put(-4, 'C');
            tree.Put(-2, 'E');
            foreach(var v in tree.Elements()) {
                Console.Write(v+", ");
            }
            Console.WriteLine();
            tree.Remove(-3);
            tree.Remove(2);
            tree.Put(10, 'J');
            tree.Put(11, 'L');
            tree.Put(11, 'K');
            Console.WriteLine("Remove -6: "+tree.Remove(-6));
            Console.WriteLine("Remove 0: "+tree.Remove(0)); //no key for 0
            Console.WriteLine("Min: "+tree.Min());
            Console.WriteLine("Max: "+tree.Max());
            Console.WriteLine("Val: "+tree.Get(-4));
            Console.WriteLine("LHeight: "+tree.LeftHeight());
            Console.WriteLine("RHeight: "+tree.RightHeight());
            Console.WriteLine("Rank: "+tree.Rank(1));
            foreach(var v in tree.Elements()) {
                Console.Write(v+", ");
            }
            Console.WriteLine();
            foreach(var k in tree.Keys()) {
                Console.Write(k+", ");
            }
            Console.WriteLine();
            foreach(var v in tree.RangeSearch(2,10)) {
                Console.Write(v+", ");
            }
            Console.WriteLine();
        }

        private static void TestHashTable() {
            var table = new HashTable<int,char>(25);
            table.Put(1, 'A');
            table.Put(2, 'B');
            table.Put(3, 'C');
            table.Put(51, 'D');
            table.Put(52, 'E');
            table.Put(53, 'F');
            table.Put(101, 'G');
            table.Put(102, 'H');
            table.Put(103, 'I');
            Console.WriteLine("Size: " + table.Size);
            Console.WriteLine(table.Get(3));
            Console.WriteLine(table.Get(53));
            Console.WriteLine(table.Get(103));
            Console.WriteLine("Has 3?: " + table.Contains(3));
            table.Remove(3);
            Console.WriteLine("Has 3?: " + table.Contains(3));
            Console.WriteLine(table.Get(3));
            Console.WriteLine(table.Get(53));
            Console.WriteLine(table.Get(103));
            Console.WriteLine("Has 53?: " + table.Contains(53));
            Console.WriteLine("Has 103?: " + table.Contains(103));
            table.Remove(53);
            table.Remove(103);
            Console.WriteLine("Has 53?: " + table.Contains(53));
            Console.WriteLine("Has 103?: " + table.Contains(103));
            Console.WriteLine(table.Get(1));
            Console.WriteLine(table.Get(51));
            Console.WriteLine(table.Get(101));
            Console.WriteLine(table.Get(2));
            Console.WriteLine(table.Get(52));
            Console.WriteLine(table.Get(102));
            foreach(var k in table.Keys()) {
                Console.Write(k+", ");
            }
            Console.WriteLine();
            foreach(var e in table.Elements()) {
                Console.Write(e+", ");
            }
            Console.WriteLine();
            Console.WriteLine("Size: " + table.Size);
        }

        private static void TestMergeSort() {
            int[] arr = {8, 9, 10, 5, 7, 90, 2, 11};
            new Sorter<int>(SortType.Asc).MergeSort(arr);
            foreach(var i in arr) {
                Console.Write(i+", ");
            }
            Console.WriteLine();
        }
    }
}