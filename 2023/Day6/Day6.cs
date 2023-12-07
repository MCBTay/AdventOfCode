// https://adventofcode.com/2023/day/6
// --- Day 6: Wait For It ---

public class Day6
{
    public static List<Race> Races = new List<Race>();

    public static void WaitForIt()
    {
        Console.WriteLine(" --- Day 6 ---");
        ParseInput();

        Console.WriteLine(Races.Aggregate(1, (x, y) => x * y.NumWaysToWin()));
    }

    private static void ParseInput()
    {
        var lines = System.IO.File.ReadLines(@"Day6/input.txt");

        var numRaces = lines.First().Split(' ').Count(x => !string.IsNullOrEmpty(x)) - 1;

        foreach (var line in lines)
        {
            var numbers = line.Split(':')[1]
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x.Trim()));

            for (int i = 0; i < numRaces; i++)
            {
                var race = Races.ElementAtOrDefault(i);

                if (race == null)
                {
                    Races.Add(new Race { Time = numbers.ElementAt(i) });
                }
                else
                {
                    race.Distance = numbers.ElementAt(i);
                }
            }
        }
    }

    public class Race
    {
        public int Time { get; set; }
        public int Distance { get; set; }

        public int NumWaysToWin()
        {
            var numWays = 0;

            for (int i = 0; i < Time; i++)
            {
                if (i == 0 || i == Time - 1) continue;
                if (i * (Time - i) <= Distance) continue;
                numWays++;
            }

            return numWays;
        }
    }
}