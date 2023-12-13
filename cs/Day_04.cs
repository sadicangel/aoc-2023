using System.Diagnostics;

namespace AdventOfCode2023;

public static class Day_04
{
    public static int Part1(string input)
    {
        //input = """
        //    Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        //    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        //    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        //    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        //    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        //    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        //    """;

        var comparer = Comparer<string>.Create(new Comparison<string>(Compare));

        return input.SplitLines()
            .Select(card =>
            {
                if (card.Split(':', StringSplitOptions.TrimEntries) is not [var cardId, var numbers])
                    throw new UnreachableException();

                if (numbers.Split('|', StringSplitOptions.TrimEntries) is not [var winningSet, var playerSet])
                    throw new UnreachableException();

                var winningNumbers = winningSet.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var playerNumbers = playerSet.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var matches = playerNumbers.Where(winningNumbers.Contains).ToArray();

                var count = matches.Length - 1;

                return count < 0 ? 0 : (int)Math.Pow(2, count);
            })
            .Sum();

        static int Compare(string a, string b) => int.Parse(a).CompareTo(int.Parse(b));
    }

    public static int Part2(string input)
    {
        //input = """
        //    Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        //    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        //    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        //    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        //    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        //    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        //    """;

        var list = new List<int>();

        var cards = input.SplitLines()
            .Select(card =>
            {
                if (card.Split(':', StringSplitOptions.TrimEntries) is not [var cardId, var numbers])
                    throw new UnreachableException();

                if (numbers.Split('|', StringSplitOptions.TrimEntries) is not [var winningSet, var playerSet])
                    throw new UnreachableException();

                var winningNumbers = winningSet.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var playerNumbers = playerSet.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var matches = playerNumbers.Count(winningNumbers.Contains);

                var id = int.Parse(cardId[4..]);
                var copies = Enumerable.Range(id + 1, matches).ToList();

                return (id, copies);
            })
            .ToDictionary(e => e.id, e => e.copies);

        var count = cards.Count;

        var queue = new Queue<int>(cards.Values.SelectMany(x => x));
        while (queue.Count > 0)
        {
            var card = queue.Dequeue();
            foreach (var copy in cards[card])
                queue.Enqueue(copy);
            count++;
        }

        return count;
    }
}