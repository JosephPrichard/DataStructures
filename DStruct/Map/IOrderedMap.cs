using System.Collections.Generic;

namespace DStruct.Map;

public interface IOrderedMap<TKey, TValue> : IMap<TKey, TValue>
{
    IEnumerable<TValue> RangeSearch(TKey lower, TKey upper);

    TValue Min();

    TValue Max();
}