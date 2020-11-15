using System;

namespace DataStructures 
{
    public interface IList<E> : ICollection<E>
    {
        public E this[int index] { set; get; }
        
        public void PushBack(E e);

        public E PopBack();

        public E PeekBack();

        public void Remove(int index);

        public void Insert(int index, E e);

        public void Sort(Func<E, E, bool> compare);
    }
}