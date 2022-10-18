namespace Lab3;
using Lab2;


public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<string> _alphabet = new() { "a", "+", "*", ")", "(", "a"};
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "test4.txt"));
        fileManager.ReadFileByLines();
        var stack = new List<string>() { "h0", "E"};

        PushdownAutomat pushdownAutomat = new(fileManager.FileLines, _alphabet);
        if (pushdownAutomat.IsPAExecutable("a+a*a", stack)) Console.WriteLine("Pushdown automata is executable!");
        else Console.WriteLine("Pushdown automata is NOT executable!");
    }
}