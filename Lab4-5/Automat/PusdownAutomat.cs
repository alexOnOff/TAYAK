﻿using System.Collections.Generic;

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
        TerminalsAlphabet.Add("~");
        GrammarRules = SetGrammarRules(grammarText);
        //TransitionFunctions = CreateAllErrorsTF();
        Dictionary<string, HashSet<string>> first = First();
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

    private Dictionary<string, HashSet<string>> First()
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

        return firsts;
    }

    private Dictionary<string, HashSet<string>> Follow()
    {
        Dictionary<string, HashSet<string>> follows = new();

        return follows;
    }
}


