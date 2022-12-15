using System.Linq;
// https://adventofcode.com/2022/day/10
// --- Day 10: Cathode Ray Tube ---

public class Day10
{
    private static string filename = @"Day10/input.txt";
    
    private static List<List<string>> CRT;

    public static void CathodeRayTube()
    {
      Console.WriteLine(" --- Day 10 ---");

      var instructions = ParseInput();

      var instructionDict = GetInstructionDict(instructions);
      
      var signalStrengths = GetSignalStrengths(instructionDict);

      Console.WriteLine($"The sum of the six signal strengths is {signalStrengths.Sum()}");

      RenderCRT(instructionDict);
    }

    private static void RenderCRT(Dictionary<int, int> instructions)
    {
      CreateCRTGrid();

      instructions.Add(0, 1);

      for (int i = 0; i < 240; i++)
      {
        var registerX = 1;
        if (instructions.ContainsKey(i))
        {
          registerX = instructions[i];
        } 
        else
        {
          registerX = instructions[i - 1];
        } 

        var row = i / 40;
        var col = i % 40;
        if (Math.Abs(registerX - col) <= 1)
        {
          CRT[row][col] = "#";
        }
      }

      PrintCRT();
    }

    private static void PrintCRT()
    {
      foreach (var row in CRT)
      {
        foreach(var col in row)
        {
          Console.Write(col);
        }
        Console.WriteLine();
      }
    }

    private static void CreateCRTGrid()
    {
      CRT = new List<List<string>>();
      for (var i = 0; i < 6; i++)
      {
        var list = new List<string>();
        for (var j = 0; j < 40; j++)
        {
          list.Add(".");
        }
        CRT.Add(list);
      }
    }

    private static Dictionary<int, int> GetInstructionDict(List<Instruction> instructions)
    {
      var cycle = 0;
      var registerX = 1;

      var dict = new Dictionary<int, int>();

      foreach (var instruction in instructions)
      {
        if (instruction.Type == InstructionType.AddX)
        {
          cycle += 2;
          registerX += instruction.Value;
        }
        else
        {
          cycle += 1;
        }

        dict.Add(cycle, registerX);
      }

      return dict;
    }

    private static List<int> GetSignalStrengths(Dictionary<int, int> dict)
    {
      var signalStrengths = new List<int>();
      var cyclesToPoll = new List<int> { 20, 60, 100, 140, 180, 220 };

      foreach (var pollCycle in cyclesToPoll)
      {
        var value = 0;

        var cycleToConsider = pollCycle;
        var cycleNotFound = false;
        if (dict.ContainsKey(pollCycle))
        {
          cycleToConsider = pollCycle;
        }
        else
        {
          cycleToConsider = pollCycle - 1;
          cycleNotFound = true;
        }
        var index = dict.Keys.ToList().IndexOf(cycleToConsider);
        value += dict.ElementAt(cycleNotFound ? index : index - 1).Value;
        signalStrengths.Add(value * pollCycle);
      }

      return signalStrengths;
    }

    private static List<Instruction> ParseInput()
    {
      var instructions = new List<Instruction>();
      
      foreach (var line in System.IO.File.ReadLines(filename)) 
      {
        var split = line.Split(' ');

        if (!Enum.TryParse(split[0], true, out InstructionType type)) continue;
        int value = 0;
        if (split.Count() > 1 && !Int32.TryParse(split[1], out value)) continue;
        instructions.Add(new Instruction(type, value));
      }

      return instructions;
    }

    public enum InstructionType
    {
      NoOp,
      AddX
    }

    public class Instruction 
    {
      public InstructionType Type;
      public int Value;

      public Instruction(InstructionType type, int value = 0)
      {
        Type = type;
        Value = value;
      }
    }
}