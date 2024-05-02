namespace DiffLib2;

public record struct DiffSegment(int LeftLength, int RightLength, bool IsMatch);

public ref struct DiffSegment<T>
{
    public DiffSegment(ReadOnlySpan<T> left, ReadOnlySpan<T> right, bool isMatch)
    {
        Left = left;
        Right = right;
        IsMatch = isMatch;
    }

    public ReadOnlySpan<T> Left { get; }
    public ReadOnlySpan<T> Right { get; }
    public bool IsMatch { get; }
}