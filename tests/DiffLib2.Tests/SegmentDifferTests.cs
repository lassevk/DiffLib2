using NUnit.Framework;

namespace DiffLib2.Tests;

public class SegmentDifferTests
{
    private SegmentDiffer<char> Create(string left, string right) => new SegmentDiffer<char>(left, right, EqualityComparer<char>.Default);

    [Test]
    public void NextSegment_EmptyInputs_ReturnsFalse()
    {
        SegmentDiffer<char> differ = Create("", "");

        bool output = differ.NextSegment(out DiffSegment<char> _);

        Assert.That(output, Is.False);
    }

    [Test]
    public void NextSegment_RightIsEmpty_ReturnsLeftSegment()
    {
        SegmentDiffer<char> differ = Create("abc", "");

        bool output = differ.NextSegment(out DiffSegment<char> segment);

        Assert.That(output, Is.True);
        Assert.That(segment.Left.ToString(), Is.EqualTo("abc"));
        Assert.That(segment.Right.Length, Is.EqualTo(0));
    }

    [Test]
    public void NextSegment_RightIsEmpty_OnlyReturnsOneSegment()
    {
        SegmentDiffer<char> differ = Create("abc", "");

        _ = differ.NextSegment(out DiffSegment<char> _);
        bool output = differ.NextSegment(out DiffSegment<char> _);

        Assert.That(output, Is.False);
    }

    [Test]
    public void NextSegment_LeftIsEmpty_ReturnsRightSegment()
    {
        SegmentDiffer<char> differ = Create("", "abc");

        bool output = differ.NextSegment(out DiffSegment<char> segment);

        Assert.That(output, Is.True);
        Assert.That(segment.Left.Length, Is.EqualTo(0));
        Assert.That(segment.Right.ToString(), Is.EqualTo("abc"));
    }

    [Test]
    public void NextSegment_LeftIsEmpty_OnlyReturnsOneSegment()
    {
        SegmentDiffer<char> differ = Create("", "abc");

        _ = differ.NextSegment(out DiffSegment<char> _);
        bool output = differ.NextSegment(out DiffSegment<char> _);

        Assert.That(output, Is.False);
    }
}