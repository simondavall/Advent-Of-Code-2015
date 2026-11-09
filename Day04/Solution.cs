using System.Security.Cryptography;
using System.Text;

namespace Day04;

internal static partial class Program
{
    private const string Title = "\n## Day 4: The Ideal Stocking Stuffer ##";
    private const string AdventOfCode = "https://adventofcode.com/2015/day/4";
    private const long ExpectedPartOne = 282749;
    private const long ExpectedPartTwo = 9962624;

    private static long PartOne(string secretKey)
    {
        return GetSmallestSuffix(secretKey, StartsWith5Zeros);
        bool StartsWith5Zeros(Span<byte> hash) => hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0;
    }
    
    private static long PartTwo(string secretKey)
    {
        return GetSmallestSuffix(secretKey, StartsWith6Zeros);
        bool StartsWith6Zeros(Span<byte> hash) => hash[0] == 0 && hash[1] == 0 && hash[2] == 0;
    }
    
    private static long GetSmallestSuffix(string secretKey, Func<Span<byte>, bool> successCondition)
    {
        const int batchSize = 100_000;

        byte[] prefix = Encoding.ASCII.GetBytes(secretKey);
        int prefixLength = prefix.Length;

        // A positive long requires at most 19 decimal digits.
        int bufferLength = checked(prefixLength + 19);
        int workers = Environment.ProcessorCount;

        for (long start = 1;;)
        {
            long end = Math.Min(start + batchSize - 1, long.MaxValue);
            long best = long.MaxValue;

            Parallel.For(0, workers, worker =>
            {
                // Allocate once per worker, not per hash.
                Span<byte> input = bufferLength <= 256
                    ? stackalloc byte[bufferLength]
                    : new byte[bufferLength];

                Span<byte> hash = stackalloc byte[16];

                prefix.CopyTo(input);

                long count = end - start + 1;
                long from = start + count * worker / workers;
                long to = start + count * (worker + 1) / workers;

                for (long number = from; number < to; number++)
                {
                    // Format directly into the byte buffer.
                    if (!number.TryFormat(input[prefixLength..], out int digits))
                    {
                        throw new InvalidOperationException("Number formatting failed.");
                    }

                    // Hash without allocating an output array.
                    MD5.HashData(input[..(prefixLength + digits)], hash);
                    
                    if (successCondition(hash))
                    {
                        // Each worker owns its range.
                        // Only one result per worker is needed.
                        InterlockedMin(ref best, number);
                        break;
                    }
                }
            });

            if (best != long.MaxValue)
                return best;

            if (end == long.MaxValue)
                throw new InvalidOperationException("No match found.");

            start = end + 1;
        }
    }

    private static void InterlockedMin(ref long target, long value)
    {
        long current;

        do
        {
            current = Volatile.Read(ref target);

            if (value >= current)
                return;
        } while (Interlocked.CompareExchange(ref target, value, current) != current);
    }
}