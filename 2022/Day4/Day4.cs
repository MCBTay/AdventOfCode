// https://adventofcode.com/2022/day/4
// --- Day 4: Camp Cleanup ---

public class Day4
{
    public static void CampCleanup()
    {
        Console.WriteLine(" --- Day 4 ---");
        var assignments = ParseInput();

        var totalOverlaps = 0;
        foreach (var assignment in assignments)
        {
          var first = assignment.First();
          var last = assignment.Last();

          if (first.First() <= last.First() &&
              first.Last() >= last.Last())
          {
            totalOverlaps++;
            continue;
          }

          if (first.First() >= last.First() &&
              first.Last() <= last.Last())
          {
            totalOverlaps++;
            continue;
          }
        }
        Console.WriteLine($"Total overlaps are {totalOverlaps}");
    }

    private static List<List<List<int>>> ParseInput()
    {
        var assignments = new List<List<List<int>>>();
    
        foreach (var line in System.IO.File.ReadLines(@"Day4/input.txt")) 
        {
          var pair = new List<List<int>>();

          var range1 = line.Split(',')[0];
          var range2 = line.Split(',')[1];

          pair.Add(RangeToListOfInt(range1));
          pair.Add(RangeToListOfInt(range2));

          assignments.Add(pair);
        }

        return assignments;
    }

    private static List<int> RangeToListOfInt(string range)
    {
      var ints = new List<int>();

      var split = range.Split('-');

      for (int i = Convert.ToInt32(split[0]); i <= Convert.ToInt32(split[1]); i++)
      {
        ints.Add(i);
      }

      return ints;
    }
}