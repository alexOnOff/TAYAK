using System.Windows;
using Lab4_5.Automat;
using Lab4_5.Analyzer;

namespace Lab4_5;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var grammarAnalyzer = new GrammarAnalyzer(TB_Grammar.Text);
        PusdownAutomat pusdownAutomat;
        TB_Console.Text = "";

        if (!grammarAnalyzer.IsTextCorrect()) 
            TB_Console.Text = "Incorrect grammar!";
        else
        {
            pusdownAutomat = CreatePA();
            PrintErrors(pusdownAutomat);
        }


        


    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }

    private PusdownAutomat CreatePA()
    {
        AlphabetAnalyzer alphabetAnalyzer = new AlphabetAnalyzer(TB_Grammar.Text);
        PusdownAutomat PA = new(alphabetAnalyzer.GetNonTerminalsAlphabet(), alphabetAnalyzer.GetTerminalsAlphabet(), TB_Grammar.Text);

        return PA;
    }

    private void PrintErrors(PusdownAutomat pa)
    {
        var inpText = TB_Program.Text.Replace("\r\n", "");
        ErrorAnalyzer errorAnalyzer = pa.Execute(inpText);

        if (errorAnalyzer.GetErrorPlace.Count == 0)
            TB_Console.Text = "PROGRAM IS EXECUTABLE";
        else
        {
            foreach(var line in errorAnalyzer.GetErrorPlace.Keys)
            {
                TB_Console.Text += $"Error in line {line}, place - ";
                foreach(var pos in errorAnalyzer.GetErrorPlace[line])
                {
                    TB_Console.Text += $" {pos}";
                }
                TB_Console.Text += "\n";
            }
        }
    }
}
