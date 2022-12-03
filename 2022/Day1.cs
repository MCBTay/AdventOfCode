// https://adventofcode.com/2022/day/1
// --- Day 1: Calorie Counting ---

public class Day1 
{
  private static string exampleInput = 
@"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

  static void Main()
  {
    var elves = ParseInput(exampleInput);
  }

  private static List<List<int>> ParseInput(string input)
  {
    var lines = exampleInput.Split(Environment.NewLine);

    var elves = new List<List<int>>();
    var currentElf = new List<int>();
    
    foreach (var line in lines) 
    {
      if (string.IsNullOrEmpty(line.Trim())) 
      {
        elves.Add(currentElf);
        currentElf = new List<int>();
        continue;
      }

      if (!Int32.TryParse(line, out var lineInt)) continue;
      currentElf.Add(lineInt);

      if (line == lines.Last()) elves.Add(currentElf);
    }

    return elves;
  }
}
