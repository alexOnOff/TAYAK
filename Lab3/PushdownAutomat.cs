namespace Lab2;

internal class PushdownAutomat
{
    HashSet<string> States = new();
    HashSet<string> Alphabet = new();
    Stack<string> Stack = new();
    string _startState = "E";
    HashSet<TransitionFunction> TransitionFunctions = new();
    string _stackButton = "h0";

    public PushdownAutomat()
    {
        Stack.Push(_stackButton);


    }

    private void AddTransitionFunc()
    {
        //TransitionFunctions.Add(new TransitionFunction());

    }


}

