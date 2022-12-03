// https://adventofcode.com/2022/day/1
// --- Day 1: Calorie Counting ---

public class Day1 
{
  static void Main()
  {
    var elves = ParseInput();

    Console.WriteLine($"{elves.Count} elves...");

    var highestCalories = 0;

    foreach (var elf in elves) 
    {
      if (elf.Sum() > highestCalories)
      {
        highestCalories = elf.Sum();
      }
    }

    Console.WriteLine($"Elf with most calories has {highestCalories} calories.");
  }

  private static List<List<int>> ParseInput()
  {
    var elves = new List<List<int>>();
    var currentElf = new List<int>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day1/input.txt")) 
    {
      if (string.IsNullOrEmpty(line.Trim())) 
      {
        elves.Add(currentElf);
        currentElf = new List<int>();
        continue;
      }

      if (!Int32.TryParse(line, out var lineInt)) continue;
      currentElf.Add(lineInt);
    }

    elves.Add(currentElf);

    return elves;
  }
}
