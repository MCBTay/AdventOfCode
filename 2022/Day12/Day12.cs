// https://adventofcode.com/2022/day/12
// --- Day 12: Hill Climbing Algorithm ---

public class Day12
{
    private static string filename = @"Day12/input.txt";

    private static List<List<char>> HeightMap;
    private static Index StartingIndex;
    private static Index EndingIndex;
    
    private static int StepCount;
    private static Index CurrentIndex;

    private static List<Index> Steps = new List<Index>();
    private static List<Index> FailedSteps = new List<Index>();

    private static char CurrentValue => HeightMap[CurrentIndex.Row][CurrentIndex.Col];

    public static void HillClimbingAlgorithm()
    {
      Console.WriteLine(" --- Day 12 ---");

      HeightMap = ParseInput();
      PrintHeightMap();
      SetIndices();

      CurrentIndex = StartingIndex;
      Steps.Add(CurrentIndex);
      DeterminePath();

      Console.WriteLine($"Step count is {Steps.Count}");
    }

    private static void DeterminePath()
    {
      var indeces = GetIndecesToConsider(CurrentIndex).OrderBy(x => CurrentValue.CompareTo(HeightMap[x.Row][x.Col]));

      foreach(var index in indeces)
      {
        if (FailedSteps.Contains(index)) continue;

        var testValue = HeightMap[index.Row][index.Col];

        if (CurrentValue == 'S' && testValue == 'a')
        {
          UpdateCurrentIndex(index);
          return;
        }
        else if (CurrentValue.CompareTo(testValue) == -1)
        {
          UpdateCurrentIndex(index);
          return;
        }
        else if (CurrentValue.CompareTo(testValue) == 0 && !Steps.Contains(index))
        {
          UpdateCurrentIndex(index);
          return;
        }
        else if (CurrentValue == 'z' && testValue == 'E')
        {
          Console.WriteLine("woohoo");
          return;
        }
      }

      FailedSteps.Add(new Index(CurrentIndex));
      Steps.RemoveAt(Steps.Count - 1);
      CurrentIndex = new Index(Steps.Last());
      DeterminePath();
    }

    private static void UpdateCurrentIndex(Index index)
    {
      CurrentIndex = new Index(index);
      Steps.Add(CurrentIndex);
      Console.WriteLine($"Current index is {CurrentIndex}");
      DeterminePath();
    }

    private static List<Index> GetIndecesToConsider(Index index)
    {
      var indeces = new List<Index>()
      {
        new Index(index.Row - 1, index.Col),
        new Index(index.Row + 1, index.Col),
        new Index(index.Row, index.Col - 1),
        new Index(index.Row, index.Col + 1)
      };

      indeces.RemoveAll(
        x => x.Row < 0 
        || x.Row > HeightMap.Count  - 1
        || x.Col < 0 
        || x.Col > HeightMap[0].Count - 1);
    
      return indeces;
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

      public Index(Index index) : this(index.Row, index.Col)
      {

      }

      public override string ToString()
      {
        return $"({Row}, {Col})[{HeightMap[Row][Col]}]";
      }

      public override bool Equals(object obj) => this.Equals(obj as Index);

      public bool Equals(Index other)
      {
        if (other is null) return false;
        if (Object.ReferenceEquals(this, other)) return true;
        return this.Row == other.Row && this.Col == other.Col;
      }
    }
}