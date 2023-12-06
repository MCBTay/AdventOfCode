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
        GetLowestLocationSeedPairs();
    }

    private static void GetLowestLocation()
    {
        Console.WriteLine($"Lowest location is {almanac.Seeds.Select(x => x.GetLocation()).Min()}.");
    }

    private static void GetLowestLocationSeedPairs()
    {
        var lowest = almanac.SeedPairs
            .Select(x => (seed: x, range: almanac.SeedToSoilMap.SliceRanges(x.Range)))
            .Select(x => (seed: x.seed, range: almanac.SoilToFertilizerMap.SliceRanges(x.range)))
            .Select(x => (seed: x.seed, range: almanac.FertilizerToWaterMap.SliceRanges(x.range)))
            .Select(x => (seed: x.seed, range: almanac.WaterToLightMap.SliceRanges(x.range)))
            .Select(x => (seed: x.seed, range: almanac.LightToTemperatureMap.SliceRanges(x.range)))
            .Select(x => (seed: x.seed, range: almanac.TemperatureToHumidityMap.SliceRanges(x.range)))
            .Select(x => (seed: x.seed, range: almanac.HumidityToLocationMap.SliceRanges(x.range)))
            .SelectMany(x =>
                x.range.GetStartIndexes().OrderBy(y => y.Key).Select(y =>
                    (seed: x.seed.Range.Intervals[0].Start + y.Value, location: y.Key)))
            .OrderBy(x => x.location)
            .First();

        Console.WriteLine($"Lowest location (seed pairs) is {lowest.location}.");
    }

    private static void ParseInput()
    {
        var text = File.ReadAllText(@"Day5/example_input.txt");

        var lines = text.Split('\n').ToList();

        Map? mapToUpdate = null;
        foreach (var line in lines)
        {
            if (line.Contains("seeds:"))
            {
                ParseSeeds(line);
                ParseSeedPairs(line);
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
          .Select(x => new Seed(long.Parse(x)))
          .ToList();
    }

    private static void ParseSeedPairs(string line)
    {
        for (int i = 0; i < almanac.Seeds.Count; i += 2)
        {
            almanac.SeedPairs.Add(new SeedPair(almanac.Seeds.ElementAt(i).Id, almanac.Seeds.ElementAt(i + 1).Id));
        }
    }

    private static Map DetermineMapToUpdate(string line)
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
        public List<SeedPair> SeedPairs { get; set; } = new List<SeedPair>();

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

        public Range SliceRanges(Range range)
        {
            var currentRange = range;

            foreach (var mapping in Entries)
            {
                currentRange = mapping.SliceRange(currentRange);
            }

            foreach (var interval in currentRange.Intervals)
            {
                interval.Start = MapLookup(interval.Start);
            }

            return currentRange;
        }
    }

    public class Interval
    {
        public long Start { get; set; }
        public long Length { get; set; }
        public long End => Start + Length;

        public Interval(long start, long length)
        {
            Start = start;
            Length = length;
        }
    }

    public class Range
    {
        public List<Interval> Intervals { get; set; } = new List<Interval>();

        public Range() : this(0, 0) { }

        public Range(long start, long length)
        {
            Intervals.Add(new Interval(start, length));
        }

        public Dictionary<long, long> GetStartIndexes()
        {
            var startIndexes = new Dictionary<long, long>();
            
            long currentIndex = 0; 

            foreach (var interval in Intervals)
            {
                startIndexes[interval.Start] = currentIndex;
                currentIndex += interval.Length;
            }

            return startIndexes;
        }
    }

    public class SeedPair : Seed
    {
        public Range Range { get; set; }

        public SeedPair(long start, long length) : base(0)
        {
            Range = new Range(start, length);
        }
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

        protected long MapLookup(Map map, long id)
        {
            var answer = map.Entries
                .Where(x => id >= x.Source.Start && id <= x.Source.End)
                .Select(x => x.Dest.Start + (id - x.Source.Start))
                .FirstOrDefault();

            if (answer == 0) answer = id;

            return answer;
        }
    }

    public class MapEntry
    {
        public Interval Dest { get; set; }
        public Interval Source { get; set; }

        public MapEntry(long destRangeStart, long sourceRangeStart, long rangeLength)
        {
            Dest = new Interval(destRangeStart, rangeLength);
            Source = new Interval(sourceRangeStart, rangeLength);
        }

        public Range SliceRange(Range range)
        {
            var newRange = new Range();

            foreach (var interval in range.Intervals)
            {
                if (interval.End <= Source.Start || interval.Start >= Source.End)
                {
                    // interval doesn't overlap with this entry
                    newRange.Intervals.Add(interval);
                }
                else
                {
                    // add part of the interval before this entry
                    if (interval.Start < Source.Start)
                    {
                        newRange.Intervals.Add(new Interval(interval.Start, Source.Start - interval.Start));
                    }

                    // add the part of the interval after this entry
                    if (interval.End > Source.End)
                    {
                        newRange.Intervals.Add(new Interval(Source.End, interval.End - Source.End));
                    }

                    // now the parts that overlap
                    long overlapStart = Math.Max(interval.Start, Source.Start);
                    long overlapEnd = Math.Min(interval.End, Source.End);

                    newRange.Intervals.Add(new Interval(overlapStart, overlapEnd - overlapStart));
                }
            }

            return newRange;
        }
    }
}
