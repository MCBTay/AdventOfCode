// https://adventofcode.com/2023/day/2
// --- Day 1: Cube Conundrum ---

public class Day2
{
  public static void CubeConundrum()
  {
    Console.WriteLine(" --- Day 2 ---");
    var games = ParseInput();

    var sum = 0;
    foreach (var game in games)
    {      
      if (game.CubeSets.All(x => x.IsPossible()))
      {
        sum += game.Id;
      }
    }

    Console.WriteLine($"Sum of game IDs is {sum}.");
  }

  private static List<Game> ParseInput()
  {
    var games = new List<Game>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day2/input.txt")) 
    {
        var game = new Game();

        var split = line.Split(":");
        game.Id = Int32.Parse(split[0].Split(" ")[1]);

        var cubeSets = split[1].Split(';');

        foreach (var cubeSet in cubeSets)
        {
          var newCubeSet = new CubeSet();

          var cubes = cubeSet.Split(',');

          foreach (var cube in cubes)
          {
            var numberAndColor = cube.Trim().Split(' ');

            switch (numberAndColor[1])
            {
              case "blue": 
                newCubeSet.BlueCubes = Int32.Parse(numberAndColor[0]);
                break;
              case "green":
                newCubeSet.GreenCubes = Int32.Parse(numberAndColor[0]);
                break;
              case "red":
                newCubeSet.RedCubes = Int32.Parse(numberAndColor[0]);
                break;
              default: break;
            }
          }

          game.CubeSets.Add(newCubeSet);
        }

        games.Add(game);
    }

    return games;
  }

  public class Game 
  {
    public int Id { get; set; }
    public List<CubeSet> CubeSets { get; set; } = new List<CubeSet>(); 
  }

  public class CubeSet 
  {
    public int RedCubes { get; set; }
    public int BlueCubes { get; set; }
    public int GreenCubes { get; set; }

    public bool IsPossible()
    {
      return RedCubes <= 12 && GreenCubes <= 13 && BlueCubes <= 14;
    }
  }
}
