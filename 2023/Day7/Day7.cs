// https://adventofcode.com/2023/day/7
// --- Day 7: Camel Cards ---

using System.Linq.Expressions;

public class Day7
{
    public static List<Hand> Hands = new List<Hand>();

    public static void CamelCards()
    {
        Console.WriteLine(" --- Day 7 ---");
        ParseInput();

        var ordered = Hands
            .OrderBy(x => x.HandType)
            .ThenBy(x => x)
            .ToList();

        Console.WriteLine($"Total winnings are {Hands.Sum(x => x.Bid * (ordered.IndexOf(x) + 1))}");
    }

    private static void ParseInput()
    {
        var lines = System.IO.File.ReadLines(@"Day7/input.txt");

        foreach (var line in lines)
        {
            var hand = new Hand
            {
                Bid = int.Parse(line.Split(' ')[1]),
                Cards = line.Split(' ')[0].Select(x => x.ToString()).ToList(),
                CardsString = line.Split(' ')[0]
            };

            Hands.Add(hand);
        }
    }

    public class Hand : IComparable
    {
        public List<string> Cards = new List<string>();

        public string CardsString;

        public int Bid;

        public int Rank = 1;

        public HandType HandType => GetHandType();

        public Hand()
        {
            
        }

        private bool IsFiveOfAKind() => !Cards.Distinct().Skip(1).Any();
        private bool IsFourOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 4);
        private bool IsFullHouse() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 2;
        private bool IsThreeOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 3;
        private bool IsTwoPair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 3;
        private bool IsOnePair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 4;
        private bool IsHighCard() => Cards.Distinct().Count() == Cards.Count;

        private HandType GetHandType()
        {
            if (IsFiveOfAKind())
            {
                return HandType.FiveOfAKind;
            }
            else if (IsFourOfAKind())
            {
                return HandType.FourOfAKind;
            }
            else if (IsFullHouse())
            {
                return HandType.FullHouse;
            }
            else if (IsThreeOfAKind())
            {
                return HandType.ThreeOfAKind;
            }
            else if (IsTwoPair())
            {
                return HandType.TwoPair;
            }
            else if (IsOnePair())
            {
                return HandType.OnePair;
            }
            else if (IsHighCard())
            {
                return HandType.HighCard;
            }

            return HandType.HighCard;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            var hand = obj as Hand;
            if (hand == null) throw new ArgumentException();

            for (int i = 0; i < Cards.Count; i++)
            {
                if (hand.Cards[i] == Cards[i]) continue;

                var compare = MapCardToNumber(Cards[i]).CompareTo(MapCardToNumber(hand.Cards[i]));
                if (compare == 0) continue;

                return compare;
            }

            return 0;
        }

        private int MapCardToNumber(string card)
        {
            switch (card)
            {
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "T": return 10;
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                case "A": return 14;
            }

            return 0;
        }
    }

    public enum HandType
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }
}
