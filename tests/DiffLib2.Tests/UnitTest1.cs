namespace DiffLib2.Tests;

public class DiffTests
{
    [Test]
    public void Segment_TwoEmptyRanges_ReturnsEmptyResult()
    {
        Diff.Segments<byte>(Array.Empty<byte>(), Array.Empty<byte>());
    }
}