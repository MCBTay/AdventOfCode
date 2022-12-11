// https://adventofcode.com/2022/day/8
// --- Day 8: Treetop Tree House ---

public class Day8
{
    private static string filename = @"Day8/sample_input.txt";
    
    public static void TreetopTreeHouse()
    {
      Console.WriteLine(" --- Day 8 ---");

      var trees = ParseInput();
      
      foreach (var row in trees)
      {
        foreach (var tree in row)
        {
          Console.Write(tree);
        }
        Console.WriteLine();
      }
    }

    private static List<List<int>> ParseInput()
    {
      var treeGrid = new List<List<int>>();
      var treeRow = new List<int>();
      
      foreach (var line in System.IO.File.ReadLines(filename)) 
      {
        foreach (var character in line)
        {
          if (!int.TryParse(character.ToString(), out var treeHeight)) continue;
          treeRow.Add(treeHeight);          
        }
        treeGrid.Add(treeRow);
        treeRow = new List<int>();
      }

      return treeGrid;
    }
}