namespace AdventOfCode2023;

public static class Extensions
{
    private static readonly string[] Splitters = ["\r\n", "\n"];
    public static string[] SplitLines(this string s) => s.Split(Splitters, StringSplitOptions.TrimEntries);
    public static string GetInputPath(this Type t, int part) => Path.GetFullPath(Path.Combine($"{t.Name}_{part}"));
}