using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        private readonly T[] _values;

        private readonly int _valuesCount;

        private int _currentIndex;

        private T _currentValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(T[] values, int valuesCount)
        {
            _values = values;
            _valuesCount = valuesCount;
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _currentValue!;
        }

        object IEnumerator.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _currentValue!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (_currentIndex == _valuesCount) return false;

            _currentValue = Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_values), _currentIndex);

            ++_currentIndex;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _currentIndex = 0;
            _currentValue = default!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}
