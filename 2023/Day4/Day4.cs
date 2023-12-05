// https://adventofcode.com/2023/day/4
// --- Day 3: Scratchcards ---

public class Day4
{
  public static List<Card> Cards = new List<Card>();

  public static void Scratchcards()
  {
    Console.WriteLine(" --- Day 4 ---");
    ParseInput();

    Console.WriteLine($"Cards are worth {Cards.Sum(x => x.GetPoints())} points.");
  }

  private static void ParseInput()
  {
    var lines = System.IO.File.ReadLines(@"Day4/input.txt");

    foreach (var line in lines)
    {
      var card = new Card();

      var split = line.Split(':');
      
      var cardLabel = split[0];
      card.CardNumber = Int32.Parse(cardLabel.Split(' ').Where(x => !string.IsNullOrEmpty(x)).Last().Trim());

      var numbersSplit = split[1].Split('|');

      card.WinningNumbers = numbersSplit[0].Split(' ')
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => Int32.Parse(x.Trim()))
        .ToList();

      card.Numbers = numbersSplit[1].Split(' ')
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => Int32.Parse(x.Trim()))
        .ToList();

      Cards.Add(card);
    }
  }

  public class Card
  {
    public int CardNumber { get; set; }
    public List<int> WinningNumbers { get; set; } = new List<int>();
    public List<int> Numbers { get; set; } = new List<int>();
    public int Points { get; set; } = 0;

    public int GetPoints()
    {     
      var numMatches = GetNumberOfMatches();

      if (numMatches == 0) return Points;

      Points = 1;

      for (int i = 0; i < numMatches - 1; i++) 
      {
        Points *= 2;
      }

      return Points;
    }

    public int GetNumberOfMatches()
    {
      return Numbers.Intersect(WinningNumbers).Count();
    }
  }
}
