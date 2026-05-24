namespace Day03;

internal static partial class Program {
  private const string Title = "\n## Day 3: Perfectly Spherical Houses in a Vacuum ##";
  private const string AdventOfCode = "https://adventofcode.com/2015/day/3";
  private const long ExpectedPartOne = 0;
  private const long ExpectedPartTwo = 0;


  private static readonly Dictionary<char, (int, int)> Directions = new()
  {
    {'^', (0, -1)},
    {'v', (0, 1)},
    {'<', (-1, 0)},
    {'>', (1, 0)},
  };
  
  private static long PartOne(string data) {
    (int x, int y) santa = (0, 0);
    var visited = new HashSet<(int, int)> { santa };

    foreach (char c in data)
    {
      var (dx, dy) = Directions[c];
      santa.x += dx;
      santa.y += dy;
      visited.Add(santa);
    }
    
    return visited.Count;
  }

  private static long PartTwo(string data) {
    (int x, int y) santa = (0, 0);
    (int x, int y) roboSanta = (0, 0);
    var visited = new HashSet<(int, int)> { santa };

    foreach ((int idx, char c) in data.Index())
    {
      var (dx, dy) = Directions[c];
      if (idx % 2 == 1)
      {
        santa.x += dx;
        santa.y += dy;
        visited.Add(santa);
      }
      else
      {
        roboSanta.x += dx;
        roboSanta.y += dy;
        visited.Add(roboSanta);
      }
    }
    
    return visited.Count;
  }
}
