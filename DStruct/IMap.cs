using System.Collections.Generic;

namespace DStruct
{
    public interface IMap<TKey, TValue>
    {
        int Size { get; }
        
        TValue this[TKey key] { set; get; }

        void Put(TKey key, TValue val);

        TValue Get(TKey key);

        bool Remove(TKey key);

        bool Contains(TKey key);

        void Clear();

        bool IsEmpty();

        IEnumerable<TKey> Keys();

        IEnumerable<TValue> Elements();
    }
}