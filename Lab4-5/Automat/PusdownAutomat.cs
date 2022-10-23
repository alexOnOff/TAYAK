using System.Collections.Generic;

namespace Lab4_5.Automat
{
    internal class PusdownAutomat
    {
        public HashSet<string> States = new();
        public HashSet<string> Alphabet = new();
        public HashSet<string> StackAlphabet = new();
        public Stack<string> Stack = new();
        public string _startState = "s0";
        public HashSet<string> FinalStates = new() { "s0" };
        public HashSet<TransitionFunction> TransitionFunctions = new();
        public string _stackButton = "h0";
    }
}
