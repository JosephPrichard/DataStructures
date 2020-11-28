using System.Collections.Generic;

namespace DataStructures.structures.tree
{
    public interface ISearchTree<K,V> : IMap<K,V>
    {
        public IEnumerable<V> RangeSearch(K lower, K upper);

        public int Rank(K key);
        
        public V Min();

        public V Max();

        public int LeftHeight();

        public int RightHeight();
    }
}