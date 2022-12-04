// https://adventofcode.com/2022/day/4
// --- Day 4: Camp Cleanup ---

public class Day4
{
    public static void CampCleanup()
    {
        Console.WriteLine(" --- Day 4 ---");
        var rucksacks = ParseInput();
    }

    private static List<List<string>> ParseInput()
    {
        var rucksacks = new List<List<string>>();
    
        foreach (var line in System.IO.File.ReadLines(@"Day4/sample_input.txt")) 
        {
            var firstCompartment = String.Concat(line.Take(line.Length / 2));
            var secondCompartment = String.Concat(line.TakeLast(line.Length / 2));

            rucksacks.Add(new List<string> { firstCompartment, secondCompartment });
        }

        return rucksacks;
    }
}