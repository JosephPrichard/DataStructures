using System;

namespace DataStructures.structures.tree.bt
{
    public class BtUtils<V>
    {
        public static void Swap(Node<V> parent, Node<V> left, Node<V> right) {
            parent.Left = right;
            parent.Right = left;
        }

        public static void Invert(Node<V> parent) {
            var hasLeft = parent.Left != null;
            var hasRight = parent.Right != null;
            var left = parent.Left;
            var right = parent.Right;
            if(hasLeft || hasRight) {
                Swap(parent, left, right);
            }
            if(hasLeft) {
                Invert(left);
            }
            if(hasRight) {
                Invert(right);
            }
        }

        public static void PrintConsole(Node<V> node) {
            Console.Write(" | ");
            Console.Write("P: "+node.Val);
            if(node.Left != null) {
                Console.Write(", L: "+node.Left.Val);
            }
            if(node.Right != null) {
                Console.Write(", R: "+node.Right.Val);
            }
            if(node.Left != null) {
                PrintConsole(node.Left);
            }
            if(node.Right != null) {
                PrintConsole(node.Right);
            }
        }
    }
}