namespace Lab2;

public class Automate
{
    public List<string> States; 
    public List<char> Alphabet;
    public HashSet<string> FinallyStates = new HashSet<string>();
    public string StartState = "q0";
    public List<TransitionFunction> TransitionFunctions;
    

    public Automate(List<string> fileLines, List<char> alphabet)
    {
        States = new();
        TransitionFunctions = new();

        Alphabet = alphabet;
        foreach(var fileLine in fileLines)
        {
            ExecuteDescriptionLine(fileLine);
        }
        States = States.Distinct().ToList();
        CheckFinallyStates();
    }


    private void AddTransitionFunc(string curState ,char symbol, string nextState)
    {
        TransitionFunctions.Add(new TransitionFunction(curState, symbol, nextState));
    }

    private void ExecuteDescriptionLine(string line)
    {
        int i = 0;
        string curState, nextState;
        char symbol;


        curState = ReadState(line, ref i);
        States.Add(curState);

        i += 1;
        symbol = line[i];

        i += 2;

        nextState = ReadState(line, ref i);
        States.Add(nextState);
        AddTransitionFunc(curState, symbol, nextState);
    }

    public void PrintStates()
    {
        foreach(var state in States)
            Console.WriteLine(state);
    }

    public void PrintTransitionFunctions()
    {
        foreach(var func in TransitionFunctions)
            Console.WriteLine($"{func.CurrentState} : {func.Symbol} - {func.NextState}");
        Console.WriteLine();
    }

    public bool IsAutomateDeterministic()
    {
        foreach(var func in TransitionFunctions.GroupBy(tf => new { tf.CurrentState, tf.Symbol}))
        {
            if (func.Count() > 1) return false;
        }
        return true;
    }

    public bool IsExecutableForInputLine(string inputLine)
    {
        string curState = StartState;
        foreach(var symbol in inputLine)
        {
            if(TransitionFunctions.Where(tf => tf.CurrentState.Equals(curState) && tf.Symbol.Equals(symbol)).Count() != 1) return false;
            curState = TransitionFunctions.Where(tf => tf.CurrentState.Equals(curState) && tf.Symbol.Equals(symbol)).First().NextState;
        }

        if(!FinallyStates.Contains(curState)) return false;
        return true;
    }

    public void Determization()
    {

        while (!IsAutomateDeterministic())
        {
            List<List<string>> newStatesByPair = new();
            List<string> newStatesNames = new();
            foreach (var func in TransitionFunctions.GroupBy(tf => new { tf.CurrentState, tf.Symbol }))
            {
                if (func.Count() > 1)
                {
                    var newStateName = "";
                    var sortedNextStatsArray = func.OrderBy(n => n.NextState).ToArray();
                    var backupStates = new List<string>();
                    var isFinal = false;
                    
                    foreach (var dest in sortedNextStatsArray)
                    {
                        newStateName += dest.NextState;
                        backupStates.Add(dest.NextState);
                        if (FinallyStates.Contains(dest.NextState)) isFinal = true; 
                    }

                    newStatesByPair.Add(backupStates);
                    newStatesNames.Add(newStateName);

                    TransitionFunctions.RemoveAll(tf => tf.CurrentState == func.Key.CurrentState && tf.Symbol == func.Key.Symbol);
                    TransitionFunctions.Add(new TransitionFunction(func.Key.CurrentState, func.Key.Symbol, newStateName));
                    States.Add(newStateName);

                }
            }

            // СДЕЛАЙТЕ ВИД, ЧТО НЕ ВИДИТЕ ЭТО
            var newTransF = new List<TransitionFunction>();
            for (int i = 0; i < newStatesByPair.Count; i++)
            {
                foreach (var newStates in newStatesByPair[i])
                {
                    foreach (var f in TransitionFunctions)
                    {
                        if (newStates == f.CurrentState)
                        {
                            newTransF.Add(new TransitionFunction(newStatesNames[i], f.Symbol, f.NextState));
                        }
                    }
                }
            }

            foreach (var f in newTransF)
                TransitionFunctions.Add(f);

            TransitionFunctions = DeleteRepeatFunctions();

        }
        CheckFinallyStates();
        Console.WriteLine("Automate is Determizated!\n");
    }

    private List<TransitionFunction> DeleteRepeatFunctions()
    {
        List<TransitionFunction> transitions = new List<TransitionFunction>();
        foreach (var item in TransitionFunctions)
        {
            bool isEntry = false;
            foreach (var item2 in transitions)
            {
                if (item.Equals(item2))
                {
                    isEntry = true;
                }
            }
            if (!isEntry) transitions.Add(item);
        }
        return transitions;
    }

    private void CheckFinallyStates()
    {
        foreach(var state in States)
        {
            if(state.Contains("f")) FinallyStates.Add(state);
        }
    }

    private string ReadState(string analyzingLine, ref int index)
    {
        var state = "";
        while (char.IsDigit(analyzingLine[index]) || char.IsLetter(analyzingLine[index]))
        {
            state += analyzingLine[index];
            index++;
            if (analyzingLine.Length == index) break;
        }
        return state;
    }
}

