using System;

namespace DataStructures.tree
{
    public interface IHeap<E>
    {
        int GetDepth();

        bool IsEmpty();

        void Push(E e);

        E Peek();

        E Pop();

        E PopPush(E e);

        bool Contains(E e, Func<E, E, bool> compare);
        
        public static IHeap<E> Meld(IHeap<E> heap1, IHeap<E> heap2, Func<E, E, bool> compare) {
            var heap = new BHeap<E>(compare);
            while(!heap1.IsEmpty()) {
                heap.Push(heap1.Pop());
            }
            while(!heap2.IsEmpty()) {
                heap.Push(heap2.Pop());
            }
            return heap;
        }

    }
}