// https://adventofcode.com/2022/day/5
// --- Day 5: Supply Stacks ---

public class Day5
{
    private static string filename = @"Day5/input.txt";
    public static void SupplyStacks()
    {
      Console.WriteLine(" --- Day 5---");
      var stacks = ParseStacksInput();
      var moves = ParseMovesInput();

      UpdateStacks(moves, stacks);
      PrintTopRowResults(stacks);

      stacks = ParseStacksInput();
      UpdateStacks(moves, stacks, true);
      PrintTopRowResults(stacks);
    }

    private static void UpdateStacks(List<List<int>> moves, List<List<string>> stacks, bool moveMultiple = false)
    {
      foreach (var move in moves)
      {
        var numCrates = move.First();
        var sourceStack = move.ElementAt(1) - 1;
        var destStack = move.Last() - 1;

        if (moveMultiple)
        {
          var itemsToMove = stacks[sourceStack].TakeLast(numCrates);
          stacks[destStack].AddRange(itemsToMove);
          stacks[sourceStack].RemoveRange(stacks[sourceStack].Count() - numCrates, numCrates);
          continue;
        }

        for (int i = 0; i < numCrates; i++)
        {
          var itemToMove = stacks[sourceStack].Last();
          stacks[destStack].Add(itemToMove);
          stacks[sourceStack].RemoveAt(stacks[sourceStack].Count() - 1);
        }
      }
    }

    private static void PrintStacks(List<List<string>> stacks)
    {
      Console.WriteLine("-----");

      for (int i = 1; i < stacks.Count() + 1; i++)
      {
        Console.Write($"Stack {i}: ");
        foreach (var crate in stacks[i - 1])
        {
          Console.Write($"{crate}, ");
        }
        Console.WriteLine();
      }
    }

    private static void PrintTopRowResults(List<List<string>> stacks)
    {
      Console.Write("Top row is: ");
      foreach (var stack in stacks) 
      {
        Console.Write(stack.Last());
      }
      Console.WriteLine();
    }

    private static List<List<string>> ParseStacksInput()
    {
      var stacks = new List<List<string>>();

      var numStacks = 0;
    
      foreach (var line in System.IO.File.ReadLines(filename)) 
      {
        if (string.IsNullOrEmpty(line)) break;
        if (!line.Contains('[')) continue;

        if (numStacks == 0)
        {
          numStacks = (line.Length + 1) / 4;

          for (int i = 0; i < numStacks; i++)
          {
            stacks.Add(new List<string>());
          }
        }

        for (int i = 0; i < line.Length; i++)
        {
          if (line[i] == '[') continue;
          if (line[i] == ']') continue;
          if (line[i] == ' ') continue;

          var stackIndex = i / 4;
          if (stackIndex == stacks.Count()) stackIndex -= 1;
          stacks[stackIndex].Insert(0, line[i].ToString());
        }
      }

      return stacks;
    }

    private static List<List<int>> ParseMovesInput()
    {
        var moves = new List<List<int>>();
        
        var parseMoves = false;
    
        foreach (var line in System.IO.File.ReadLines(filename)) 
        {
          if (string.IsNullOrEmpty(line)) {
            parseMoves = true;
            continue;
          }

          if (parseMoves)
          {
            var split = line.Split(' ');
            moves.Add(new List<int> 
            { 
              Convert.ToInt32(split[1]),
              Convert.ToInt32(split[3]),
              Convert.ToInt32(split[5]),
            });
          }
        }

        return moves;
    }
}