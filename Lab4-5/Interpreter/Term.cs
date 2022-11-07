using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4_5.Interfaces;

namespace Lab4_5.Interpreter;

internal class Term : IExpression
{
    private Term TermVar;
    private Expression ExpressionVar;
    private char Operation;

    public Term(Term term, Expression expression, char operation)
    {
        TermVar = term;
        ExpressionVar = expression;
        Operation = operation;
    }
     // NEED FIX
    public int Interpret(Context context)
    {
        try
        {
            if (ExpressionVar == null)
                return TermVar.Interpret(context);
            else if (Operation == '*')
                return TermVar.Interpret(context) + ExpressionVar.Interpret(context);
            else if (Operation == '/')
                return TermVar.Interpret(context) - ExpressionVar.Interpret(context);
            else
                throw new Exception("Incorrect Operation Exception");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message.ToString());
        }
        return 0;
    }
}
