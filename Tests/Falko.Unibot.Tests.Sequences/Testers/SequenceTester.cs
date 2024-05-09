using Falko.Unibot.Collections;
using NUnit.Framework;

namespace Falko.Unibot.Testers;

public class SequenceTester
{
    [Test]
    public void TestSequenceAdd0()
    {
        var sequence = new Sequence<int>();

        using var enumerator = sequence.GetEnumerator();

        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void TestSequenceAdd1()
    {
        var sequence = new Sequence<int> { 0 };

        using var enumerator = sequence.GetEnumerator();

        Assert.Multiple(() =>
        {
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo(0));
        });

        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void TestSequenceAdd10()
    {
        const int capacity = 10;
        var sequence = new Sequence<int>();

        for (var index = 0; index < capacity; index++)
        {
            sequence.Add(index);
        }

        using var enumerator = sequence.GetEnumerator();

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
