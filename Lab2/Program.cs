namespace Lab2;

public static class Program
{
    private static readonly string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<char> Alphabet = new List<char>() { '\\', '/','a','+', 'd', '\"', 'c' ,'e', 'f', 'g','b' , '8', '*', '0', '1' };
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(directory, "var2.txt"));

        fileManager.ReadFileByLines();

        CodeAnalyzer codeAnalyzer = new(Alphabet);
        if (!codeAnalyzer.IsAutomatDescriptionCorrect(fileManager.FileLines))
            return;

        Automate automate = new Automate(fileManager.FileLines, Alphabet);

        if (automate.IsAutomateDeterministic())
            Console.WriteLine("Automate is Deterministic.\n");
        else 
            Console.WriteLine("Automate is not Deterministic.\n");
        
        automate.PrintTransitionFunctions();
        automate.Determization();
        automate.PrintTransitionFunctions();

        if (automate.IsExecutableForInputLine("aaaaaaaab"))
            Console.WriteLine("Is executable");
        else
            Console.WriteLine("Is NOT executable");

        
    }
}


