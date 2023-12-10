// https://adventofcode.com/2023/day/8
// --- Day 8: Haunted Wasteland ---

public class Day8
{
    public static List<string> Instructions = new List<string>();
    public static Dictionary<string, Node> Steps = new Dictionary<string, Node>();

    public static void HauntedWasteland()
    {
        Console.WriteLine(" --- Day 8 ---");
        ParseInput();

        Console.WriteLine($"Num steps to get to ZZZ is {GetSteps("AAA", x => x.Key == "ZZZ").Count}.");
        Console.WriteLine($"Num steps to end on nodes ending with Z is {GetNumStepsPart2()}.");
    }

    private static List<string> GetSteps(string start, Func<Node, bool> condition)
    {
        var current = Steps[start];
        var count = 0;
        var path = new List<string>();

        while (!condition(current))
        {
            path.Add(current.Key);
            current = Steps[Instructions[count % Instructions.Count] == "L" ? current.Left : current.Right];
            count++;
        }

        return path;
    }

    private static long GetNumStepsPart2()
    {
        var stepCounts = Steps.Values
            .Where(x => x.Key.EndsWith("A"))
            .Select(x => (long)GetSteps(x.Key, y => y.Key.EndsWith("Z")).Count);

        return LCM(stepCounts);
    }

    private static long GCD(long left, long right)
    {
        while (right != 0)
        {
            left %= right;
            (left, right) = (right, left);
        }

        return left;
    }

    private static long LCM(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(1l, (x, y) => x / GCD(x, y) * y);
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
