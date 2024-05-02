using System.Text;

using BenchmarkDotNet.Attributes;

using DiffLib2;

// ReSharper disable RedundantNameQualifier
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class SegmentBenchmarks
{
    [Params(100, 1000)]
    public int N { get; set; }

    private char[] _left;
    private char[] _right;

    [GlobalSetup]
    public void Setup()
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
        var left = new StringBuilder();
        var right = new StringBuilder();
        var rng = new Random(12345);

        for (var index = 0; index < N; index++)
            left.Append(chars[rng.Next(chars.Length)]);

        right.Append(left);

        for (var index = 0; index < N / 20; index++)
        {
            int pos = rng.Next(left.Length);
            left.Insert(pos, "---");
        }

        for (var index = 0; index < N / 20; index++)
        {
            int pos = rng.Next(right.Length);
            right.Insert(pos, "---");
        }

        _left = left.ToString().ToCharArray();
        _right = right.ToString().ToCharArray();
    }

    [Benchmark]
    public void Version1()
    {
        _ = DiffLib.Diff.CalculateSections(_left, _right).ToList();
    }

    [Benchmark]
    public void Version2()
    {
        SegmentDiffer<char> differ = DiffLib2.Diff.Segments<char>(_left, _right);
        while (differ.NextSegment(out DiffSegment<char> _))
        {
        }
    }
}