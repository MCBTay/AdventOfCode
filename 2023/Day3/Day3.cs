// https://adventofcode.com/2023/day/3
// --- Day 3: Gear Ratios ---

public class Day3
{
  public static List<Symbol> Symbols = new List<Symbol>();
  public static List<PartNumber> PartNumbers = new List<PartNumber>();

  public static void GearRatios()
  {
    Console.WriteLine(" --- Day 3 ---");
    ParseInput();

    var sum = 0;
    foreach (var partNumber in PartNumbers)
    {
      var potentialSymbols = Symbols.Where(x => Math.Abs(x.Position.Row - partNumber.Positions.First().Row) <= 1);

      if (potentialSymbols == null) continue;
      
      sum += GetPartNumberIfNearSymbol(partNumber, potentialSymbols);
    }

    Console.WriteLine($"Sum of part numbers is {sum}.");

    CalculateGearRatios();
  }

  private static void CalculateGearRatios()
  {
    var gears = Symbols.Where(x => x.Character == '*');
    var sumGearRatios = 0;
    foreach (var gear in gears)
    {
      var partNumbers = PartNumbers
        .Where(x => Math.Abs(x.Positions.First().Row - gear.Position.Row) <= 1 && x.NearSymbol(gear));

      if (partNumbers.Count() != 2) continue;

      var gearRatio = partNumbers.First().Number * partNumbers.Last().Number;
      sumGearRatios += gearRatio;
    }

    Console.WriteLine($"Sum of gear ratios is {sumGearRatios}.");
  }

  private static int GetPartNumberIfNearSymbol(PartNumber partNumber, IEnumerable<Symbol> potentialSymbols)
  {
    foreach (var symbol in potentialSymbols)
    {
      if (partNumber.NearSymbol(symbol))
      {
        return partNumber.Number;
      }
    }

    return 0;
  }

  private static void ParseInput()
  {
    var lines = System.IO.File.ReadLines(@"Day3/input.txt");

    for (int i = 0; i < lines.Count(); i++)
    {
      var line = lines.ElementAt(i);
      
      ParseSymbols(line, i);

      var characters = line.Select(c => char.IsNumber(c) ? c : '.').ToArray();             
      var output = new string(characters);

      var split = output.Split('.');
      var previousSection = string.Empty;
      for (int j = 0; j < split.Count(); j++)
      {
        var section = split[j];

        if (string.IsNullOrEmpty(section)) continue;

        var strippedSection = section.Where(x => char.IsDigit(x)).ToArray();

        var previousIndex = !string.IsNullOrEmpty(previousSection) ? line.IndexOf(previousSection) + previousSection.Count() : 0;
        var positions = new List<Position>();

        positions.Add(new Position(i, line.IndexOf(section, previousIndex)));
        for (int k = 1; k < strippedSection.Count(); k++)
        {
          positions.Add(new Position(i, line.IndexOf(section, previousIndex) + k));
        }

        if (!Int32.TryParse(strippedSection, out var partNumber)) continue;
        previousSection = new string(strippedSection);

        PartNumbers.Add(new PartNumber(partNumber, positions));
      }
    }
  }

  private static void ParseSymbols(string line, int row)
  {
    for (int col = 0; col < line.Count(); col++)
    {
      var character = line[col];

      if (!char.IsLetterOrDigit(character) && character != '.')
      {
        Symbols.Add(new Symbol(character, new Position(row, col)));
      }
    }
  }

  public class Symbol
  {
    public char Character;
    public Position Position;

    public Symbol(char character, Position position)
    {
      Character = character;
      Position = position;
    }
  }

  public class PartNumber
  {
    public int Number;
    public List<Position> Positions = new List<Position>();

    public PartNumber(int number, List<Position> positions)
    {
      Number = number;
      Positions = positions;
    }

    public bool NearSymbol(Symbol symbol)
    {    
      foreach (var position in Positions)
      {
        if (Math.Abs(position.Col - symbol.Position.Col) > 1) continue;
        return true;
      }

      return false;
    }
  }

  public class Position 
  {
    public int Row;
    public int Col;

    public Position (int row, int col)
    {
      Row = row;
      Col = col;
    }
  }
}
