using System.Collections;
using System.Runtime.CompilerServices;

namespace Falko.Talkie.Sequences;

public partial class FrozenSequence<T>
{
    public readonly struct Enumerable : IEnumerable<T>
    {
        private readonly T[] _values;

        private readonly int _valuesCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerable(T[] values, int valuesCount)
        {
            _values = values;
            _valuesCount = valuesCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator() => new Enumerator(_values, _valuesCount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_values, _valuesCount);
    }
}
