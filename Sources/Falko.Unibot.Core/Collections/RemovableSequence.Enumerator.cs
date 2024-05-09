using System.Collections;
using System.Runtime.CompilerServices;

namespace Falko.Unibot.Collections;

public partial class RemovableSequence<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        private readonly Node? _first;

        private Node? _current;

        private T _currentValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(Node? first) => _current = _first = first;

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
            if (_current is null) return false;

            _currentValue = _current!.Value;
            _current = _current!.Next!;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _current = _first;
            _currentValue = default!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}
