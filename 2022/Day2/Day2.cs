// https://adventofcode.com/2022/day/2
// --- Day 2: Rock Paper Scissors ---

public class Day2
{
  public static void RockPaperScissors()
  {
    var strategyGuide = ParseInput();

    var totalScore = 0;
    foreach (var game in strategyGuide) 
    {
      totalScore += DetermineOutcome(game) + MapSelected(game);
    }
    Console.WriteLine($"Total Score: {totalScore}");

    totalScore = 0;
    foreach (var game in strategyGuide)
    {
      totalScore += DetermineOutcomeUpdated(game) + MapSelectedUpdated(game);
    }
    Console.WriteLine($"Total Updated Score: {totalScore}");
  }

  private static int MapSelectedUpdated(List<string> game)
  {
    switch(TranslateToNeededMove(game))
    {
      case "X": return 1;
      case "Y": return 2;
      case "Z": return 3;
      default:  return 0;
    }
  }

  private static int MapSelected(List<string> game)
  {
    switch(game.Last())
    {
      case "X": return 1;
      case "Y": return 2;
      case "Z": return 3;
      default:  return 0;
    }
  }

  private static int DetermineOutcomeUpdated(List<string> game)
  {
    switch (game.First()) 
    {
      case "A": return OpponentThrewRock(TranslateToNeededMove(game));
      case "B": return OpponentThrewPaper(TranslateToNeededMove(game));
      case "C": return OpponentThrewScissors(TranslateToNeededMove(game));
      default:  return 0;
    }
  }

  private static string TranslateToNeededMove(List<string> game)
  {
    switch (game.First()) 
    {
      case "A": 
        switch (game.Last()) 
        {
          case "X": return "Z";
          case "Y": return "X";
          case "Z": return "Y";
          default: return "X";
        }
      case "B": 
        switch (game.Last()) 
        {
          case "X": return "X";
          case "Y": return "Y";
          case "Z": return "Z";
          default: return "X";
        }
      case "C": 
        switch (game.Last()) 
        {
          case "X": return "Y";
          case "Y": return "Z";
          case "Z": return "X";
          default: return "X";
        }
      default:  return "X";
    }
  }

  private static int DetermineOutcome(List<string> game)
  {
    switch (game.First()) 
    {
      case "A": return OpponentThrewRock(game.Last());
      case "B": return OpponentThrewPaper(game.Last());
      case "C": return OpponentThrewScissors(game.Last());
      default:  return 0;
    }
  }

  private static int OpponentThrewRock(string ours)
  {
    switch (ours) 
    {
      case "X": return 3;
      case "Y": return 6;
      case "Z": return 0;
      default:  return 0;
    }
  }

  private static int OpponentThrewPaper(string ours)
  {
    switch (ours) 
    {
      case "X": return 0;
      case "Y": return 3;
      case "Z": return 6;
      default:  return 0;
    }
  }

  private static int OpponentThrewScissors(string ours)
  {
    switch (ours) 
    {
      case "X": return 6;
      case "Y": return 0;
      case "Z": return 3;
      default:  return 0;
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