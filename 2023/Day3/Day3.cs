// https://adventofcode.com/2023/day/3
// --- Day 3:  ---

public class Day3
{
  public static void CubeConundrum()
  {
    Console.WriteLine(" --- Day 3 ---");
    var games = ParseInput();

    var sum = 0;
    var sumOfPowers = 0;
    foreach (var game in games)
    {      
      if (game.CubeSets.All(x => x.IsPossible()))
      {
        sum += game.Id;
      }

      sumOfPowers += game.GetPowerOfSet();
    }

    Console.WriteLine($"Sum of game IDs is {sum}.");
    Console.WriteLine($"Sum of powers is {sumOfPowers}.");
  }

  private static List<Game> ParseInput()
  {
    var games = new List<Game>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day3/example_input.txt")) 
    {
        var game = ParseGame(line);
        games.Add(game);
    }

    return games;
  }
}
