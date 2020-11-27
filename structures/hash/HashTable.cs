
using System.Collections.Generic;
using DataStructures.structures.list;

namespace DataStructures.structures.hash
{
    public class HashTable<K,V> : IMap<K,V>
    {
        private readonly Node<KeyValuePair<K,V>>[] table;

        public HashTable(int predictedCount) {
            table = new Node<KeyValuePair<K,V>>[predictedCount*2];
        }
        
        public int Size { private set; get; }
        
        public void Put(K key, V val) {
            var h = Hash(key);
            var head = table[h];
            var pair = new KeyValuePair<K,V>(key,val);
            if(head == null) {
                table[h] = new Node<KeyValuePair<K, V>>(pair);
                Size++;
            } else {
                var prev = head;
                while(head != null) {
                    if(head.Val.Key.Equals(key)) {
                        head.Val = pair;
                        return;
                    }
                    prev = head;
                    head = head.Next;
                }
                prev.Next = new Node<KeyValuePair<K, V>>(pair);
                Size++;
            }
        }

        public V Get(K key) {
            var pair = GetPair(key);
            return pair.HasValue ? pair.Value.Value : default;
        }

        public bool Remove(K key) {
            var h = Hash(key);
            var head  = table[h];
            var prev = head;
            if(head != null) {
                if(head.Val.Key.Equals(key)) {
                    table[h] = head.Next;
                    return true;
                }
                head = head.Next;
                while(head != null) {
                    if(head.Val.Key.Equals(key)) {
                        prev.Next = head.Next;
                        return true;
                    }
                    prev = head;
                    head = head.Next;
                }
            }
            return false;
        }

        public bool Contains(K key) {
            return GetPair(key).HasValue;
        }

        public void Clear() {
            for(var i = 0; i < table.Length; i++) {
                table[0] = null;
            }
        }

        public bool IsEmpty() {
            return Size == 0;
        }

        public IEnumerable<K> Keys() {
            foreach(var n in table) {
                var head = n;
                while(head != null) {
                    yield return head.Val.Key;
                    head = head.Next;
                }
            }
        }

        public IEnumerable<V> Elements() {
            foreach(var n in table) {
                var head = n;
                while(head != null) {
                    yield return head.Val.Value;
                    head = head.Next;
                }
            }
        }

        private KeyValuePair<K,V>? GetPair(K key) {
            var head  = table[Hash(key)];
            while(head != null) {
                if(head.Val.Key.Equals(key)) {
                    return head.Val;
                }
                head = head.Next;
            }
            return null;
        }

        private static int Abs(int val) {
            if(val >= 0) {
                return val;
            } else {
                return -val;
            }
        }

        public int Hash(K key) {
            return Abs(key.GetHashCode() % table.Length);
        }
        
    }
}