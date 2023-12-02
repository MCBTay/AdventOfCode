// https://adventofcode.com/2023/day/1
// --- Day 1: Trebuchet?! ---

public class Day1 
{
  public static void Trebuchet()
  {
    Console.WriteLine(" --- Day 1 ---");
    var lines = ParseInput();

    var sum = 0;
    foreach (var line in lines)
    {      
        var calibrationValue = string.Empty;

        var digits = line
            .Where(x => Char.IsNumber(x))
            .ToArray();

        var firstDigit = digits.FirstOrDefault();
        var lastDigit = digits.LastOrDefault();
        
        calibrationValue = $"{firstDigit}{lastDigit}";

        Console.WriteLine($"Calibration value is {calibrationValue}.");

        sum += Int32.Parse(calibrationValue);
    }

    Console.WriteLine($"Sum of calibration values is {sum}.");
  }

  private static List<string> ParseInput()
  {
    var lines = new List<string>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day1/input.txt")) 
    {
        lines.Add(line);
    }

    return lines;
  }
}
