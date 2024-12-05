using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    public ref struct StructEnumerator : IEnumerator<T>
    {
        private readonly ref T _valuesReference;

        private readonly int _valuesCount;

        private int _currentIndex;

        private T _currentValue = default!;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal StructEnumerator(T[] values, int valuesCount)
        {
            _valuesReference = ref MemoryMarshal.GetArrayDataReference(values);
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

            _currentValue = Unsafe.Add(ref _valuesReference, _currentIndex);

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
