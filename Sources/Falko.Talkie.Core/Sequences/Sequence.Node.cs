namespace Falko.Talkie.Sequences;

public partial class Sequence<T>
{
    public sealed class Node
    {
        public readonly T Value;

        public Node? Next;

        internal Node(T value) => Value = value;
    }
}
