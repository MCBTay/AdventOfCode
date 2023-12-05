// https://adventofcode.com/2023/day/2
// --- Day 2: Cube Conundrum ---

public class Day2
{
  public static void CubeConundrum()
  {
    Console.WriteLine(" --- Day 2 ---");
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
    
    foreach (var line in System.IO.File.ReadLines(@"Day2/input.txt")) 
    {
        var game = ParseGame(line);
        games.Add(game);
    }

    return games;
  }

  private static Game ParseGame(string line)
  {
    var game = new Game();

    var split = line.Split(":");
    game.Id = Int32.Parse(split[0].Split(" ")[1]);

    var cubeSets = split[1].Split(';');

    foreach (var cubeSet in cubeSets)
    {
      var newCubeSet = ParseCubeSet(cubeSet);

      game.CubeSets.Add(newCubeSet);
    }

    return game;
  }

  private static CubeSet ParseCubeSet(string input)
  {
    var cubeSet = new CubeSet();

    var cubes = input.Split(',');

    foreach (var cube in cubes)
    {
      var numberAndColor = cube.Trim().Split(' ');

      switch (numberAndColor[1])
      {
        case "blue": 
          cubeSet.BlueCubes = Int32.Parse(numberAndColor[0]);
          break;
        case "green":
          cubeSet.GreenCubes = Int32.Parse(numberAndColor[0]);
          break;
        case "red":
          cubeSet.RedCubes = Int32.Parse(numberAndColor[0]);
          break;
        default: break;
      }
    }

    return cubeSet;
  }

  public class Game 
  {
    public int Id { get; set; }
    public List<CubeSet> CubeSets { get; set; } = new List<CubeSet>();

    public int GetPowerOfSet() => GetMinimunNumberOfRedCubes() * GetMinimunNumberOfGreenCubes() * GetMinimunNumberOfBlueCubes();

    private int GetMinimunNumberOfRedCubes() => CubeSets.Select(x => x.RedCubes).Max();
    private int GetMinimunNumberOfBlueCubes() => CubeSets.Select(x => x.BlueCubes).Max();
    private int GetMinimunNumberOfGreenCubes() => CubeSets.Select(x => x.GreenCubes).Max();
  }

  public class CubeSet 
  {
    public int RedCubes { get; set; }
    public int BlueCubes { get; set; }
    public int GreenCubes { get; set; }

    public bool IsPossible() => RedCubes <= 12 && GreenCubes <= 13 && BlueCubes <= 14;
  }
}
