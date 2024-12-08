using NUnit.Framework;
using Talkie.Sequences;

namespace Talkie.Tests.Testers;

public class FrozenSequenceTester
{
    [Test]
    public void TestFrozenSequenceFrom0()
    {
        var sequence = FrozenSequence<int>.Empty;

        using var enumerator = sequence.GetEnumerator();

        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void TestFrozenSequenceFrom1()
    {
        var number = 1;

        var frozenSequence = new FrozenSequence<int>(number);

        using var enumerator = frozenSequence.AsEnumerable().GetEnumerator();

        Assert.Multiple(() =>
        {
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo(number));
        });

        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void TestFrozenSequenceFrom10()
    {
        const int capacity = 10;

        var sequence = new Sequence<int>();

        for (var index = 0; index < capacity; index++)
        {
            sequence.Add(index);
        }

        var frozenSequence = FrozenSequence<int>.From(sequence);

        using var enumerator = frozenSequence.AsEnumerable().GetEnumerator();

        for (var index = 0; index < capacity; index++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current, Is.EqualTo(index));
            });
        }

        Assert.That(enumerator.MoveNext(), Is.False);
    }
}
