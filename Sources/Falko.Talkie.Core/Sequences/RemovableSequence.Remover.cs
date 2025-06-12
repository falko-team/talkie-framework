namespace Falko.Talkie.Sequences;

public partial class RemovableSequence<T>
{
    public readonly struct Remover
    {
        private readonly RemovableSequence<T> _sequence;

        private readonly Node _current;

        public readonly T Value;

        public Remover(RemovableSequence<T> sequence, Node current)
        {
            _sequence = sequence;
            _current = current;
            Value = _current.Value;
        }

        public void Remove()
        {
            if (_current.Removed || _sequence._count is 0) return;

            _current.Removed = true;

            --_sequence._count;

            if (_sequence._count is 0)
            {
                _sequence._first = null;
                return;
            }

            if (_sequence._first == _current)
            {
                _sequence._first = _current.Next;
                _sequence._first!.Previous!.Next = null;
                return;
            }

            if (_current.Next is not null)
            {
                _current.Next.Previous = _current.Previous;
                _current.Previous!.Next = _current.Next;
            }
            else
            {
                _current.Previous!.Next = null;
            }
        }
    }
}
