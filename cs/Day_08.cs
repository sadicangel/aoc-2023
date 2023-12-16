using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode2023;

public static class Day_08
{
    public static long Part1(string input)
    {
        //input = """
        //    RL

        //    AAA = (BBB, CCC)
        //    BBB = (DDD, EEE)
        //    CCC = (ZZZ, GGG)
        //    DDD = (DDD, DDD)
        //    EEE = (EEE, EEE)
        //    GGG = (GGG, GGG)
        //    ZZZ = (ZZZ, ZZZ)
        //    """;

        var lines = input.SplitLines();

        var sequence = lines[0];

        var map = lines[1..].Select(line =>
        {
            var key = line[0..3];
            var l = line[7..10];
            var r = line[12..15];
            return (key, l, r);
        })
            .ToDictionary(k => k.key, k => new Choice(k.l, k.r));

        var node = map["AAA"];

        for (int i = 0, steps = 1; i < sequence.Length; i = (i + 1) % sequence.Length, steps++)
        {
            var next = sequence[i] is 'L' ? node.Left : node.Right;
            if (next == "ZZZ")
                return steps;
            node = map[next];
        }

        throw new UnreachableException();
    }

    public static long Part2(string input)
    {
        //input = """
        //    LR

        //    11A = (11B, XXX)
        //    11B = (XXX, 11Z)
        //    11Z = (11B, XXX)
        //    22A = (22B, XXX)
        //    22B = (22C, 22C)
        //    22C = (22Z, 22Z)
        //    22Z = (22B, 22B)
        //    XXX = (XXX, XXX)
        //    """;

        var lines = input.SplitLines();

        var sequence = lines[0];

        var map = lines[1..].Select(line =>
        {
            var key = line[0..3];
            var l = line[7..10];
            var r = line[12..15];
            return (key, l, r);
        })
            .ToDictionary(k => k.key, k => new Choice(k.l, k.r));

        var keys = map.Where(e => e.Key is [_, _, 'A']).ToList();
        var nodes = keys.Select(e => e.Value).ToArray();
        var next = new string[nodes.Length];
        var steps = new long[nodes.Length];
        Array.Fill(steps, 1);

        for (int i = 0; i < sequence.Length; i = (i + 1) % sequence.Length)
        {
            Func<Choice, string> project = sequence[i] is 'L' ? n => n.Left : n => n.Right;
            for (int k = 0; k < nodes.Length; ++k)
            {
                if (next[k] is not [_, _, 'Z'])
                    next[k] = project(nodes[k]);
            }
            if (next.All(s => s is [_, _, 'Z']))
                break;
            for (int k = 0; k < nodes.Length; ++k)
            {
                if (next[k] is not [_, _, 'Z'])
                {
                    nodes[k] = map[next[k]];
                    steps[k]++;
                }
            }
        }

        return LeastCommonMultiple(steps);
    }

    private readonly record struct Choice(string Left, string Right);

    private static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        => a / GreatestCommonDivisor(a, b) * b;

    private static T LeastCommonMultiple<T>(IEnumerable<T> values) where T : INumber<T>
        => values.Aggregate(LeastCommonMultiple);
}