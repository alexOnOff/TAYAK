namespace Lab2;


public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<char> _alphabet = new List<char>() { 'a','a','b'};
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "test1.txt"));
        fileManager.ReadFileByLines();
        //fileManager.PrintFileByLines();

        PushdownAutomat pushdownAutomat = new(fileManager.FileLines, _alphabet);

        
    }
}