using AocHelper;

namespace Day02;

internal static partial class Program
{
    private const string Title = "\n## Day 2: I Was Told There Would Be No Math ##";
    private const string AdventOfCode = "https://adventofcode.com/2019/day/2";
    private const long ExpectedPartOne = 1606483;
    private const long ExpectedPartTwo = 3842356;

    private static long PartOne(string[] data)
    {
        long tally = 0;
        foreach (string line in data)
        {
            var dimension = line.Split('x').ToIntArray();
            var surface = new int[3];
            for (var i = 0; i < dimension.Length; i++)
            {
                surface[i] = dimension[i] * dimension[(i + 1) % 3];
            }

            var subTally = 2 * surface.Sum() + surface.Min();
            //Console.WriteLine(subTally);
            tally += subTally;
        }

        return tally;
    }

    private static long PartTwo(string[] data)
    {
        long tally = 0;
        foreach (string line in data)
        {
            var dimension = line.Split('x').ToIntList().Sorted();

            var surface = 2 * dimension[0] + 2 *  dimension[1];
            var volume = dimension[0] * dimension[1] * dimension[2];
            var subTally = surface + volume;
            //Console.WriteLine(subTally);
            tally += subTally;
        }
        return tally;
    }
}