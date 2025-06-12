using Falko.Talkie.Sequences;
using NUnit.Framework;

namespace Falko.Talkie.Tests.Testers;

public class RemovableSequenceTester
{
    [Test]
    public void TestRemovableSequenceAdd0()
    {
        var removableSequence = new RemovableSequence<int>();

        using var enumerator = removableSequence.GetEnumerator();

        Assert.That(enumerator.MoveNext(), Is.False);

        Assert.That(removableSequence, Is.Empty);
    }

    [Test]
    public void TestRemovableSequenceAdd1AndRemove()
    {
        var removableSequence = new RemovableSequence<int>();

        var first = removableSequence.Add(0);

        using var enumerator1 = removableSequence.GetEnumerator();

        Assert.Multiple(() =>
        {
            Assert.That(enumerator1.MoveNext(), Is.True);
            Assert.That(enumerator1.Current, Is.EqualTo(0));
        });

        Assert.That(enumerator1.MoveNext(), Is.False);

        Assert.That(removableSequence, Has.Count.EqualTo(1));

        first.Remove();
        first.Remove();

        using var enumerator2 = removableSequence.GetEnumerator();

        Assert.That(enumerator2.MoveNext(), Is.False);

        Assert.That(removableSequence, Is.Empty);
    }

    [Test]
    public void TestRemovableSequenceAdd10AndRemove()
    {
        const int capacity = 10;
        var removableSequence = new RemovableSequence<int>();
        var removers = new List<RemovableSequence<int>.Remover>();

        for (var index = 0; index < capacity; index++)
        {
            removers.Add(removableSequence.Add(index));
        }

        using var enumerator = removableSequence.GetEnumerator();

        for (var index = 0; index < capacity; index++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current, Is.EqualTo(index));
            });
        }

        Assert.That(removableSequence, Has.Count.EqualTo(capacity));

        Assert.That(enumerator.MoveNext(), Is.False);

        for (var i = 0; i < capacity; i++)
        {
            var removerIndex = Random.Shared.Next(0, removers.Count);
            var currentRemover = removers[removerIndex];
            removers.RemoveAt(removerIndex);

            currentRemover.Remove();
            currentRemover.Remove();

            Assert.That(removableSequence.Contains(currentRemover.Value), Is.False);
            Assert.That(removableSequence, Has.Count.EqualTo(capacity - i - 1));
        }
    }
}
