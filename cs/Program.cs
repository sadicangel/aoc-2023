using AdventOfCode2023;
using System.Reflection;

foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.StartsWith("Day_")))
{
    var part1 = type.GetMethod("Part1")?.Invoke(null, [type.GetInputPath(1)]);
    var part2 = type.GetMethod("Part2")?.Invoke(null, [type.GetInputPath(2)]);
    Console.WriteLine($"{type.Name} ({part1}, {part2})");
}
