// https://adventofcode.com/2023/day/8
// --- Day 8: Haunted Wasteland ---

public class Day8
{
    public static List<string> Instructions = new List<string>();
    public static Dictionary<string, Node> Steps = new Dictionary<string, Node>();
    public static bool FoundZZZ = false;

    public static void HauntedWasteland()
    {
        Console.WriteLine(" --- Day 8 ---");
        ParseInput();

        GetNumSteps();
    }

    private static void GetNumSteps()
    {
        var current = Steps["AAA"];
        var count = 0;

        while (current.Key != "ZZZ")
        {
            current = Steps[Instructions[count % Instructions.Count] == "L" ? current.Left : current.Right];
            count++;
        }

        Console.WriteLine($"Num steps to get to ZZZ is {count}.");
    }

    private static void ParseInput()
    {
        var lines = File.ReadLines(@"Day8/input.txt");

        for (int i = 0; i < lines.Count(); i++)
        {
            var line = lines.ElementAt(i);
            if (i == 0)
            {
                Instructions = line.Select(x => x.ToString()).ToList();
                continue;
            }

            if (string.IsNullOrEmpty(line)) continue;

            var right = line.Split('=')[1]
                .Replace("(", "")
                .Replace(")", "")
                .Split(',')
                .Select(x => x.Trim())
                .ToList();

            var node = new Node
            {
                Key = line.Split('=')[0].Trim(),
                Left = right.ElementAt(0),
                Right = right.ElementAt(1)
            };

            Steps.Add(node.Key, node);
        }
    }

    public class Node
    {
        public string Key;
        public string Left;
        public string Right;
    }
}
