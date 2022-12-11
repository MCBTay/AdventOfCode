using System.Linq;
// https://adventofcode.com/2022/day/8
// --- Day 8: Treetop Tree House ---

public class Day8
{
    private static string filename = @"Day8/input.txt";
    
    public static void TreetopTreeHouse()
    {
      Console.WriteLine(" --- Day 8 ---");

      var trees = ParseInput();
      
      Console.WriteLine($"There are {GetVisibleTrees(trees)} visible trees.");
    }

    private static int GetVisibleTrees(List<List<int>> treeGrid)
    {
      var exteriorTrees = (treeGrid.Count() * 2) + ((treeGrid.First().Count() - 2) * 2);

      var interiorTrees = 0;

      for (int i = 1; i < treeGrid.Count() - 1; i++)
      {
        for (int j = 1; j < treeGrid[i].Count() - 1; j++)
        {
          if (VisibleInRow(treeGrid[i], j)) interiorTrees++;
          else if (VisibleInColumn(treeGrid, i, j)) interiorTrees++;
        }
      }

      return exteriorTrees + interiorTrees;
    }

    private static bool VisibleInRow(List<int> treeRow, int j)
    {      
      return !treeRow.Take(j).Any(x => x >= treeRow[j]) // visible from left
        || !treeRow.TakeLast(treeRow.Count() - (j + 1)).Any(x => x >= treeRow[j]); // visible from right
    }

    private static bool VisibleInColumn(List<List<int>> treeGrid, int i, int j)
    {
      var column = treeGrid.Select(treeRow => treeRow[j]).ToList();
      
      return !column.Take(i).Any(x => x >= column[i]) // visible from top
        || !column.TakeLast(column.Count() - (i + 1)).Any(x => x >= column[i]); // visible from bottom
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