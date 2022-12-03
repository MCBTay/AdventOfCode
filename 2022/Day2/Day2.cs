// https://adventofcode.com/2022/day/2
// --- Day 2: Rock Paper Scissors ---

public class Day2
{
  public static void RockPaperScissors()
  {
    var strategyGuide = ParseInput();

    foreach (var game in strategyGuide) 
    {
      foreach (var move in game)
      {
        Console.Write(move + " ");
      }
      Console.WriteLine();
    }
  }

  private static List<List<string>> ParseInput()
  {
    var games = new List<List<string>>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day2/example_input.txt")) 
    {
      var split = line.Split(' ');
      if (split.Count() < 2) continue;

      games.Add(split.ToList()); 
    }

    return games;
  }
}