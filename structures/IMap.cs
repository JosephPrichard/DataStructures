
using System.Collections.Generic;

namespace DataStructures.structures
{
    public interface IMap<K,V> 
    {
        public int Size { get; }
        
        void Put(K key, V val);

        V Get(K key);

        bool Remove(K key);

        bool Contains(K key);

        void Clear();

        bool IsEmpty();

        IEnumerable<K> Keys();

        IEnumerable<V> Elements();
    }
}