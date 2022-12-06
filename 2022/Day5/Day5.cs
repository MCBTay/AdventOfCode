// https://adventofcode.com/2022/day/5
// --- Day 4: Supply Stacks ---

public class Day5
{
    public static void SupplyStacks()
    {
        Console.WriteLine(" --- Day 5---");
        var assignments = ParseInput();
    }

    private static List<List<List<int>>> ParseInput()
    {
        var assignments = new List<List<List<int>>>();
    
        foreach (var line in System.IO.File.ReadLines(@"Day4/input.txt")) 
        {
          var pair = new List<List<int>>();

          assignments.Add(pair);
        }

        return assignments;
    }
}