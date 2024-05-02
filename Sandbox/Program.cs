using DiffLib2;

string[] left = File.ReadAllLines("left.txt");
string[] right = File.ReadAllLines("right.txt");

SegmentDiffer<string> segments = Diff.Segments<string>(left, right);
while (segments.NextSegment(out DiffSegment<string> segment))
{
    if (segment.IsMatch)
    {
        foreach (string s in segment.Left)
            Console.WriteLine($"  {s}");
    }
    else
    {
        foreach (string s in segment.Left)
            Console.WriteLine($"- {s}");

        foreach (string s in segment.Right)
            Console.WriteLine($"+ {s}");
    }
}

Console.WriteLine("--- end ---");