namespace Lab2;


public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "test1.txt"));
        fileManager.ReadFileByLines();
        fileManager.PrintFileByLines();

        
    }
}