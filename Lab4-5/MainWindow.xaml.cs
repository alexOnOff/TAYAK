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
        TB_Console.Text = "";
        /* foreach (var line in grammarAnalyzer.GetInputGrammarLines()) 
             TB_Console.Text += line;*/


        if (!grammarAnalyzer.IsTextCorrect()) TB_Console.Text = "Incorrect grammar!";
        else
        {
            AlphabetAnalyzer alphabetAnalyzer = new AlphabetAnalyzer(TB_Grammar.Text);
            foreach(var str in alphabetAnalyzer.GetTerminalsAlphabet())
            {
                TB_Console.Text += str;
                TB_Console.Text += '\n';
            }
        }
    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
}
