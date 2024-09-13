namespace Talkie.Sequences;

public partial class RemovableSequence<T>
{
    public sealed partial class Node
    {
        public readonly T Value;

        public Node? Next;

        public Node? Previous;

        public bool Removed;

        internal Node(T value) => Value = value;
    }
}
