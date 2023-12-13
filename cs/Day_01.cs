namespace AdventOfCode2023;
public static class Day_01
{
    public static int Part1(string inputPath)
    {
        return File.ReadLines(inputPath).Select(FindCalibrationValues).Sum();

        static int FindCalibrationValues(string line)
        {
            bool sFound = false, eFound = false;
            int s = 0, e = line.Length - 1;
            while (!sFound || !eFound)
            {
                if (!sFound)
                {
                    if (Char.IsDigit(line[s]))
                        sFound = true;
                    else
                        s++;
                }

                if (!eFound)
                {
                    if (Char.IsDigit(line[e]))
                        eFound = true;
                    else
                        e--;
                }
            }

            return int.Parse(stackalloc char[2] { line[s], line[e] });
        }
    }

    private static readonly string[] Numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

    public static int Part2(string inputPath)
    {
        return File.ReadLines(inputPath).Select(FindCalibrationValues).Sum();

        static int FindCalibrationValues(string line)
        {
            int sn = -1, en = -1;
            int s = 0, e = line.Length - 1;
            while (sn == -1 || en == -1)
            {
                if (sn == -1)
                {
                    if (TryFindNumber(line.AsSpan(s), out var sf))
                    {
                        sn = sf;
                    }
                    else if (Char.IsDigit(line[s]))
                    {
                        sn = line[s] - '0';
                    }
                    else
                    {
                        s++;
                    }
                }

                if (en == -1)
                {
                    if (TryFindNumber(line.AsSpan(e), out var ef))
                    {
                        en = ef;
                    }
                    else if (Char.IsDigit(line[e]))
                    {
                        en = line[e] - '0';
                    }
                    else
                    {
                        e--;
                    }
                }
            }

            return sn * 10 + en;
        }

        static bool TryFindNumber(ReadOnlySpan<char> input, out int number)
        {
            for (number = 1; number <= Numbers.Length; ++number)
                if (input.StartsWith(Numbers[number - 1]))
                    return true;
            return false;
        }
    }
}
