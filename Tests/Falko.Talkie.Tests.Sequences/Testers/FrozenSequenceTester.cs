using System.Collections.Immutable;
using NUnit.Framework;
using Talkie.Sequences;

namespace Talkie.Tests.Testers;

public class FrozenSequenceTester
{
    [Test]
    public void TestFrozenSequenceFrom0()
    {
        var sequence = FrozenSequence.Empty<int>();

        using var enumerator = sequence.GetEnumerator();

        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void TestFrozenSequenceFrom1()
    {
        var number = 1;

        var frozenSequence = FrozenSequence.Wrap(number);

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

        var numbers = Enumerable.Range(0, capacity).ToImmutableArray();

        var frozenSequence = FrozenSequence.Copy(numbers);

        using var enumerator = frozenSequence.AsEnumerable().GetEnumerator();

        foreach (var number in numbers)
        {
            Assert.Multiple(() =>
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current, Is.EqualTo(number));
            });
        }

        Assert.That(enumerator.MoveNext(), Is.False);
    }
}
