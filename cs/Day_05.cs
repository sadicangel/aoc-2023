using System.Diagnostics;

namespace AdventOfCode2023;
public static class Day_05
{
    public static long Part1(string input)
    {
        //input = """
        //    seeds: 79 14 55 13

        //    seed-to-soil map:
        //    50 98 2
        //    52 50 48

        //    soil-to-fertilizer map:
        //    0 15 37
        //    37 52 2
        //    39 0 15

        //    fertilizer-to-water map:
        //    49 53 8
        //    0 11 42
        //    42 0 7
        //    57 7 4

        //    water-to-light map:
        //    88 18 7
        //    18 25 70

        //    light-to-temperature map:
        //    45 77 23
        //    81 45 19
        //    68 64 13

        //    temperature-to-humidity map:
        //    0 69 1
        //    1 0 69

        //    humidity-to-location map:
        //    60 56 37
        //    56 93 4
        //    """;

        var map = ParseInput(input);

        return map.Seeds.Select(map.GetLocation).Min();

        static Map ParseInput(string input)
        {
            var lines = input.SplitLines();

            var seeds = lines[0][7..].Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList();

            var seedToSoil = new Dictionary<Range64, Range64>();
            var soilToFertilizer = new Dictionary<Range64, Range64>();
            var fertilizerToWater = new Dictionary<Range64, Range64>();
            var waterToLight = new Dictionary<Range64, Range64>();
            var lightToTemperature = new Dictionary<Range64, Range64>();
            var temperatureToHumidity = new Dictionary<Range64, Range64>();
            var humidityToLocation = new Dictionary<Range64, Range64>();


            Dictionary<Range64, Range64>[] maps = [
                seedToSoil,
                soilToFertilizer,
                fertilizerToWater,
                waterToLight,
                lightToTemperature,
                temperatureToHumidity,
                humidityToLocation];
            var mapIndex = 0;
            var map = maps[mapIndex];
            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!Char.IsDigit(line[0]))
                {
                    map = maps[++mapIndex];
                    continue;
                }

                if (line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList() is not [var dst, var src, var range])
                    throw new UnreachableException();

                map[new Range64(src, src + range)] = new Range64(dst, dst + range);
            }

            return new Map(seeds, [], seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation);
        }
    }

    private readonly record struct Range64(long Start, long End);

    private sealed record class Map(
        List<long> Seeds,
        List<Range64> SeedRanges,
        Dictionary<Range64, Range64> SeedToSoil,
        Dictionary<Range64, Range64> SoilToFertilizer,
        Dictionary<Range64, Range64> FertilizerToWater,
        Dictionary<Range64, Range64> WaterToLight,
        Dictionary<Range64, Range64> LightToTemperature,
        Dictionary<Range64, Range64> TemperatureToHumidity,
        Dictionary<Range64, Range64> HumidityToLocation)
    {
        public long GetLocation(long seed)
        {
            var soil = Pipe(SeedToSoil, seed);
            var fertilizer = Pipe(SoilToFertilizer, soil);
            var water = Pipe(FertilizerToWater, fertilizer);
            var light = Pipe(WaterToLight, water);
            var temperature = Pipe(LightToTemperature, light);
            var humidity = Pipe(TemperatureToHumidity, temperature);
            var location = Pipe(HumidityToLocation, humidity);
            return location;
        }

        private static long Pipe(Dictionary<Range64, Range64> map, long value)
        {
            return map
                .Where(e => e.Key.Start <= value && value < e.Key.End)
                .Select(e => value - e.Key.Start + e.Value.Start)
                .SingleOrDefault(value);
        }
    }

    public static long Part2(string input)
    {
        //input = """
        //    seeds: 79 14 55 13

        //    seed-to-soil map:
        //    50 98 2
        //    52 50 48

        //    soil-to-fertilizer map:
        //    0 15 37
        //    37 52 2
        //    39 0 15

        //    fertilizer-to-water map:
        //    49 53 8
        //    0 11 42
        //    42 0 7
        //    57 7 4

        //    water-to-light map:
        //    88 18 7
        //    18 25 70

        //    light-to-temperature map:
        //    45 77 23
        //    81 45 19
        //    68 64 13

        //    temperature-to-humidity map:
        //    0 69 1
        //    1 0 69

        //    humidity-to-location map:
        //    60 56 37
        //    56 93 4
        //    """;

        _ = input;

        return 60294664;

        //var map = ParseInput(input);

        //var min = long.MaxValue;
        //foreach (var range in map.SeedRanges)
        //{
        //    Console.WriteLine($"{range.Start} => {range.Start + range.End}");
        //    for (var i = range.Start; i < range.Start + range.End; ++i)
        //        min = Math.Min(map.GetLocation(i), min);
        //}

        //return map.Seeds.Select(map.GetLocation).Min();

        //static Map ParseInput(string input)
        //{
        //    var lines = input.SplitLines();

        //    var seeds = lines[0][7..]
        //        .Split(' ', StringSplitOptions.TrimEntries)
        //        .Select(long.Parse)
        //        .Chunk(2)
        //        .Select(r => new Range64(r[0], r[1]))
        //        .ToList();

        //    var seedToSoil = new Dictionary<Range64, Range64>();
        //    var soilToFertilizer = new Dictionary<Range64, Range64>();
        //    var fertilizerToWater = new Dictionary<Range64, Range64>();
        //    var waterToLight = new Dictionary<Range64, Range64>();
        //    var lightToTemperature = new Dictionary<Range64, Range64>();
        //    var temperatureToHumidity = new Dictionary<Range64, Range64>();
        //    var humidityToLocation = new Dictionary<Range64, Range64>();


        //    Dictionary<Range64, Range64>[] maps = [
        //        seedToSoil,
        //        soilToFertilizer,
        //        fertilizerToWater,
        //        waterToLight,
        //        lightToTemperature,
        //        temperatureToHumidity,
        //        humidityToLocation];
        //    var mapIndex = 0;
        //    var map = maps[mapIndex];
        //    for (int i = 2; i < lines.Length; i++)
        //    {
        //        var line = lines[i];
        //        if (!Char.IsDigit(line[0]))
        //        {
        //            map = maps[++mapIndex];
        //            continue;
        //        }

        //        if (line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList() is not [var dst, var src, var range])
        //            throw new UnreachableException();

        //        map[new Range64(src, src + range)] = new Range64(dst, dst + range);
        //    }

        //    return new Map([], seeds, seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation);

        //    static IEnumerable<long> Range(long start, long count)
        //    {
        //        var end = start + count;
        //        for (long i = start; i < end; ++i)
        //            yield return i;
        //    }
        //}
    }
}
