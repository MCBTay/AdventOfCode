// https://adventofcode.com/2022/day/5
// --- Day 5: Supply Stacks ---

public class Day5
{
    private static string filename = @"Day5/sample_input.txt";
    public static void SupplyStacks()
    {
      Console.WriteLine(" --- Day 5---");
      var stacks = ParseStacksInput();
      var moves = ParseMovesInput();

      for (int i = 0; i < stacks.Count(); i++)
      {
        Console.Write($"Stack {i}: ");
        foreach (var crate in stacks[i])
        {
          Console.Write(crate + ", ");
        }
        Console.WriteLine();
      }
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

          var stackIndex = i / 3;
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