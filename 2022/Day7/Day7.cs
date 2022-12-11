// https://adventofcode.com/2022/day/7
// --- Day 7: No Space Left On Device ---

public class Day7
{
    private static string filename = @"Day7/input.txt";

    static Directory root;
    
    public static void NoSpaceLeftOnDevice()
    {
      Console.WriteLine(" --- Day 7---");

      root = new Directory("/");
      ParseInput();

      var subDirectories = GetAllSubDirectories(root).Where(x => x.GetTotalSize() <= 100000);

      int totalSize = 0;
      foreach (var subDir in subDirectories)
      {
        totalSize += subDir.GetTotalSize();
      }

      Console.WriteLine($"Total directory size to delete is {totalSize}");
    }

    private static List<Directory> GetAllSubDirectories(Directory root)
    {
      var dirs = new List<Directory>();

      foreach (var directory in root.Subdirectories)
      {
        dirs.Add(directory);
        dirs.AddRange(GetAllSubDirectories(directory));
      }

      return dirs;
    }

    private static void ParseInput()
    {
      Directory currentDir = root; 

      foreach (var line in System.IO.File.ReadLines(filename))
      {
        // a command we entered
        if (line.StartsWith('$')) ParseUserCommands(line, ref currentDir);
        else ParseOutput(line, ref currentDir);
      }
    } 

    private static void ParseUserCommands(string line, ref Directory currentDir)
    {
      if (line.StartsWith("$ ls")) return;

      // Changing into a directory
      if (line.StartsWith("$ cd"))
      {
        var split = line.Split(' ');
        var dirName = split[2];

        if (dirName == "/") 
        {
          currentDir = root;
          return;
        }

        if (dirName == "..")
        {
          currentDir = currentDir.Parent;
          return;
        }

        if (currentDir.Subdirectories.Any(x => x.Name == dirName))
        {
          currentDir = currentDir.Subdirectories.First(x => x.Name == dirName);
        }
        else
        {
          var newDir = new Directory(dirName, currentDir);
          currentDir.Subdirectories.Add(newDir);
          currentDir = newDir;
        }
      }
    }

    private static void ParseOutput(string line, ref Directory currentDir)
    {
      var split = line.Split(' ');

      if (split[0] == "dir")
      {
        currentDir.Subdirectories.Add(new Directory(split[1], currentDir));
      }
      else
      {
        currentDir.Files.Add(new File(split[1], Convert.ToInt32(split[0])));
      }
    }
}

public class Directory 
{
  public string Name;

  public Directory Parent;
  public List<Directory> Subdirectories;
  public List<File> Files;

  public Directory(string name, Directory parent = null)
  { 
    Name = name;
    Subdirectories = new List<Directory>();
    Files = new List<File>();
    Parent = parent;
  }

  public int GetTotalSize()
  {
    var size = Files.Sum(x => x.Size);

    foreach (var subDir in Subdirectories)
    {
      size += subDir.GetTotalSize();
    }

    return size;
  }
}

public class File 
{
  public string Name;
  public int Size;

  public File(string name, int size)
  {
    Name = name;
    Size = size;
  }
}