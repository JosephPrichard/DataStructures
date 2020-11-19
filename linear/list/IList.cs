using System;

namespace DataStructures.linear.list 
{
    public interface IList<E> : ICollection<E>
    {
        E this[int index] { set; get; }
        
        void PushBack(E e);

        E PopBack();

        E PeekBack();

        void Remove(int index);

        void Insert(int index, E e);

        void Sort(Func<E, E, bool> compare);
    }
}