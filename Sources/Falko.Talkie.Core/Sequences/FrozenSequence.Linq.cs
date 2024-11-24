using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    public bool Any()
    {
        return _itemsCount > 0;
    }

    public T? FirstOrDefault()
    {
        return _itemsCount is 0
            ? default
            : Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0);
    }

    public T First()
    {
        return _itemsCount is 0
            ? throw new InvalidOperationException()
            : Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0);
    }

    public T? SingleOrDefault()
    {
        return _itemsCount is 1
            ? Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0)
            : default;
    }

    public T Single()
    {
        return _itemsCount is 1
            ? Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0)
            : throw new InvalidOperationException();
    }

    public T Last()
    {
        return _itemsCount is 0
            ? throw new InvalidOperationException()
            : Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), _itemsCount - 1);
    }

    public T? LastOrDefault()
    {
        return _itemsCount is 0
            ? default
            : Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), _itemsCount - 1);
    }

    public bool Contains(T value)
    {
        return Contains(value, EqualityComparer<T>.Default);
    }

    public bool Contains(T value, IEqualityComparer<T> comparer)
    {
        foreach (var item in this)
        {
            if (comparer.Equals(item, value)) return true;
        }

        return false;
    }
}
