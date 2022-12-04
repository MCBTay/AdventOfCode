// https://adventofcode.com/2022/day/3
// --- Day 3: Rucksack Reorganization ---

public class Day3
{
    public static void RucksackReorganization()
    {
        Console.WriteLine(" --- Day 3 ---");
        var rucksacks = ParseInput();

        var sumOfPriorities = 0;
        foreach (var rucksack in rucksacks)
        {
            var commonItem = FindCommonItem(rucksack);
            sumOfPriorities += ConvertToPriority(commonItem);
        }
        Console.WriteLine($"Sum of the priorities of items in each compartment of all rucksacks: {sumOfPriorities}");
    }

    private static int ConvertToPriority(char c)
    {
        var priority = char.ToUpper(c) - 64;
        if (char.IsUpper(c)) priority += 26;
        return priority;
    }

    private static char FindCommonItem(List<string> rucksack)
    {
        var firstCompartment = rucksack.First();
        var secondCompartment = rucksack.Last();

        foreach (var item in firstCompartment)
        {
            foreach (var item2 in secondCompartment)
            {
                if (item == item2) return item;
            }
        }

        return ' ';
    }

    private static List<List<string>> ParseInput()
    {
        var rucksacks = new List<List<string>>();
    
        foreach (var line in System.IO.File.ReadLines(@"Day3/sample_input.txt")) 
        {
            var firstCompartment = String.Concat(line.Take(line.Length / 2));
            var secondCompartment = String.Concat(line.TakeLast(line.Length / 2));

            rucksacks.Add(new List<string> { firstCompartment, secondCompartment });
        }

        return rucksacks;
    }
}