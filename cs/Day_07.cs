namespace AdventOfCode2023;
public static class Day_07
{
    public static long Part1(string input)
    {
        //input = """
        //    32T3K 765
        //    T55J5 684
        //    KK677 28
        //    KTJJT 220
        //    QQQJA 483
        //    """;

        var getWeight = GetWeightFunc("23456789TJQKA");
        return input.SplitLines()
            .Select(line => ParseLine(line, getWeight, hasJokers: false))
            .OrderBy(x => x.Rank).ThenBy(x => x.Weight)
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
    }
    public static long Part2(string input)
    {
        //input = """
        //    32T3K 765
        //    T55J5 684
        //    KK677 28
        //    KTJJT 220
        //    QQQJA 483
        //    """;

        var getWeight = GetWeightFunc("J23456789TQKA");
        return input.SplitLines()
            .Select(line => ParseLine(line, getWeight, hasJokers: true))
            .OrderBy(x => x.Rank).ThenBy(x => x.Weight)
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
    }

    private enum Rank
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    };

    private static Func<char, int, int> GetWeightFunc(string order) => (card, index) => order.IndexOf(card) << (4 * (5 - index));

    private static (string Hand, Rank Rank, int Weight, int Bid) ParseLine(string line, Func<char, int, int> getWeight, bool hasJokers)
    {
        var parts = line.Split(' ');
        var hand = parts[0];
        var bid = int.Parse(parts[1]);

        Rank rank = Rank.FiveOfAKind;
        var handWithoutJokers = hasJokers ? hand.Replace("J", "") : hand;
        var jokers = hand.Length - handWithoutJokers.Length;

        if (jokers < 5)
        {
            var groups =
                  handWithoutJokers
                  .GroupBy(x => x)
                  .Select(x => x.Count())
                  .OrderByDescending(x => x)
                  .ToArray();

            groups[0] += jokers;
            rank = groups switch
            {
            [5, ..] => Rank.FiveOfAKind,
            [4, ..] => Rank.FourOfAKind,
            [3, 2, ..] => Rank.FullHouse,
            [3, ..] => Rank.ThreeOfAKind,
            [2, 2, ..] => Rank.TwoPair,
            [2, ..] => Rank.OnePair,
            [..] => Rank.HighCard,
            };
        }

        var weight = hand.Select(getWeight).Sum();

        return (hand, rank, weight, bid);
    }
}