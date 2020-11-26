using System;

namespace DataStructures.structures.tree
{
    public class BtUtils<V>
    {
        public static bool IsBst(Node<V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;
            var isBst = (!hasLeft || node.Left.Key < node.Key) && (!hasRight || node.Right.Key > node.Key);
            if(hasLeft) {
                isBst = isBst && IsBst(node.Left);
            }
            if(hasRight) {
                isBst = isBst && IsBst(node.Right);
            }
            return isBst;
        }

        public static void Swap(Node<V> parent, Node<V> left, Node<V> right) {
            parent.Left = right;
            parent.Right = left;
        }

        public static void Invert(Node<V> root) {
            Invert(root, root.Left, root.Right);
        }

        private static void Invert(Node<V> parent, Node<V> left, Node<V> right) {
            var hasLeft = left != null;
            var hasRight = right != null;
            if(hasLeft || hasRight) {
                Swap(parent, left, right);
            }
            if(hasLeft) {
                Invert(left, left.Left, left.Right);
            }
            if(hasRight) {
                Invert(right, right.Left, right.Right);
            }
        }

        public static void PrintConsole(Node<V> node) {
            Console.Write(" | ");
            Console.Write("P: "+node.Key);
            if(node.Left != null) {
                Console.Write(", L: "+node.Left.Key);
            }
            if(node.Right != null) {
                Console.Write(", R: "+node.Right.Key);
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