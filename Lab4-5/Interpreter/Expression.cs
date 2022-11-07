using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4_5.Interfaces;

namespace Lab4_5.Interpreter;

internal class Expression : IExpression
{
    private IExpression TermVar;
    private Expression ExpressionVar;
    private char Operation;

    public Expression(IExpression termVar, Expression expressionVar, char operation)
    {
        TermVar = termVar;
        ExpressionVar = expressionVar;
        Operation = operation;
    }

    public int Interpret(Context context)
    {
        try
        {
            if (ExpressionVar == null)
                return TermVar.Interpret(context);
            else if (Operation == '+')
                return TermVar.Interpret(context) + ExpressionVar.Interpret(context);
            else if (Operation == '-')
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
