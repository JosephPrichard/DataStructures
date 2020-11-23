using System;

namespace DataStructures.structures.list 
{
    public interface IList<E> : ICollection<E>
    {
        E this[int index] { set; get; }
        
        void PushBack(E e);

        E PopBack();

        E PeekBack();

        void Remove(int index);

        void Insert(int index, E e);
        
    }
}