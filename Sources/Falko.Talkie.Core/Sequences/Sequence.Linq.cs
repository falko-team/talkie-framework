namespace Falko.Talkie.Sequences;

public partial class Sequence<T>
{
    public bool Any()
    {
        return _first is not null;
    }

    public T? FirstOrDefault()
    {
        return _first is null
            ? default
            : _first.Value;
    }

    public T First()
    {
        return _first is null
            ? throw new InvalidOperationException()
            : _first.Value;
    }

    public T? SingleOrDefault()
    {
        return _first is not null && _first == _last
            ? _first.Value
            : default;
    }

    public T Single()
    {
        return _first is not null && _first == _last
            ? _first.Value
            : throw new InvalidOperationException();
    }

    public T Last()
    {
        return _last is null
            ? throw new InvalidOperationException()
            : _last.Value;
    }

    public T? LastOrDefault()
    {
        return _last is null
            ? default
            : _last.Value;
    }

    public bool Contains(T value)
    {
        return Contains(value, EqualityComparer<T>.Default);
    }

    public bool Contains(T value, IEqualityComparer<T> comparer)
    {
        for (var current = _first; current is not null; current = current.Next)
        {
            if (comparer.Equals(current.Value, value)) return true;
        }

        return false;
    }
}
