
using System.Collections.Generic;

namespace DataStructures.structures
{
    public interface IVault<V>
    {
        void Put(int key, V v);

        V Get(int key);

        void Remove(int key);

        V Min();

        V Max();

        bool IsEmpty();

        IEnumerable<int> Keys();

        IEnumerable<V> Elements();
    }
}