namespace Lab2;
public class FileManager
{
    private readonly FileInfo _fileInfo;
    public List<string>? FileLines;

    public FileManager(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found at path {path}");

        _fileInfo = new FileInfo(path);
    }

    public void ReadFileByLines()
    {
        if (!File.Exists(_fileInfo.FullName))
            throw new FileNotFoundException($"File not found at path {_fileInfo.FullName}");

        var stringArray = File.ReadAllLines(_fileInfo.FullName);
        FileLines = new List<string>(stringArray);
    }

    public void PrintFileByLines()
    {
        foreach(var line in FileLines)
        {
            Console.WriteLine(line);
        }
    }
}

