namespace Lab2;

internal class TransitionFunction
{
    string CurrentState;
    char InputSymbol;
    char StackSymbol;

    string NextState;
    string StackOutputSymbols;

    public TransitionFunction(string _curState, char _inpSymbol, char _stackSymbol, string _sextState, string _stackOutSymbols)
    {
        CurrentState = _curState;
        InputSymbol = _inpSymbol;
        StackSymbol = _stackSymbol;
        NextState = _sextState;
        StackOutputSymbols = _stackOutSymbols;
    }

    public bool Equals(TransitionFunction transitionFunction2)
    {
        if (CurrentState == transitionFunction2.CurrentState &&
            InputSymbol == transitionFunction2.InputSymbol &&
            NextState == transitionFunction2.NextState &&
            StackSymbol == transitionFunction2.StackSymbol &&
            StackOutputSymbols == transitionFunction2.StackOutputSymbols) return true;
        return false;
    }
}

