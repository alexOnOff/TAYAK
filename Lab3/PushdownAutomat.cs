namespace Lab2;

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
        //Stack.Push(_stackButton);
        States.Add(_startState);

        Alphabet = alphabet.ToHashSet();
        StackAlphabet = alphabet.ToHashSet();

        foreach (var line in fileLines)
            ExecuteDescriptionLine(line);

        AddTerminalTransFunctions();
        AddFinalStateTransFunction();

        //PrintAlphabet();
        PrintTransactionFunctions();
    }

    private void AddTransitionFunc()
    {
        //TransitionFunctions.Add(new TransitionFunction());

    }

    private void PrintAlphabet()
    {
        foreach(var v in Alphabet)
        {
            Console.WriteLine(v);
        }
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
            var curStackSymbol = curConfig.Stack.Pop();

            var tfWithEmptySyms = TransitionFunctions.Where(tf => tf.CurrentState == curConfig.State &&
                                                                   tf.StackSymbol == curStackSymbol && 
                                                                   tf.InputSymbol == "~");
            if(tfWithEmptySyms.Count() > 0)
            {
                foreach(var tf in tfWithEmptySyms)
                {
                    queue.Enqueue(GetNextCongiguration(tf, inputLine, curConfig));
                }
            }
            else
            {
                foreach(var tf in TransitionFunctions)
                {
                    if (tf.CurrentState == curConfig.State && tf.InputSymbol == curConfig.InputLine && tf.StackSymbol == curStackSymbol)
                    {
                        queue.Enqueue(GetNextCongiguration(tf, inputLine, curConfig));
                    }
                }
            }

            if (FinalStates.Contains(curConfig.State) && curStackSymbol == "~" && Stack.Pop() == _stackButton) return true;
        }


        return false;
    }

/*    private bool IsExecututableIteration(string curState, string inputLine, string stackSymbol)
    {
        Queue<Configuration> queue = new();

        foreach(var tf in TransitionFunctions)
        {
            if (tf.CurrentState == curState && tf.InputSymbol == inputLine[0].ToString() && tf.StackSymbol == stackSymbol)
                queue.Append(new Configuration(tf.NextState, inputLine, Stack));
        }
            
        return true;
    }*/

    private Configuration GetNextCongiguration(TransitionFunction transitionFunction, string inputLine, Configuration prevConfig)
    {
        var tempStack = new Stack<string>(new Stack<string>(prevConfig.Stack));
        foreach (var sym in transitionFunction.StackOutputSymbols)
        {
            tempStack.Push(sym.ToString());
        }

        var newConfig = new Configuration(transitionFunction.NextState, inputLine, tempStack, prevConfig);
        return newConfig;
    }
}

