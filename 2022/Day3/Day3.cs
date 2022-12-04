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

        sumOfPriorities = 0;
        var elfGroups = rucksacks.Chunk(3).ToList();
        foreach (var elfGroup in elfGroups)
        {
            var commonItem = FindCommonItem(elfGroup.ToList());
            sumOfPriorities += ConvertToPriority(commonItem);
        }
        Console.WriteLine($"Sum of the priorities of the elf group badges: {sumOfPriorities}");
    }

    private static int ConvertToPriority(char c)
    {
        var priority = char.ToUpper(c) - 64;
        if (char.IsUpper(c)) priority += 26;
        return priority;
    }

    private static char FindCommonItem(List<List<string>> elfGroup)
    {
        var ruckSacks = elfGroup.Select(x => x.First() + x.Last());
        var intersection = ruckSacks.First().Intersect(ruckSacks.ElementAt(1).Intersect(ruckSacks.Last()));
        return intersection.First();
    }

    private static char FindCommonItem(List<string> rucksack)
    {
        return rucksack.First().Intersect(rucksack.Last()).First();
    }

    private static List<List<string>> ParseInput()
    {
        var rucksacks = new List<List<string>>();
    
        foreach (var line in System.IO.File.ReadLines(@"Day3/input.txt")) 
        {
            var firstCompartment = String.Concat(line.Take(line.Length / 2));
            var secondCompartment = String.Concat(line.TakeLast(line.Length / 2));

            rucksacks.Add(new List<string> { firstCompartment, secondCompartment });
        }

        return rucksacks;
    }
}