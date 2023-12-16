namespace AdventOfCode2023;

public static class Day_09
{
    public static long Part1(string input)
    {
        //input = """
        //    0 3 6 9 12 15
        //    1 3 6 10 15 21
        //    10 13 16 21 30 45
        //    """;

        return input.SplitLines()
            .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray())
            .Select(GetNextValue)
            .Sum();

        static long GetNextValue(long[] values)
        {
            var next = new long[values.Length - 1];
            for (int i = 0; i < next.Length; ++i)
                next[i] = values[i + 1] - values[i];

            if (next.All(x => x == 0))
                return values[^1];

            return values[^1] + GetNextValue(next);
        }
    }
    public static long Part2(string input)
    {
        //input = """
        //    10  13  16  21  30  45
        //    """;

        return input.SplitLines()
            .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray())
            .Select(GetNextValue)
            .Sum();

        static long GetNextValue(long[] values)
        {
            var next = new long[values.Length - 1];
            for (int i = 0; i < next.Length; ++i)
                next[i] = values[i] - values[i + 1];

            if (next.All(x => x == 0))
                return values[0];

            return values[0] + GetNextValue(next);
        }
    }
}