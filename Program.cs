using System;
using System.Numerics;
using DataStructures.linear;

namespace DataStructures
{
    public class Program 
    {
        private static void Main(string[] args) {
            TestLl();
        }

        private static void Output<E>(ICollection<E> list) {
            foreach(var item in list.GetEnumerable()) 
                Console.Write(" Item: "+item);
            Console.WriteLine();
        }

        private static void TestStack() {
            var stack = new Stack<BigInteger>();
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            
            var stack1 = new Stack<BigInteger>();
            stack1.Push(3);
            stack1.Push(2);
            stack1.Push(1);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek());

            stack.AddAll(stack1);
            
            Output(stack);
        }

        private static void TestVec() {
            var vector = new linear.Vector<BigInteger>();
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

            var vector1 = new linear.Vector<BigInteger>();
            vector1.Push(5);
            vector1.PushBack(6);
            vector1.Push(4);
            vector1.AddAll(vector1);
            Output(vector1);

            vector.AddAll(vector1);
            Output(vector);
            
            vector.Sort((left, right) => left > right);
            Output(vector);
        }

        public static void TestLl() {
            var list = new LinkedList<BigInteger>();
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

            var list2 = new LinkedList<BigInteger>();
            list2.PushBack(11);
            list2.PushBack(12);
            list2.Push(19);

            list.AddAll(list2);

            //list.Sort((left, right) => left > right);

            Output(list);
        }
    }
}