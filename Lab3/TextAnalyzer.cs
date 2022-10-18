namespace Lab3;
using Lab2;

internal class TextAnalyzer : IAnalyzer
{
    public bool IsAutomatDescriptionCorrect(List<string> inputLines)
    {
        foreach (var line in inputLines)
            if (!IsDescriptionLineCorrect(line)) 
                return false;

        return true;
    }

    public bool IsDescriptionLineCorrect(string analyzingLine)
    {
        analyzingLine = analyzingLine.Replace(" ", "");
        if (analyzingLine[1] != '>') return false;

        return true;
    }

    public string ReadState(string analyzingLine, ref int index)
    {
        throw new NotImplementedException();
    }
}

