using System.Collections.Generic;

namespace Lab4_5.Automat
{
    internal class TransitionFunction
    {

        private readonly string InputSymbol;
        public string GetInputSymbol { get { return InputSymbol; } }

        private readonly string NonTerminal;
        public string GetNonTerminal { get { return NonTerminal; } }


        private readonly List<string> StackOutput = new();
        public List<string> GetStackOutput { get { return StackOutput; } }

        private readonly bool IsSync;
        public bool GetIsSync { get { return IsSync; } }

        public TransitionFunction( string _inpSymbol, string _stackSymbol, List<string> _stackOutSymbols)
        {
            InputSymbol = _inpSymbol;
            NonTerminal = _stackSymbol;
            StackOutput = _stackOutSymbols;
            IsSync = false;
        }

        public TransitionFunction(bool isSync)
        {
            InputSymbol = null;
            NonTerminal = null;
            StackOutput = null;
            IsSync = isSync;
        }

    }
}
