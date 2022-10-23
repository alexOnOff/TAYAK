using System.Collections.Generic;

namespace Lab4_5.Analyzer
{
    internal class GrammarAnalyzer
    {
        private List<string> InputGrammarLines;

        public GrammarAnalyzer(string inputGrammarLines)
        {
            InputGrammarLines = new(inputGrammarLines.Split('\n'));
        }

        public List<string> GetInputGrammarLines()
        {
            return InputGrammarLines;
        }


    }
}
