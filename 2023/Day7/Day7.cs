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

        
    }

    private static void ParseInput()
    {
        var lines = System.IO.File.ReadLines(@"Day7/example_input.txt");

        foreach (var line in lines)
        {
            var hand = new Hand();

            hand.Bid = int.Parse(line.Split(' ')[1]);
            hand.Cards = line.Split(' ')[0].Select(x => x.ToString()).ToList();

            Console.Write($"{line.Split(' ')[0]}: ");

            if (hand.IsFiveOfAKind())
            {
                Console.Write("five of a kind");
            }
            else if (hand.IsFourOfAKind())
            {
                Console.Write("four of a kind");
            }
            else if (hand.IsFullHouse())
            {
                Console.Write("full house");
            }
            else if (hand.IsThreeOfAKind())
            {
                Console.Write("three of a kind");
            }
            else if (hand.IsTwoPair())
            {
                Console.Write("two pair");
            }
            else if (hand.IsOnePair())
            {
                Console.Write("one pair");
            }
            else if (hand.IsHighCard())
            {
                Console.WriteLine("high card");
            }

            Console.WriteLine();
        }
    }

    public class Hand
    {
        public List<string> Cards = new List<string>();

        public int Bid;

        public Hand()
        {
            
        }

        public bool IsFiveOfAKind() => !Cards.Distinct().Skip(1).Any();
        public bool IsFourOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 4);
        public bool IsFullHouse() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 2;
        public bool IsThreeOfAKind() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 3) && Cards.Distinct().Count() == 3;
        public bool IsTwoPair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 3;
        public bool IsOnePair() => Cards.Any(x => Cards.Count(y => y.Equals(x)) == 2) && Cards.Distinct().Count() == 4;
        public bool IsHighCard() => Cards.Distinct().Count() == Cards.Count;
    }
}
