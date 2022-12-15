using System.Linq;
// https://adventofcode.com/2022/day/11
// --- Day 11: Monkey in the Middle ---

public class Day11
{
    private static string filename = @"Day11/sample_input.txt";

    private static List<Monkey> Monkeys = new List<Monkey>();
    public static void MonkeyInTheMiddle()
    {
      Console.WriteLine(" --- Day 11 ---");

      Monkeys = ParseInput();

      foreach (var monkey in Monkeys)
      {
        monkey.InspectItems();
      }
    }

    private static List<Monkey> ParseInput()
    {
      var currentMonkey = new Monkey();
      var monkeys = new List<Monkey>();

      foreach (var line in System.IO.File.ReadLines(filename))
      {
        if (string.IsNullOrEmpty(line))
        {
          monkeys.Add(currentMonkey);
          currentMonkey = new Monkey();
        }
        else if (line.StartsWith("Monkey"))
        {
          var id = line.Split("Monkey ")[1].First().ToString();
          if (!int.TryParse(id, out var parsedId)) continue;
          currentMonkey.Id = parsedId;
        }
        else if (line.Trim().StartsWith("Starting items:"))
        {
          var worryLevels = line.Trim().Split("Starting items: ")[1];
          var list = worryLevels.Split(',');
          foreach (var item in list)
          {
            if (!Int32.TryParse(item, out var parsedInt)) continue;
            currentMonkey.StartingItems.Add(parsedInt);
          }
        }
        else if (line.Trim().StartsWith("Operation:"))
        {
          currentMonkey.Operation = line.Trim().Split("Operation: ")[1];
        }
        else if (line.Trim().StartsWith("Test"))
        {
          currentMonkey.Test.Condition = line.Trim().Split("Test: ")[1];
        }
        else if (line.Trim().StartsWith("If true:"))
        {
          currentMonkey.Test.IfTrue = line.Trim().Split("If true: ")[1];
        }
        else if (line.Trim().StartsWith("If false:"))
        {
          currentMonkey.Test.IfFalse = line.Trim().Split("If false: ")[1];
        }
      }

      monkeys.Add(currentMonkey);

      return monkeys;
    }

    public class Monkey
    {
      public int Id;
      public List<int> StartingItems;
      public string Operation;
      public Test Test;

      public Monkey()
      {
        Id = 0;
        StartingItems = new List<int>();
        Operation = string.Empty;
        Test = new Test();
      }

      public void InspectItems()
      {
        foreach (var item in StartingItems)
        {
          var worryLevel = item;
          worryLevel = InspectItem(worryLevel);
          worryLevel /= 3;
          ThrowItem(worryLevel);
        }
        StartingItems.Clear();
      }

      private int InspectItem(int item)
      {
        var equation = Operation.Split(" = ")[1];
        var currentOperator = equation.Split(' ')[1];
        var value = equation.Split(' ')[2];

        if (!Int32.TryParse(value, out var parsedInt)) return item;

        switch(currentOperator)
        {
          case "*": return item * parsedInt;
          case "+": return item + parsedInt;
          default: return item;
        }
      }

      private void ThrowItem(int item)
      {
        var divisor = Test.Condition.Split("divisible by ")[1];
        if (!Int32.TryParse(divisor, out var parsedDivisor)) return;

        if (item % parsedDivisor == 0)
        {
          var dest = Test.IfTrue.Split(' ').Last();
          if (!Int32.TryParse(dest, out var parsedDest)) return;
          Monkeys[parsedDest].StartingItems.Add(item);
        }
        else
        {
          var dest = Test.IfFalse.Split(' ').Last();
          if (!Int32.TryParse(dest, out var parsedDest)) return;
          Monkeys[parsedDest].StartingItems.Add(item);
        }
      }
    }

    public class Test
    {
      public string Condition;
      public string IfTrue;
      public string IfFalse;

      public Test()
      {
        Condition = string.Empty;
        IfTrue = string.Empty;
        IfFalse = string.Empty; 
      }
    }
}