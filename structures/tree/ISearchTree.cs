using System.Collections.Generic;

namespace DataStructures.structures.tree
{
    public interface ISearchTree<K, V> : IMap<K, V>
    {
        public IEnumerable<V> RangeSearch(K lower, K upper);

        public V Min();

        public V Max();

        public int Rank(K key);

        public int Number(K key);
    }
}