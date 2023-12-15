namespace AdventOfCode2023;
public static class Day_06
{
    public static int Part1(string input)
    {
        //input = """
        //    Time:      7  15   30
        //    Distance:  9  40  200
        //    """;

        var records = ParseRecords(input);

        static Dictionary<int, int> ParseRecords(string input)
        {
            var lines = input.SplitLines();
            var times = lines[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
            var distances = lines[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

            return Enumerable.Range(0, times.Length).ToDictionary(i => times[i], i => distances[i]);
        }

        static int GetDistance(int holdTime, int raceTime) => (raceTime - holdTime) * holdTime;

        var results = new List<int>(records.Count);

        foreach (var (time, distance) in records)
        {
            var start = 0;
            for (int t = 0; t <= time; ++t)
            {
                if (GetDistance(t, time) > distance)
                {
                    start = t;
                    break;
                }
            }

            var end = 0;
            for (int t = time; t >= 0; --t)
            {
                if (GetDistance(t, time) > distance)
                {
                    end = t;
                    break;
                }
            }
            results.Add(end - start + 1);
        }

        return results.Aggregate((a, b) => a * b);
    }

    public static long Part2(string input)
    {
        //input = """
        //    time:      7  15   30
        //    distance:  9  40  200
        //    """;

        static (long Time, long Distance) ParseRecord(string input)
        {
            var lines = input.SplitLines();
            var time = long.Parse(string.Concat(lines[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1)));
            var distance = long.Parse(string.Concat(lines[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1)));

            return (time, distance);
        }

        static long GetDistance(long holdTime, long raceTime) => (raceTime - holdTime) * holdTime;

        var (time, distance) = ParseRecord(input);
        var start = 0L;
        for (long t = 0; t <= time; ++t)
        {
            if (GetDistance(t, time) > distance)
            {
                start = t;
                break;
            }
        }

        var end = 0L;
        for (long t = time; t >= 0; --t)
        {
            if (GetDistance(t, time) > distance)
            {
                end = t;
                break;
            }
        }
        return end - start + 1;
    }
}
