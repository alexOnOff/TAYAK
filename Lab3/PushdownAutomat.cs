namespace Lab2;

internal class PushdownAutomat
{
    HashSet<string> States = new();
    HashSet<char> Alphabet = new();
    Stack<string> Stack = new();
    string _startState = "E";
    HashSet<TransitionFunction> TransitionFunctions = new();
    string _stackButton = "h0";

    public PushdownAutomat(List<string> fileLines, List<char> alphabet)
    {
        Stack.Push(_stackButton);

        Alphabet = alphabet.ToHashSet();
        foreach(var line in fileLines)
        {
            
        }


        PrintAlphabet();
    }

    private void AddTransitionFunc()
    {
        //TransitionFunctions.Add(new TransitionFunction());

    }

    private void PrintAlphabet()
    {
        foreach(var v in Alphabet)
        {
            Console.WriteLine(v);
        }
    }

    private void ExecuteDescriptionLine(string inputLine)
    {
        
        
    }


}

