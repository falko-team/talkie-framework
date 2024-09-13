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
        var frozenSequence = new FrozenSequence<int>(new[] { 0 });

        using var enumerator = frozenSequence.GetEnumerator();

        Assert.Multiple(() =>
        {
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo(0));
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

        var frozenSequence = new FrozenSequence<int>(sequence);

        using var enumerator = frozenSequence.GetEnumerator();

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
