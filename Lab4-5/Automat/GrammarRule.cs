using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_5.Automat;

internal class GrammarRule
{
    private string NonTerminal;
    public string GetNonTerminal() { return NonTerminal; }
    private List<string> StackOutput;
    public List<string> GetStackOutput { get { return StackOutput; } }

    public GrammarRule(string nonTerminal, List<string> stackOutput)
    {
        NonTerminal = nonTerminal;
        StackOutput = stackOutput;
    }

    public void AddNonterminalRule(string str)
    {
        StackOutput.Add(str);
    }
}
