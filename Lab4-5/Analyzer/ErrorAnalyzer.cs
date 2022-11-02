using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4_5.Interfaces;

namespace Lab4_5.Analyzer
{
    internal class ErrorAnalyzer : IAnalyzer
    {
        private readonly Dictionary<int, List<int>> ErrorPlace;
        public Dictionary<int, List<int>> GetErrorPlace { get { return ErrorPlace; } }

        public ErrorAnalyzer()
        {
            ErrorPlace = new();
        }

        public void AddErrorPlace(int line, int position)
        {
            if(!ErrorPlace.ContainsKey(line))
                ErrorPlace.Add(line, new List<int> { position});
            else
                ErrorPlace[line].Add(position);
        }


        public bool IsTextCorrect()
        {
            throw new NotImplementedException();
        }

        public bool IsTextLineCorrect(string analyzingLine)
        {
            throw new NotImplementedException();
        }
    }
}
