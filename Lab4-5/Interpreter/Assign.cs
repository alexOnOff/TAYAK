using System;
using Lab4_5.Interpreter.Primitives;

namespace Lab4_5.Interpreter;

internal class Assign : IExpression
{
    private Identifier Id;
    private Expression ExpressionVar;

    public int Interpret(Context context)
    {
        throw new NotImplementedException();
    }
}
