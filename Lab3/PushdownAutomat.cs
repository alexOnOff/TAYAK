namespace Lab3;

internal class PushdownAutomat
{
    HashSet<string> States = new();
    HashSet<string> Alphabet = new();
    HashSet<string> StackAlphabet = new();
    Stack<string> Stack = new();
    string _startState = "s0";
    HashSet<string> FinalStates = new() { "s0" };
    HashSet<TransitionFunction> TransitionFunctions = new();
    string _stackButton = "h0";

    public PushdownAutomat(List<string> fileLines, List<string> alphabet)
    {
        States.Add(_startState);

        Alphabet = alphabet.ToHashSet();
        StackAlphabet = alphabet.ToHashSet();
        StackAlphabet.Add(_stackButton);

        foreach (var line in fileLines)
            ExecuteDescriptionLine(line);

        AddTerminalTransFunctions();
        AddFinalStateTransFunction();


        PrintTransactionFunctions();
    }

    private void PrintAlphabet()
    {
        foreach(var v in Alphabet)
            Console.WriteLine(v);
    }

    private void PrintTransactionFunctions()
    {
        foreach (var tf in TransitionFunctions)
        {
            Console.WriteLine($"{tf.CurrentState}, {tf.InputSymbol}, {tf.StackSymbol} = {tf.NextState}, {tf.StackOutputSymbols}");
        }
    }

    private void ExecuteDescriptionLine(string inputLine)
    {
        var leftPart = inputLine.Substring(0, inputLine.IndexOf('>'));
        StackAlphabet.Add(leftPart);

        var rightParts = inputLine.Substring(inputLine.IndexOf('>') + 1, inputLine.Length - leftPart.Length - 1).Split('|');
        foreach(var rightPartToStack in rightParts)
        {
            var rightPartChars = rightPartToStack.ToCharArray();
            Array.Reverse(rightPartChars);
            TransitionFunctions.Add(new TransitionFunction(_startState, "~", leftPart, _startState, new string(rightPartChars)));
        }
    }

    private void AddTerminalTransFunctions()
    {
        foreach(var letter in Alphabet)
            TransitionFunctions.Add(new TransitionFunction(_startState, letter, letter, _startState, "~"));
    }

    private void AddFinalStateTransFunction()
    {
        TransitionFunctions.Add(new TransitionFunction(_startState, "~", _stackButton, _startState, "~"));
    }

    public bool IsPAExecutable(string inputLine, List<string> stackSymbols)
    {
        var curState = _startState;
        Queue<Configuration> queue = new();


        foreach (var symbol in stackSymbols)
            Stack.Push(symbol);
       

        queue.Enqueue(new Configuration(curState, inputLine, Stack, null));


        while(queue.Count != 0)
        {
            var curConfig = queue.Dequeue();

            if (FinalStates.Contains(curConfig.State) && curConfig.Stack.Count() == 0 && curConfig.InputLine == "")  return true;
            if (curConfig.Stack.Count() == 0) continue;
            if (curConfig.InputLine == "" && curConfig.Stack.Count() != 1) continue;
           
            var curStackSymbol = curConfig.Stack.Pop();

            if(curConfig.InputLine != "")
            {
                if (!StackAlphabet.Contains(curConfig.InputLine[0].ToString()))
                {
                    Console.WriteLine($"Incorrect symbol - {curConfig.InputLine[0]}");
                    return false;
                }
            }
            

            var tfWithEmptySyms = TransitionFunctions.Where(tf => tf.CurrentState == curConfig.State &&
                                                                   tf.StackSymbol == curStackSymbol && 
                                                                   tf.InputSymbol == "~");
            if(tfWithEmptySyms.Count() > 0)
            {
                foreach(var tf in tfWithEmptySyms)
                {
                    queue.Enqueue(GetNextCongiguration(tf, curConfig));
                }
            }
            else
            {
                foreach(var tf in TransitionFunctions)
                {
                    var sym = curConfig.InputLine[0].ToString();
                    if (tf.CurrentState == curConfig.State && tf.InputSymbol == sym && tf.StackSymbol == curStackSymbol)
                    {
                        queue.Enqueue(GetNextCongiguration(tf, curConfig));
                    }
                }
            }            
        }


        return false;
    }

    private Configuration GetNextCongiguration(TransitionFunction transitionFunction, Configuration prevConfig)
    {
        var tempStack = new Stack<string>(new Stack<string>(prevConfig.Stack));
        foreach (var sym in transitionFunction.StackOutputSymbols)
        {
            if (sym == '~') continue;
            tempStack.Push(sym.ToString());
        }
        string line = prevConfig.InputLine;
        if(transitionFunction.InputSymbol != "~")
        {
            line = line.Remove(0, 1);
        }

        var newConfig = new Configuration(transitionFunction.NextState, line, tempStack, prevConfig);
        return newConfig;
    }

    private void PrintConfig(Configuration config)
    {
        Console.Write($"{config.InputLine}, ");
        foreach(var v in config.Stack)
        {
            Console.Write($"{v}");
        }
        Console.WriteLine();
    }
}

