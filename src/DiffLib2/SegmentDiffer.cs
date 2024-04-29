namespace DiffLib2;

public ref struct SegmentDiffer<T>
{
    private ReadOnlySpan<T> _left;
    private ReadOnlySpan<T> _right;
    private readonly IEqualityComparer<T> _comparer;

    public SegmentDiffer(ReadOnlySpan<T> left, ReadOnlySpan<T> right, IEqualityComparer<T> comparer)
    {
        _left = left;
        _right = right;
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public bool NextSegment(out DiffSegment<T> segment)
    {
        if (_left.Length == 0 && _right.Length == 0)
        {
            segment = default;
            return false;
        }

        if (_left.Length == 0 || _right.Length == 0)
        {
            segment = new DiffSegment<T>(_left, _right);
            _left = _left.Slice(_left.Length);
            _right = _right.Slice(_right.Length);
            return true;
        }

        segment = default;
        return false;
    }
}