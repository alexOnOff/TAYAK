namespace Lab3;
using Lab2;


public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<string> _alphabet = new() { "a", "+", "*", ")", "(", "a", "E", "m", "T", "!", "P", "\\", "/", "R", "S", "C", "-", "b", "c", "0", ">"};
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "test1.txt"));
        fileManager.ReadFileByLines();
        var stack = new List<string>() { "h0", "E"};

        PushdownAutomat pushdownAutomat = new(fileManager.FileLines, _alphabet);
        if (pushdownAutomat.IsPAExecutable("!/abc/", stack)) Console.WriteLine("Pushdown automata is executable!");
        else Console.WriteLine("Pushdown automata is NOT executable!");
    }
}