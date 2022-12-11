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

      var scenicScore = 0;

      for (int i = 1; i < treeGrid.Count() - 1; i++)
      {
        for (int j = 1; j < treeGrid[i].Count() - 1; j++)
        {
          if (VisibleInRow(treeGrid[i], j)) interiorTrees++;
          else if (VisibleInColumn(treeGrid, i, j)) interiorTrees++;

          if (GetScenicScore(treeGrid, i, j) > scenicScore)
          {
            scenicScore = GetScenicScore(treeGrid, i, j);
          }
        }
      }

      Console.WriteLine($"Max scenic score is: {scenicScore}");

      return exteriorTrees + interiorTrees;
    }

    private static int GetScenicScore(List<List<int>> treeGrid, int i, int j)
    {
      return VisibleLeft(treeGrid, i, j) 
        * VisibleRight(treeGrid, i, j)
        * VisibleTop(treeGrid, i, j)
        * VisibleBottom(treeGrid, i, j);
    }

    private static int VisibleLeft(List<List<int>> treeGrid, int i, int j)
    { 
      var treesLookingLeft = treeGrid[i].Take(j).Reverse().ToList();

      for (int x = 0; x < treesLookingLeft.Count(); x++)
      {
        if (treesLookingLeft[x] >= treeGrid[i][j]) return x + 1;
      }
      return treesLookingLeft.Count();
    }
    
    private static int VisibleRight(List<List<int>> treeGrid, int i, int j) 
    { 
      var treesLookingRight = treeGrid[i].TakeLast(treeGrid[i].Count() - (j + 1)).ToList();

      for (int x = 0; x < treesLookingRight.Count(); x++)
      {
        if (treesLookingRight[x] >= treeGrid[i][j]) return x + 1;
      }
      return treesLookingRight.Count();
    }

    private static int VisibleTop(List<List<int>> treeGrid, int i, int j) 
    { 
      var treesLookingUp = treeGrid.Select(treeRow => treeRow[j]).Take(i).Reverse().ToList();

      for (int x = 0; x < treesLookingUp.Count(); x++)
      {
        if (treesLookingUp[x] >= treeGrid[i][j]) return x + 1;
      }
      return treesLookingUp.Count();
    }

    private static int VisibleBottom(List<List<int>> treeGrid, int i, int j) 
    { 
      var treesLookingDown = treeGrid.Select(treeRow => treeRow[j]).TakeLast(treeGrid.Select(treeRow => treeRow[j]).Count() - (i + 1)).ToList();

      for (int x = 0; x < treesLookingDown.Count(); x++)
      {
        if (treesLookingDown[x] >= treeGrid[i][j]) return x + 1;
      }
      return treesLookingDown.Count();
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