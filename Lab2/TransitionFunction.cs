namespace Lab2;

public class TransitionFunction
{
    public string   CurrentState;
    public char     Symbol;
    public string   NextState;

    public TransitionFunction(string curState, char symbol, string nextState)
    {
        CurrentState = curState;
        Symbol = symbol;
        NextState = nextState;
    }

    public bool Equals(TransitionFunction transitionFunction2)
    {
        if (CurrentState == transitionFunction2.CurrentState &&
            Symbol == transitionFunction2.Symbol &&
            NextState == transitionFunction2.NextState) return true;
        return false;
    }
}
