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

        sum += Int32.Parse(calibrationValue);
    }

    Console.WriteLine($"Sum of calibration values is {sum}.");
  }

  private static List<string> ParseInput()
  {
    var lines = new List<string>();
    
    foreach (var line in System.IO.File.ReadLines(@"Day1/input.txt")) 
    {
        var replacedLine = ReplaceWrittenWithNumber(line);

        lines.Add(replacedLine);
    }

    return lines;
  }

  private static string ReplaceWrittenWithNumber(string input)
  {
    input = InsertNumberIntoString(input, "one", "1");
    input = InsertNumberIntoString(input, "two", "2");
    input = InsertNumberIntoString(input, "three", "3");
    input = InsertNumberIntoString(input, "four", "4");
    input = InsertNumberIntoString(input, "five", "5");
    input = InsertNumberIntoString(input, "six", "6");
    input = InsertNumberIntoString(input, "seven", "7");
    input = InsertNumberIntoString(input, "eight", "8");
    input = InsertNumberIntoString(input, "nine", "9");
    return input;
  }

  private static string InsertNumberIntoString(string input, string searching, string number)
  {
    var index = input.IndexOf(searching);

    while (index != -1)
    {
        input = input.Insert(index + searching.Length - 1, number);
        index = input.IndexOf(searching, index + 1);
    }

    return input;
  }
}
