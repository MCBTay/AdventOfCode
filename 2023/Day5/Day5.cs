// https://adventofcode.com/2023/day/5
// --- Day 5: If You Give A Seed A Fertilizer ---

public class Day5
{
    private const string SeedToSoil = "seed-to-soil";
    private const string SoilToFertilizer = "soil-to-fertilizer";
    private const string FertilizerToWater = "fertilizer-to-water";
    private const string WaterToLight = "water-to-light";
    private const string LightToTemperature = "light-to-temperature";
    private const string TemperatureToHumidity = "temperature-to-humidity";
    private const string HumidityToLocation = "humidity-to-location";

    public static Almanac almanac { get; set; } = new Almanac();

    public static void IfYouGiveASeedAFertilizer()
    {
        Console.WriteLine(" --- Day 5 ---");
        ParseInput();

        Console.WriteLine("hehe");
    }

    private static void ParseInput()
    {
        var text = File.ReadAllText(@"Day5/example_input.txt");

        var lines = text.Split('\n').ToList();
        
        Map mapToUpdate = null;
        foreach (var line in lines)
        {
            if (line.Contains("seeds:"))
            {
                ParseSeeds(line);
                continue;
            }

            if (string.IsNullOrEmpty(line) || line == "\r")
            {
                mapToUpdate = null;
                continue;
            }

            if (char.IsDigit(line[0]) && mapToUpdate != null)
            {
                var entries = line
                    .Split(' ')
                    .Select(int.Parse);

                var mapEntry = new MapEntry(entries.ElementAt(0), entries.ElementAt(1), entries.ElementAt(2));

                mapToUpdate.Entries.Add(mapEntry);
            }
            else
            {
                mapToUpdate = DetermineMapToUpdate(line);
            }
        }
    }

    private static void ParseSeeds(string line)
    {
        almanac.Seeds = line
          .Split(':')[1]
          .Split(' ')
          .Where(x => !string.IsNullOrEmpty(x))
          .Select(x => Int32.Parse(x))
          .ToList();

        // Console.WriteLine($"Found {almanac.Seeds.Count} seeds...");

        // foreach (var seed in almanac.Seeds)
        // {
        //   Console.Write($"{seed}, ");
        // }
        // Console.WriteLine();
    }

    private static Map DetermineMapToUpdate(string line)
    {
        return line switch
        {
            string s when s.StartsWith(SeedToSoil) => almanac.SeedToSoilMap,
            string s when s.StartsWith(SoilToFertilizer) => almanac.SoilToFertilizerMap,
            string s when s.StartsWith(FertilizerToWater) => almanac.FertilizerToWaterMap,
            string s when s.StartsWith(WaterToLight) => almanac.WaterToLightMap,
            string s when s.StartsWith(LightToTemperature) => almanac.LightToTemperatureMap,
            string s when s.StartsWith(TemperatureToHumidity) => almanac.TemperatureToHumidityMap,
            string s when s.StartsWith(HumidityToLocation) => almanac.HumidityToLocationMap,
            _ => null
        };
    }

    public class Almanac
    {
        public List<int> Seeds { get; set; } = new List<int>();

        public Map SeedToSoilMap { get; set; } = new Map();
        public Map SoilToFertilizerMap { get; set; } = new Map();
        public Map FertilizerToWaterMap { get; set; } = new Map();
        public Map WaterToLightMap { get; set; } = new Map();
        public Map LightToTemperatureMap { get; set; } = new Map();
        public Map TemperatureToHumidityMap { get; set; } = new Map();
        public Map HumidityToLocationMap { get; set; } = new Map();
    }

    public class Map
    {
        public List<MapEntry> Entries { get; set; } = new List<MapEntry>();
    }

    public class MapEntry
    {
        public int DestRangeStart { get; set; }
        public int SourceRangeStart { get; set; }
        public int RangeLength { get; set; }

        public MapEntry(int destRangeStart, int sourceRangeStart, int rangeLength)
        {
            DestRangeStart = destRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }
    }
}
