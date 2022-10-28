using System.Collections.Generic;

namespace Lab4_5.Automat;

internal class PusdownAutomat
{

    public HashSet<string> Alphabet = new();
    public HashSet<string> StackAlphabet = new();
    public Stack<string> Stack = new();

    public HashSet<TransitionFunction> TransitionFunctions = new();
    public HashSet<GrammarRule> GrammarRules = new();
    public string _stackButton = "$";

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet)
    {
        Alphabet = alpabet;
        StackAlphabet = stackAlphabet;
    }

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet, string grammarText)
    {
        Alphabet = alpabet;
        StackAlphabet = stackAlphabet;
        SetGrammarRules(grammarText);
    }

    private HashSet<GrammarRule> SetGrammarRules(string grammarText)
    {
        HashSet<GrammarRule> GR = new();


        return GR;
    }


}


