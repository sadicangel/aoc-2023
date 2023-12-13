using System.Diagnostics;

namespace AdventOfCode2023;

public static class Day_02
{
    public static int Part1(string input)
    {
        //input = """
        //    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        //    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        //    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        //    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        //    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        //    """;
        return input.SplitLines().Select(IsPossibleGame).Sum();

        static int IsPossibleGame(string line)
        {
            if (line.Split(':', StringSplitOptions.TrimEntries) is not [var gameId, var subsets])
                throw new UnreachableException();

            foreach (var subset in subsets.Split(';', StringSplitOptions.TrimEntries))
            {
                foreach (var cubes in subset.Split(',', StringSplitOptions.TrimEntries))
                {
                    if (cubes.Split(' ', StringSplitOptions.TrimEntries) is not [var count, var colour])
                        throw new UnreachableException();

                    if (int.Parse(count) > GetMaxFor(colour))
                        return 0;
                }
            }

            return int.Parse(gameId[5..]);
        }

        static int GetMaxFor(string colour) => colour switch { "red" => 12, "green" => 13, "blue" => 14, _ => throw new UnreachableException() };
    }

    public static int Part2(string input)
    {
        //input = """
        //    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        //    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        //    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        //    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        //    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        //    """;
        return input.SplitLines().Select(IsPossibleGame).Sum();

        static int IsPossibleGame(string line)
        {
            if (line.Split(':', StringSplitOptions.TrimEntries) is not [var gameId, var subsets])
                throw new UnreachableException();

            int red = 0, green = 0, blue = 0;

            foreach (var subset in subsets.Split(';', StringSplitOptions.TrimEntries))
            {
                foreach (var cubes in subset.Split(',', StringSplitOptions.TrimEntries))
                {
                    if (cubes.Split(' ', StringSplitOptions.TrimEntries) is not [var count, var colour])
                        throw new UnreachableException();

                    switch (colour)
                    {
                        case "red":
                            red = int.Max(red, int.Parse(count));
                            break;
                        case "green":
                            green = int.Max(green, int.Parse(count));
                            break;
                        case "blue":
                            blue = int.Max(blue, int.Parse(count));
                            break;
                        default:
                            throw new UnreachableException();
                    }
                }
            }

            return red * green * blue;
        }
    }
}
