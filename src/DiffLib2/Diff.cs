using System.Text.RegularExpressions;

namespace DiffLib2;

public static class Diff
{
    public static SegmentDiffer<T> Segments<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, IEqualityComparer<T>? comparer = null)
        => new SegmentDiffer<T>(left, right, comparer ?? EqualityComparer<T>.Default);
}