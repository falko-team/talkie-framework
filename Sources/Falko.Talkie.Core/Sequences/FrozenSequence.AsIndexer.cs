using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    public Indexer AsIndexer() => new(_items, _itemsCount);

    public readonly ref struct Indexer
    {
        private readonly ref T _valuesReference;

        private readonly int _valuesCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Indexer(T[] values, int valuesCount)
        {
            _valuesReference = ref MemoryMarshal.GetArrayDataReference(values);
            _valuesCount = valuesCount;
        }

        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= _valuesCount)
                {
                    throw new IndexOutOfRangeException();
                }

                return ref Unsafe.Add(ref _valuesReference, index);
            }
        }
    }
}
