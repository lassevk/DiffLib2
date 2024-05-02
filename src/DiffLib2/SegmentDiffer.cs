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
        if (IsEmpty(out segment))
            return false;

        if (EitherSideIsEmpty(out segment))
            return true;

        if (IsMatch(out segment))
            return true;

        if (TraverseCommonSubstrings(out segment))
            return true;

        throw new InvalidOperationException();
    }

    private bool TraverseCommonSubstrings(out DiffSegment<T> segment) => TraverseForSubstrings(0, _left.Length, 0, _right.Length, out segment);

    private bool TraverseForSubstrings(int leftStart, int leftEnd, int rightStart, int rightEnd, out DiffSegment<T> segment)
    {
        var longestLeftStart = 0;
        var longestRightStart = 0;
        var longestLength = 0;

        for (int leftOffset = leftStart; leftOffset < leftEnd; leftOffset++)
        {
            for (int rightOffset = rightStart; rightOffset < rightEnd; rightOffset++)
            {
                if (!_comparer.Equals(_left[leftOffset], _right[rightOffset]))
                    continue;

                var length = 1;
                int maxPossibleLength = Math.Min(leftEnd - leftStart - leftOffset, rightEnd - rightStart - rightOffset);
                while (length < maxPossibleLength)
                {
                    if (!_comparer.Equals(_left[leftOffset + length], _right[rightOffset + length]))
                        break;

                    length++;
                }

                if (length > longestLength)
                {
                    longestLeftStart = leftOffset;
                    longestRightStart = rightOffset;
                    longestLength = length;
                }
            }
        }

        if (longestLength == 0)
        {
            segment = new DiffSegment<T>(_left[leftStart .. leftEnd], _right[rightStart .. rightEnd], false);
            Advance(leftEnd - leftStart, rightEnd - rightStart);
            return true;
        }

        if (longestLeftStart == 0 && longestRightStart == 0)
        {
            segment = new DiffSegment<T>(_left[..longestLength], _right[..longestLength], true);
            return true;
        }

        if (longestLeftStart == 0)
        {
            segment = new DiffSegment<T>(_left[..0], _right[..longestRightStart], false);
            return true;
        }

        if (longestRightStart == 0)
        {
            segment = new DiffSegment<T>(_left[..longestLeftStart], _right[..0], false);
            return true;
        }

        return TraverseForSubstrings(leftStart, longestLeftStart, rightStart, longestRightStart, out segment);
    }

    private bool IsMatch(out DiffSegment<T> segment)
    {
        int match = Match();
        if (match > 0)
        {
            segment = new DiffSegment<T>(_left[..match], _right[..match], true);
            Advance(match);
            return true;
        }

        segment = default;
        return false;
    }

    private bool EitherSideIsEmpty(out DiffSegment<T> segment)
    {
        if (_left.Length == 0 || _right.Length == 0)
        {
            segment = new DiffSegment<T>(_left, _right, false);
            Advance(_left.Length, _right.Length);
            return true;
        }

        segment = default;
        return false;
    }

    private bool IsEmpty(out DiffSegment<T> segment)
    {
        segment = default;
        return _left.Length == 0 && _right.Length == 0;
    }

    private void Advance(int length) => Advance(length, length);
    private void Advance(int leftLength, int rightLength)
    {
        _left = _left[leftLength..];
        _right = _right[rightLength..];
    }

    private int Match()
    {
        for (var index = 0;; index++)
        {
            if (index == _left.Length || index == _right.Length)
                return index;

            if (!_comparer.Equals(_left[index], _right[index]))
                return index;
        }
    }
}