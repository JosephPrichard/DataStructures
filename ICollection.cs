using System;
using System.Collections.Generic;

namespace DataStructures 
{
    public interface ICollection<E> 
    {
        public int Size { get; }
        
        public void Push(E e);

        public E Peek();

        public E Pop();

        public void AddAll(ICollection<E> list);
        
        public IEnumerable<E> GetEnumerable();
        
        public bool IsEmpty();
        
        public void Clear();

        public bool Contains(E e, Func<E, E, bool> equals);
    }
}