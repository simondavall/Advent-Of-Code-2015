namespace Day01;

internal static partial class Program
{
    private const string Title = "\n## Day 1: Not Quite Lisp ##";
    private const string AdventOfCode = "https://adventofcode.com/2015/day/1";
    private const long ExpectedPartOne = 74;
    private const long ExpectedPartTwo = 1795;

    private static long PartOne(string data)
    {
        long floor = 0;

        foreach (char c in data)
        {
            floor += Next(c, floor);
        }

        return floor;
    }

    private static long PartTwo(string data)
    {
        long floor = 0;
        int i;

        for (i = 0; i < data.Length; i++)
        {
            floor += Next(data[i], floor);
            if (floor < 0)
                break;
        }

        return i + 1;
    }

    private static long Next(char c, long floor)
    {
        return c == '(' ? 1 : -1;
    }
}