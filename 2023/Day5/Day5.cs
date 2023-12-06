// https://adventofcode.com/2023/day/5
// --- Day 5: If You Give A Seed A Fertilizer ---

using System.Security.Cryptography.X509Certificates;

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

        GetLowestLocation();
    }

    private static void GetLowestLocation()
    {
        Console.WriteLine($"Lowest location is {almanac.Seeds.Select(x => x.GetLocation()).Min()}.");
    }

    private static void ParseInput()
    {
        var text = File.ReadAllText(@"Day5/input.txt");

        var lines = text.Split('\n').ToList();

        List<MapEntry>? mapToUpdate = null;
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
                    .Select(long.Parse);

                var mapEntry = new MapEntry(entries.ElementAt(0), entries.ElementAt(1), entries.ElementAt(2));

                mapToUpdate.Add(mapEntry);
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
          .Select(x => new Seed(long.Parse(x)))
          .ToList();
    }

    private static List<MapEntry> DetermineMapToUpdate(string line)
    {
        return line switch
        {
            { } s when s.StartsWith(SeedToSoil) => almanac.SeedToSoilMap,
            { } s when s.StartsWith(SoilToFertilizer) => almanac.SoilToFertilizerMap,
            { } s when s.StartsWith(FertilizerToWater) => almanac.FertilizerToWaterMap,
            { } s when s.StartsWith(WaterToLight) => almanac.WaterToLightMap,
            { } s when s.StartsWith(LightToTemperature) => almanac.LightToTemperatureMap,
            { } s when s.StartsWith(TemperatureToHumidity) => almanac.TemperatureToHumidityMap,
            { } s when s.StartsWith(HumidityToLocation) => almanac.HumidityToLocationMap,
            _ => throw new ArgumentOutOfRangeException(nameof(line), line, null)
        };
    }

    public class Almanac
    {
        public List<Seed> Seeds { get; set; } = new List<Seed>();

        public List<MapEntry> SeedToSoilMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> SoilToFertilizerMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> FertilizerToWaterMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> WaterToLightMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> LightToTemperatureMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> TemperatureToHumidityMap { get; set; } = new List<MapEntry>();
        public List<MapEntry> HumidityToLocationMap { get; set; } = new List<MapEntry>();
    }

    public class Seed
    {
        public long Id { get; set; }

        public Seed(long id)
        {
            Id = id;
        }

        public long GetLocation()
        {
            var soil = MapLookup(almanac.SeedToSoilMap, Id);
            var fert = MapLookup(almanac.SoilToFertilizerMap, soil);
            var aqua = MapLookup(almanac.FertilizerToWaterMap, fert);
            var lite = MapLookup(almanac.WaterToLightMap, aqua);
            var temp = MapLookup(almanac.LightToTemperatureMap, lite);
            var humd = MapLookup(almanac.TemperatureToHumidityMap, temp);
            return MapLookup(almanac.HumidityToLocationMap, humd);
        }

        private long MapLookup(List<MapEntry> map, long id)
        {
            var answer = map //almanac.SeedToSoilMap
                .Where(x => id >= x.SourceRangeStart && id <= x.SourceRangeStart + x.RangeLength)
                .Select(x => x.DestRangeStart + (id - x.SourceRangeStart))
                .FirstOrDefault();

            if (answer == 0) answer = id;

            return answer;
        }
    }

    public class MapEntry
    {
        public long DestRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public MapEntry(long destRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestRangeStart = destRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }
    }
}
