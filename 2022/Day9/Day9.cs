// https://adventofcode.com/2022/day/9
// --- Day 9: Rope Bridge ---

public class Day9
{
    private static string filename = @"Day9/sample_input.txt";

    private static List<List<string>> Grid;

    private static int HeadRow;
    private static int HeadCol;
    private static int TailRow;
    private static int TailCol;
    
    public static void RopeBridge()
    {
      Console.WriteLine(" --- Day 9 ---");

      
      var moves = ParseInput();

      var gridWidth = moves.Max(x => x.Amount);
      CreateGrid(gridWidth);
      PrintGrid();
      
      foreach (var move in moves)
      {
        UpdateHeadPosition(move);
        //PerformMove(move);
        PrintGrid();
      }
    }

    private static void UpdateHeadPosition(Move move) 
    {
      switch (move.Direction)
      {
        case Directions.L:
          Grid[HeadRow][HeadCol] = ".";
          Grid[HeadRow][HeadCol - move.Amount] = "H";
          HeadCol -= move.Amount;

          if (Math.Abs(HeadCol - TailCol) > 1)
          {
            Grid[TailRow][TailCol] = ".";

            if (TailRow != HeadRow && move.Amount > 1) 
            {
              TailRow = HeadRow;
            }

            Grid[TailRow][TailCol - move.Amount + 1] = "T";
            TailCol = TailCol - move.Amount + 1;
          }
          break;
        case Directions.R:
          Grid[HeadRow][HeadCol] = ".";
          Grid[HeadRow][HeadCol + move.Amount] = "H";
          HeadCol += move.Amount;

          if (HeadCol - TailCol > 1)
          {
            Grid[TailRow][TailCol] = ".";

            if (TailRow != HeadRow && move.Amount > 1) 
            {
              TailRow = HeadRow;
            }

            Grid[TailRow][TailCol + move.Amount - 1] = "T";
            TailCol = TailCol + move.Amount - 1;
          }
          break;
        case Directions.D:
          Grid[HeadRow][HeadCol] = ".";
          Grid[HeadRow + move.Amount][HeadCol] = "H";
          HeadRow += move.Amount;

          if (Math.Abs(HeadRow - TailRow) > 1)
          {
            Grid[TailRow][TailCol] = ".";

            if (TailCol != HeadCol && move.Amount > 1)
            {
              TailCol = HeadCol;
            }

            Grid[TailRow + move.Amount - 1][TailCol] = "T";
            TailRow = TailRow + move.Amount - 1;
          }
          break;
        case Directions.U:
          Grid[HeadRow][HeadCol] = ".";
          Grid[HeadRow - move.Amount][HeadCol] = "H";
          HeadRow -= move.Amount;

          if (Math.Abs(HeadRow - TailRow) > 1)
          {
            Grid[TailRow][TailCol] = ".";

            if (TailCol != HeadCol && move.Amount > 1)
            {
              TailCol = HeadCol;
            }

            Grid[TailRow - move.Amount + 1][TailCol] = "T";
            TailRow = TailRow - move.Amount + 1;
          }
          break;
      }
    }

    private static void UpdateTailPosition(int amount)
    {
      
    }

    private static void CreateGrid(int width)
    {
      Grid = new List<List<string>>();

      for (int i = 0; i < width; i++)
      {
        var list = new List<string>();
        for (int j = 0; j < width + 1; j++)
        {
          list.Add(".");
        }
        Grid.Add(list);
      }

      Grid[Grid.Count() - 1][0] = "H";
      HeadRow = TailRow = Grid.Count() - 1;
      HeadCol = TailCol = 0;
    }

    private static void PrintGrid()
    {
      foreach (var row in Grid)
      {
        foreach (var character in row)
        {
          Console.Write(character);
        }
        Console.WriteLine();
      }
      Console.WriteLine("------------------------");
    }

    private static List<Move> ParseInput()
    {
      var result = new List<Move>();
      
      foreach (var line in System.IO.File.ReadLines(filename))
      {
        var split = line.Split(' ');

        if (!Enum.TryParse(split[0].ToString(), out Directions direction)) continue;
        if (!Int32.TryParse(split[1].ToString(), out var amount)) continue;

        result.Add(new Move(direction, amount));
      }

      return result;
    }
}

public enum Directions
{
  L,
  R,
  D,
  U
}

public class Move
{
  public Directions Direction;
  public int Amount;

  public Move(Directions direction, int amount)
  {
    Direction = direction;
    Amount = amount;
  }
}
