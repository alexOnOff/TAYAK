namespace Lab2;

public class CodeAnalyzer : IAnalyzer
{
    private List<char> _alphabet = new();

    public CodeAnalyzer(List<char> alphabet)
    {
        _alphabet = alphabet;
    }

    public bool IsAutomatDescriptionCorrect(List<string> inputLines)
    {
        foreach (var line in inputLines)
            if (!IsDescriptionLineCorrect(line))
                return false;

        return true;
    }

    public bool IsDescriptionLineCorrect(string analyzingLine)
    {
        var i = 0;

        ReadState(analyzingLine, ref i);

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter ',' and symbol of transmission and output state. ");
            return false;
        }

        if (analyzingLine[i] != ',')
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }
        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter symbol of transmission and output state.");
            return false;
        }

        if (!_alphabet.Contains(analyzingLine[i]))
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }
        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter '=state'");
            return false;
        }


        if (analyzingLine[i] != '=')
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }
        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter output state");
            return false;
        }

        ReadState(analyzingLine, ref i);

        if (i < analyzingLine.Length)
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }

        return true;
    }

    public string ReadState(string analyzingLine, ref int index)
    {
        var state = "";
        while (char.IsDigit(analyzingLine[index]) || char.IsLetter(analyzingLine[index]))
        {
            state += analyzingLine[index];
            index++;
            if (analyzingLine.Length == index) break;
        }
        return state;
    }
}

