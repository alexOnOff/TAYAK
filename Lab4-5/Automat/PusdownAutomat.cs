using System.Collections.Generic;

namespace Lab4_5.Automat;

internal class PusdownAutomat
{

    public HashSet<string> NonTerminalsAlphabet = new();
    public HashSet<string> TerminalsAlphabet = new();
    public Stack<string> Stack = new();

    public HashSet<TransitionFunction> TransitionFunctions = new();
    public HashSet<GrammarRule> GrammarRules = new();
    public string _stackButton = "$";

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet)
    {
        NonTerminalsAlphabet = alpabet;
        TerminalsAlphabet = stackAlphabet;
    }

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet, string grammarText)
    {
        NonTerminalsAlphabet = alpabet;
        TerminalsAlphabet = stackAlphabet;
        GrammarRules = SetGrammarRules(grammarText);
        //TransitionFunctions = CreateAllErrorsTF();
        Dictionary<string, List<string>> first = First();
    }

    private HashSet<GrammarRule> SetGrammarRules(string grammarText)
    {
        HashSet<GrammarRule> GRs = new();

        var grammars = grammarText.Split("\r\n");

        foreach(var grammarLine in grammars)
        {
            var leftPart = grammarLine.Substring(0, grammarLine.IndexOf(":"));
            leftPart = leftPart.Substring(1, leftPart.Length - 2);

            var rightPart = grammarLine.Substring(grammarLine.IndexOf(":") + 1, grammarLine.Length - grammarLine.IndexOf(":") - 1);
            var rightParts = rightPart.Split("|");

            foreach (var rightPartRule in rightParts)
            {
                GRs.Add(CreateGrammarRule(leftPart, rightPartRule));
            }
        }
        return GRs;
    }

    private GrammarRule CreateGrammarRule(string leftPart, string rightPart)
    {
        GrammarRule gr = new GrammarRule(leftPart);

        if(rightPart.Length == 0)
            gr.AddNonterminalRule("~");
        

        string curSymbols = "";
        bool isTerminal = false;
        bool isNonTerminal = false;
        for (int i = 0; i < rightPart.Length; i++)
        {
            if (isTerminal)
            {
                if (rightPart[i] == '’')
                {
                    gr.AddNonterminalRule(curSymbols);
                    curSymbols = "";
                    isNonTerminal = isTerminal = false;
                }
                else
                {
                    curSymbols += rightPart[i];
                }
            }
            else if (isNonTerminal)
            {
                if (rightPart[i] == '>')
                {
                    gr.AddNonterminalRule(curSymbols);
                    curSymbols = "";
                    isNonTerminal = isTerminal = false;
                }
                else
                {
                    curSymbols += rightPart[i];
                }
            }
            else
            {
                if (rightPart[i] == '‘')
                    isTerminal = true;
                else if (rightPart[i] == '<')
                    isNonTerminal = true;
                else if ((rightPart[i] == ' '))
                    gr.AddNonterminalRule(" ");
            }
        }

        return gr;
    }

    private Dictionary<string, List<string>> First()
    {
        Dictionary<string, List<string>> firsts = new();
        foreach(var gr in GrammarRules)
        {
            if(!firsts.ContainsKey(gr.GetNonTerminal()))
            {
                firsts.Add(gr.GetNonTerminal(),new List<string>());
            }

            var strs = gr.GetStackOutput();
            if (!firsts[gr.GetNonTerminal()].Contains(strs[0]) &&
                   (TerminalsAlphabet.Contains(strs[0]) || strs[0] == " " || strs[0] == "~"))
            {
                firsts[gr.GetNonTerminal()].Add(strs[0]);
            }

        }

        bool isChanged = true;

        while (isChanged)
        {
            isChanged = false;
            foreach (var gr in GrammarRules)
            {
                //if()
            }


        }

        return firsts;
    }

}


