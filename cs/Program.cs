using AdventOfCode2023;
using System.Reflection;

var last = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.StartsWith("Day_")).Last();
ShowResult(last);
return;

foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.StartsWith("Day_")))
{
    ShowResult(type);
}

static void ShowResult(Type type)
{
    var part1 = type.GetMethod("Part1")?.Invoke(null, [type.GetInputContent()]);
    var part2 = type.GetMethod("Part2")?.Invoke(null, [type.GetInputContent()]);
    Console.WriteLine($"{type.Name} ({part1}, {part2})");
}