namespace DiffLib2;

public ref struct DiffSegment<T>
{
    public DiffSegment(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
    {
        Left = left;
        Right = right;
    }

    public ReadOnlySpan<T> Left { get; }
    public ReadOnlySpan<T> Right { get; }
}