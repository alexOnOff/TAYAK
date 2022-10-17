namespace Lab2;


public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<string> _alphabet = new() { "a", "+", "*", ")", "(", "a"};
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "test.txt"));
        fileManager.ReadFileByLines();
        var stack = new List<string>() { "h0", "E"};
        //fileManager.PrintFileByLines();

        PushdownAutomat pushdownAutomat = new(fileManager.FileLines, _alphabet);
        pushdownAutomat.IsPAExecutable("a+a*a", stack);



    }
}