using System.Collections.Generic;
using System.Linq;

namespace Lab4_5.Automat;

internal class PusdownAutomat
{

    public HashSet<string> NonTerminalsAlphabet = new();
    public HashSet<string> TerminalsAlphabet = new();
    public Stack<string> Stack = new();

    public HashSet<TransitionFunction> PredictAnalyzerTable = new();
    public HashSet<GrammarRule> GrammarRules = new();
    public string _stackButton = "$";

    private readonly string _startNonTerminal = "E";

    private Dictionary<string, HashSet<string>> First;
    private Dictionary<string, HashSet<string>> Follow;

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet)
    {
        NonTerminalsAlphabet = alpabet;
        TerminalsAlphabet = stackAlphabet;
    }

    public PusdownAutomat(HashSet<string> alpabet, HashSet<string> stackAlphabet, string grammarText)
    {
        NonTerminalsAlphabet = alpabet;
        TerminalsAlphabet = stackAlphabet;
        TerminalsAlphabet.Add("~");
        GrammarRules = SetGrammarRules(grammarText);

        SetFirst();
        SetFollow();
        SetPredictAnalyzerTable();
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
            gr.AddStackRule("~");
        

        string curSymbols = "";
        bool isTerminal = false;
        bool isNonTerminal = false;
        for (int i = 0; i < rightPart.Length; i++)
        {
            if (isTerminal)
            {
                if (rightPart[i] == '’')
                {
                    gr.AddStackRule(curSymbols);
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
                    gr.AddStackRule(curSymbols);
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
                    gr.AddStackRule(" ");
            }
        }

        return gr;
    }

    private void SetFirst()
    {
        bool isChanged = true;
        Dictionary<string, HashSet<string>> firsts = new();

        foreach(var gr in GrammarRules)
            if(!firsts.ContainsKey(gr.GetNonTerminal()))
                firsts.Add(gr.GetNonTerminal(),new HashSet<string>());

        while (isChanged)
        {
            isChanged = false;

            foreach (var gr in GrammarRules) 
            {
                var prevLen = firsts[gr.GetNonTerminal()].Count;

                if (NonTerminalsAlphabet.Contains(gr.GetStackOutput()[0]) )
                    firsts[gr.GetNonTerminal()].UnionWith(firsts[gr.GetStackOutput()[0]]);
                else if(TerminalsAlphabet.Contains(gr.GetStackOutput()[0]))
                    firsts[gr.GetNonTerminal()].Add(gr.GetStackOutput()[0]);
                
                if (prevLen != firsts[gr.GetNonTerminal()].Count) isChanged = true;
            }
        }

        First = firsts;
    }

    private void SetFollow()
    {
        Dictionary<string, HashSet<string>> follows = new();
        bool isChanged = true;

        foreach (var gr in GrammarRules)
            if (!follows.ContainsKey(gr.GetNonTerminal()))
                follows.Add(gr.GetNonTerminal(), new HashSet<string>());

        follows[_startNonTerminal].Add("$");

        while(isChanged)
        {
            isChanged = false;

            foreach (var gr in GrammarRules)
            {
                for (int i = 0; i < gr.GetStackOutput().Count; i++)
                {
                    if (TerminalsAlphabet.Contains(gr.GetStackOutput()[i])) continue;
                    var prevLen = follows[gr.GetStackOutput()[i]].Count;

                    HashSet<string> epsilon = new HashSet<string>() { "~" };


                    if(i < gr.GetStackOutput().Count - 1)
                    {
                        if (NonTerminalsAlphabet.Contains(gr.GetStackOutput()[i + 1]))
                        {
                            follows[gr.GetStackOutput()[i]].UnionWith(First[gr.GetStackOutput()[i + 1]].Except(epsilon));

                            if (First[gr.GetStackOutput()[i + 1]].Contains("~"))
                                follows[gr.GetStackOutput()[i]].UnionWith(follows[gr.GetNonTerminal()]);
                        }
                        else if (TerminalsAlphabet.Contains(gr.GetStackOutput()[i + 1]))
                            follows[gr.GetStackOutput()[i]].Add(gr.GetStackOutput()[i + 1]);
                    }
                    else
                        follows[gr.GetStackOutput()[i]].UnionWith(follows[gr.GetNonTerminal()]);

                    if (follows[gr.GetStackOutput()[i]].Count != prevLen) isChanged = true;
                }
            }
        }

        Follow = follows;
    }
  
    private void SetPredictAnalyzerTable()
    {
        foreach(var nonterm in NonTerminalsAlphabet)
        {
            foreach(var terminal in First[nonterm])
            {

            }
        } 

        foreach(var item in First)
        {
            foreach(var terminal in item.Value)
            {


                PredictAnalyzerTable.Add(new TransitionFunction(item.Key, terminal,));
            }
        }

    }
}