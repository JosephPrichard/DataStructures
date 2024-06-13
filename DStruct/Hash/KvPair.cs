using System.ComponentModel.Design;

namespace DStruct.Hash;

public class KvPair<TKey, TValue>(TKey key, TValue value)
{
    public TKey Key { get; set; } = key;
    public TValue Value { get; set; } = value;
    
    public bool HasKey => Key != null;

    public bool HasValue => Value != null;
}