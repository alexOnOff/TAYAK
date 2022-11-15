using Lab4_5;
using System.Collections.Generic;

namespace Lab4_5.Interpreter;

internal class CBASInterpreter
{
    private Context CurContext;
    private string Program;
    private List<IExpression> Commands;

    public CBASInterpreter(string program)
    {
        Program = program;
        CurContext = new Context();
    }

    public void Execute()
    {
        
    }

    private void AnalyzeProgram()
    {

    }


}
