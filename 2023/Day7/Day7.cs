// https://adventofcode.com/2023/day/7
// --- Day 7: Camel Cards ---

using System.Linq.Expressions;

public class Day7
{
    public static List<Hand> Hands = new List<Hand>();
    public static bool JokersWild = false;

    public static void CamelCards()
    {
        Console.WriteLine(" --- Day 7 ---");
        ParseInput();

        var ordered = Hands
            .OrderBy(x => x.HandType)
            .ThenBy(x => x)
            .ToList();

        Console.WriteLine($"Total winnings are {Hands.Sum(x => x.Bid * (ordered.IndexOf(x) + 1))}");
        
        JokersWild = true;
        ordered = Hands
            .OrderBy(x => x.HandType)
            .ThenBy(x => x)
            .ToList();

        Console.WriteLine($"Total winnings (with jokers) are {Hands.Sum(x => x.Bid * (ordered.IndexOf(x) + 1))}");
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

        private bool IsFiveOfAKind() => !Cards.Distinct().Skip(1).Any();
        private bool IsFourOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 4);
        private bool IsFullHouse() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 2;
        private bool IsThreeOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 3;
        private bool IsTwoPair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 3;
        private bool IsOnePair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 4;
        private bool IsHighCard() => Cards.Distinct().Count() == Cards.Count;

        private HandType GetHandType()
        {
            var handType = HandType.HighCard;

            var numJokers = Cards.Count(x => x.Equals("J"));

            if (IsFiveOfAKind())
            {
                handType = HandType.FiveOfAKind;
            }
            else if (IsFourOfAKind())
            {
                handType = HandType.FourOfAKind;

                if (!JokersWild || numJokers <= 0) return handType;

                handType = HandType.FiveOfAKind;
            }
            else if (IsFullHouse())
            {
                handType = HandType.FullHouse;

                if (!JokersWild || numJokers <= 0) return handType;

                handType = HandType.FiveOfAKind;
            }
            else if (IsThreeOfAKind())
            {
                handType = HandType.ThreeOfAKind;

                if (!JokersWild || numJokers <= 0) return handType;

                handType = HandType.FourOfAKind;
            }
            else if (IsTwoPair())
            {
                handType = HandType.TwoPair;

                if (!JokersWild || numJokers <= 0) return handType;

                handType = numJokers switch
                {
                    1 => HandType.FullHouse,
                    2 => HandType.FourOfAKind,
                    _ => handType
                };
            }
            else if (IsOnePair())
            {
                handType = HandType.OnePair;

                if (!JokersWild || numJokers <= 0) return handType;

                handType = numJokers switch
                {
                    1 => HandType.ThreeOfAKind,
                    2 => HandType.ThreeOfAKind,
                    _ => handType
                };
            }
            else
            {
                if (!JokersWild || numJokers <= 0) return handType;

                handType = HandType.OnePair;
            }

            return handType;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            var hand = obj as Hand;
            if (hand == null) throw new ArgumentException();

            for (int i = 0; i < Cards.Count; i++)
            {
                if (hand.Cards[i] == Cards[i]) continue;

                int compare = MapCardToNumber(Cards[i]).CompareTo(MapCardToNumber(hand.Cards[i]));

                if (compare == 0) continue;

                return compare;
            }

            return 0;
        }

        private int MapCardToNumber(string card)
        {
            return card switch
            {
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                "8" => 8,
                "9" => 9,
                "T" => 10,
                "J" => JokersWild ? 1 : 11,
                "Q" => 12,
                "K" => 13,
                "A" => 14,
                _ => 0
            };
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
