// https://adventofcode.com/2022/day/6
// --- Day 6: Tuning Trouble ---

public class Day6
{
    private static string filename = @"Day6/input.txt";
    public static void TuningTrouble()
    {
      Console.WriteLine(" --- Day 6---");
      Console.WriteLine($"Marker index is {FindIndexOfMarker()}");
    }

    public static int FindIndexOfMarker()
    {
      var chars = System.IO.File.ReadAllText(filename);
      var marker = new List<char>();

      for (int i = 0; i < chars.Length; i++)
      {
        if (marker.Distinct().Count() == 4) return i;
        if (marker.Count() == 4) marker.RemoveAt(0);
        marker.Add(chars[i]);       
      }

      return 0;
    }
}