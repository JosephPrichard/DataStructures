using System.Threading;

namespace DStruct.Hash;

public class HtShard<TKey, TValue>(ReaderWriterLock rwLock, HashTable<TKey, TValue> hashTable)
{
    public ReaderWriterLock Lock { get; set; } = rwLock;
    public HashTable<TKey, TValue> HashTable { get; set; } = hashTable;
}