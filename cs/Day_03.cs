namespace AdventOfCode2023;

public static class Day_03
{
    public static int Part1(string input)
    {
        //input = """
        //    467..114..
        //    ...*......
        //    ..35..633.
        //    ......#...
        //    617*......
        //    .....+.58.
        //    ..592.....
        //    ......755.
        //    ...$.*....
        //    .664.598..
        //    """;

        var cols = input.IndexOfAny(['\r', '\n']);
        input = input.ReplaceLineEndings("");
        var rows = input.Length / cols;

        var value = 0;

        for (int i = 0; i < rows; ++i)
        {
            var row = GetRow(input, i, cols);
            for (int j = 0; j < row.Length; ++j)
            {
                if (Char.IsDigit(row[j]))
                {
                    var start = j;
                    var shouldUse = ShouldUseLeft(input, i, j, cols);

                    while (j < row.Length && Char.IsDigit(row[j]))
                    {
                        shouldUse |= ShouldUseCentre(input, i, j, cols);
                        ++j;
                    }

                    shouldUse |= ShouldUseRight(input, i, j - 1, cols);

                    if (shouldUse)
                        value += int.Parse(row[start..j]);
                }
            }
        }

        return value;

        static ReadOnlySpan<char> GetRow(string src, int row, int len) => src.AsSpan(row * len, len);
        static bool ShouldUse(string src, int row, int col, int len)
        {
            var index = row * len + col;
            if (index < 0 || index >= src.Length)
                return false;
            var c = src[index];
            return c != '.' && !Char.IsDigit(c);
        }
        static bool ShouldUseLeft(string src, int row, int col, int len)
        {
            // top left
            return ShouldUse(src, row - 1, col - 1, len) ||
            // left
            ShouldUse(src, row, col - 1, len) ||
            // bot left
            ShouldUse(src, row + 1, col - 1, len);
        }

        static bool ShouldUseCentre(string src, int row, int col, int len)
        {
            // top
            return ShouldUse(src, row - 1, col, len) ||
            // bot
            ShouldUse(src, row + 1, col, len);
        }

        static bool ShouldUseRight(string src, int row, int col, int len)
        {
            // topRight
            return ShouldUse(src, row - 1, col + 1, len)
            // right
            || ShouldUse(src, row, col + 1, len)
            // botRight
            || ShouldUse(src, row + 1, col + 1, len);
        }
    }

    public static int Part2(string input)
    {
        //input = """
        //    467..114..
        //    ...*......
        //    ..35..633.
        //    ......#...
        //    617*......
        //    .....+.58.
        //    ..592.....
        //    ......755.
        //    ...$.*....
        //    .664.598..
        //    """;

        var cols = input.IndexOfAny(['\r', '\n']);
        input = input.ReplaceLineEndings("");
        var rows = input.Length / cols;

        var gears = new Dictionary<int, List<int>>();

        for (int i = 0; i < rows; ++i)
        {
            var row = GetRow(input, i, cols);
            for (int j = 0; j < row.Length; ++j)
            {
                if (Char.IsDigit(row[j]))
                {
                    int gear = -1;
                    var start = j;
                    var shouldUse = ShouldUseLeft(input, i, j, cols, out gear);

                    while (j < row.Length && Char.IsDigit(row[j]))
                    {
                        shouldUse = shouldUse || ShouldUseCentre(input, i, j, cols, out gear);
                        ++j;
                    }

                    shouldUse = shouldUse || ShouldUseRight(input, i, j - 1, cols, out gear);

                    if (shouldUse)
                    {
                        if (!gears.TryGetValue(gear, out var list))
                            gears[gear] = list = [];
                        list.Add(int.Parse(row[start..j]));
                    }
                }
            }
        }

        return gears.Values.Where(l => l.Count == 2).Select(l => l[0] * l[1]).Sum();

        static ReadOnlySpan<char> GetRow(string src, int row, int len) => src.AsSpan(row * len, len);
        static bool ShouldUse(string src, int row, int col, int len, out int idx)
        {
            idx = row * len + col;
            if (idx < 0 || idx >= src.Length)
                return false;
            var c = src[idx];
            return c == '*';
        }
        static bool ShouldUseLeft(string src, int row, int col, int len, out int idx)
        {
            // top left
            return ShouldUse(src, row - 1, col - 1, len, out idx) ||
            // left
            ShouldUse(src, row, col - 1, len, out idx) ||
            // bot left
            ShouldUse(src, row + 1, col - 1, len, out idx);
        }

        static bool ShouldUseCentre(string src, int row, int col, int len, out int idx)
        {
            // top
            return ShouldUse(src, row - 1, col, len, out idx) ||
            // bot
            ShouldUse(src, row + 1, col, len, out idx);
        }

        static bool ShouldUseRight(string src, int row, int col, int len, out int idx)
        {
            // topRight
            return ShouldUse(src, row - 1, col + 1, len, out idx)
            // right
            || ShouldUse(src, row, col + 1, len, out idx)
            // botRight
            || ShouldUse(src, row + 1, col + 1, len, out idx);
        }
    }
}
