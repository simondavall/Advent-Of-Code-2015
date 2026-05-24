using System.Security;

namespace Day04;

internal static partial class Program {
  private const string Title = "\n## Day 4: The Ideal Stocking Stuffer ##";
  private const string AdventOfCode = "https://adventofcode.com/2015/day/4";
  private const long ExpectedPartOne = 0;
  private const long ExpectedPartTwo = 0;

   private static long PartOne(string secretKey) {
    long number = 0;

    while (true)
    {
        number++;
        var key = string.Concat(secretKey, number.ToString());
        
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
        byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);
        var hash = Convert.ToHexString(hashBytes);
        
        if (hash.StartsWith("00000"))
            break;
    }
    
    return number;
  }

  private static long PartTwo(string secretKey) {
      long number = 0;
      
      while (true)
      {
          number++;
          var key = string.Concat(secretKey, number.ToString());
        
          byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
          byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);
          var hash = Convert.ToHexString(hashBytes);
        
          if (hash.StartsWith("000000"))
              break;
      }

      return number;
  }
}
