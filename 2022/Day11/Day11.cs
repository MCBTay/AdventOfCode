using System.Linq;
// https://adventofcode.com/2022/day/11
// --- Day 11: Monkey in the Middle ---

public class Day11
{
    private static string filename = @"Day11/input.txt";

    private static List<Monkey> Monkeys = new List<Monkey>();
    public static void MonkeyInTheMiddle()
    {
      Console.WriteLine(" --- Day 11 ---");

      Monkeys = ParseInput();

      int numRounds = 10000;
      for (int i = 0; i < numRounds; i++)
      {
        foreach (var monkey in Monkeys)
        {
          monkey.InspectItems();
        }

        var roundCheck = new List<int> { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 1000 };
        if (roundCheck.Contains(i))
        {
          Console.WriteLine($"== After round {i} ==");
          foreach (var monkey in Monkeys)
          {
            Console.WriteLine($"Monkey {monkey.Id} inspected items {monkey.InspectionCount} times.");
          }
        }
      }

      var topMonkeys = Monkeys.OrderByDescending(x => x.InspectionCount).Take(2);
      
      Console.WriteLine($"Monkey business is {topMonkeys.First().InspectionCount * topMonkeys.Last().InspectionCount}.");
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

    private static long Lcm(IEnumerable<long> numbers)
    {
        return numbers.Aggregate((s, val) => s * val / Gcd(s, val));
    }

    private static long Gcd(long n1, long n2)
    {
        while (true)
        {
            if (n2 == 0) return n1;
            var n3 = n1;
            n1 = n2;
            n2 = n3 % n2;
        }
    }

    public class Monkey
    {
      public int Id;
      public List<long> StartingItems;
      public string Operation;
      public Test Test;
      public long InspectionCount;

      public Monkey()
      {
        Id = 0;
        StartingItems = new List<long>();
        Operation = string.Empty;
        Test = new Test();
        InspectionCount = 0;
      }

      public void InspectItems()
      {
        var lcm = Lcm(Monkeys.Select(m => m.Test.Divisor));
        foreach (var item in StartingItems)
        {
          var worryLevel = item;
          worryLevel = InspectItem(worryLevel);
          //worryLevel /= 3;
          worryLevel %= lcm;
          ThrowItem(worryLevel);
          InspectionCount++;
        }
        StartingItems.Clear();
      }

      private long InspectItem(long item)
      {
        var equation = Operation.Split(" = ")[1];
        var currentOperator = equation.Split(' ')[1];
        var value = equation.Split(' ')[2];

        long parsedInt = 0;
        if (value == "old")
        {
          parsedInt = item;
        }
        else if (!Int64.TryParse(value, out parsedInt))
        {
          return item;
        } 

        switch(currentOperator)
        {
          case "*": return item * parsedInt;
          case "+": return item + parsedInt;
          default: return item;
        }
      }

      private void ThrowItem(long item)
      {
        if (item % Test.Divisor == 0)
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

      public long Divisor 
      {
        get
        {
          var divisor = Condition.Split("divisible by ")[1];
          if (!Int64.TryParse(divisor, out var parsedDivisor)) return 1;
          return parsedDivisor;
        }
      }

      public Test()
      {
        Condition = string.Empty;
        IfTrue = string.Empty;
        IfFalse = string.Empty; 
      }
    }
}