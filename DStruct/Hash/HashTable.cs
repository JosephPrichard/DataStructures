using System.Collections.Generic;
using DStruct.List;

namespace DStruct.Hash
{
    public class HashTable<TKey, TValue>(int predictedCount) : IMap<TKey, TValue>
    {
        private readonly Node<KeyValuePair<TKey, TValue>>[] _table = new Node<KeyValuePair<TKey, TValue>>[predictedCount * 2];

        public HashTable() : this(7)
        {
        }

        public int Size { private set; get; }
        
        public TValue this[TKey key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public void Put(TKey key, TValue val)
        {
            var h = Hash(key);
            var head = _table[h];
            var pair = new KeyValuePair<TKey, TValue>(key, val);
            if (head == null)
            {
                _table[h] = new Node<KeyValuePair<TKey, TValue>>(pair);
            }
            else
            {
                var prev = head;
                while (head != null)
                {
                    if (head.Val.Key.Equals(key))
                    {
                        head.Val = pair;
                    }

                    prev = head;
                    head = head.Next;
                }

                prev.Next = new Node<KeyValuePair<TKey, TValue>>(pair);
            }

            Size++;
        }

        public TValue Get(TKey key)
        {
            var pair = GetPair(key);
            return pair.HasValue ? pair.Value.Value : default;
        }

        public bool Remove(TKey key)
        {
            var h = Hash(key);
            var head = _table[h];
            var prev = head;
            if (head != null)
            {
                if (head.Val.Key.Equals(key))
                {
                    _table[h] = head.Next;
                    Size--;
                    return true;
                }

                head = head.Next;
                while (head != null)
                {
                    if (head.Val.Key.Equals(key))
                    {
                        prev.Next = head.Next;
                        Size--;
                        return true;
                    }

                    prev = head;
                    head = head.Next;
                }
            }

            return false;
        }

        public bool Contains(TKey key)
        {
            return GetPair(key).HasValue;
        }

        public void Clear()
        {
            for (var i = 0; i < _table.Length; i++)
            {
                _table[i] = null;
            }

            Size = 0;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        public IEnumerable<TKey> Keys()
        {
            foreach (var n in _table)
            {
                var head = n;
                while (head != null)
                {
                    yield return head.Val.Key;
                    head = head.Next;
                }
            }
        }

        public IEnumerable<TValue> Elements()
        {
            foreach (var n in _table)
            {
                var head = n;
                while (head != null)
                {
                    yield return head.Val.Value;
                    head = head.Next;
                }
            }
        }

        private KeyValuePair<TKey, TValue>? GetPair(TKey key)
        {
            var head = _table[Hash(key)];
            while (head != null)
            {
                if (head.Val.Key.Equals(key))
                {
                    return head.Val;
                }

                head = head.Next;
            }

            return null;
        }

        private static int Abs(int val)
        {
            if (val >= 0)
            {
                return val;
            }
            else
            {
                return -val;
            }
        }

        private int Hash(TKey key)
        {
            return Abs(key.GetHashCode() % _table.Length);
        }
    }
}