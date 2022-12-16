// https://adventofcode.com/2022/day/12
// --- Day 12: Hill Climbing Algorithm ---

public class Day12
{
    private static string filename = @"Day12/sample_input.txt";

    private static List<List<char>> HeightMap;
    private static Index StartingIndex;
    private static Index EndingIndex;
    private static int StepCount;

    public static void HillClimbingAlgorithm()
    {
      Console.WriteLine(" --- Day 12 ---");

      HeightMap = ParseInput();
      PrintHeightMap();
      SetIndices();
    }

    private static void SetIndices()
    {
      for (int i = 0; i < HeightMap.Count; i++)
      {
        for (int j = 0; j < HeightMap[i].Count; j++)
        {
          switch(HeightMap[i][j])
          {
            case 'S':
              StartingIndex = new Index(i, j);
              break;
            case 'E':
              EndingIndex = new Index(i, j);
              break;
          }
        }
      }
    }

    private static void PrintHeightMap()
    {
      foreach (var row in HeightMap)
      {
        foreach (var col in row)
        {
          Console.Write(col);
        }
        Console.WriteLine();
      }
    }

    private static List<List<char>> ParseInput()
    {
      var heightMap = new List<List<char>>();
      
      foreach (var line in System.IO.File.ReadLines(filename))
      {
        var row = new List<char>();
        foreach (var character in line)
        {
          row.Add(character);
        }
        heightMap.Add(row);
      }
      
      return heightMap;
    }

    public class Index 
    {
      public int Row;
      public int Col;

      public Index(int row, int col)
      {
        Row = row;
        Col = col;
      }
    }
}