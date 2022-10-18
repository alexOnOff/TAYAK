namespace Lab3;

internal class Configuration
{
    public string? State;
    public string? InputLine;
    public Stack<string> Stack = new();
    public Configuration PrevConfiguration;
    
    public Configuration(string state, string inputLine, Stack<string> stack, Configuration prevConfig)
    {
        State = state;
        InputLine = inputLine;
        Stack = stack;
        PrevConfiguration = prevConfig;
    }
}

