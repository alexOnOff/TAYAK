using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class ConverterToRPN
    {
        Stack<char> operationStack = new Stack<char>();
        public string ConvertToRPN(string inputStr)
        {
            char lastOperation;
            int bracketCount = 0;
            string resultStr = string.Empty;

            // привожу строку к "хорошему виду без пробелов", с расставленными знаками умножения
            inputStr = PrepareInputString(inputStr);

            // посимвольно разбираю строку
            foreach(var letter in inputStr)
            {
                // запись числа 
                if (Char.IsDigit(letter) || letter.Equals('.'))
                {
                    resultStr += letter;
                    continue;
                }

                else if (IsOperation(letter))
                {
                    resultStr += ' ';
                    // берем начало стека в качестве последней операции
                    if (operationStack.Count != 0)
                        lastOperation = operationStack.Peek();
                    else
                    {
                        operationStack.Push(letter);
                        continue;
                    }
                    // если это операция приоритетней последней, пихаю ее в стек
                    if (GetOperationPriority(lastOperation) < GetOperationPriority(letter))
                    {
                        operationStack.Push(letter);
                        continue;
                    }
                    else
                    {
                        // иначе вытаскиваю последнюю и пихаю новую
                        resultStr += operationStack.Pop();

                        operationStack.Push(letter);
                        continue;
                    }
                }
                // открывающаяся скобка - в стек ее
                else if (letter.Equals('('))
                {
                    operationStack.Push(letter);
                    bracketCount++;
                    continue;
                }
                // закрывающаяся, ищем первую открывающуюся
                else if (letter.Equals(')'))
                {
                    resultStr += ' ';
                    try
                    {
                        while (operationStack.Peek() != '(')
                        {
                            resultStr += operationStack.Pop();
                        }
                        operationStack.Pop();
                        bracketCount--;
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect order of operations");
                        return "";
                    }

                }
                else if (letter.Equals(','))
                {
                    resultStr += ' ';
                }
                else
                {
                    Console.WriteLine("Incorrect Symbol - " + letter);
                    return "";
                }
            }


            while (!(operationStack.Count == 0))
            {
                resultStr += ' ';
                resultStr += operationStack.Pop();
            }
                

            if (bracketCount != 0)
            {
                Console.WriteLine("Incorrect count of brackets");
                return "";
            }

            return resultStr;
        }

        public bool IsOperation(char inputChar)
        {
            if( inputChar.Equals('+') ||
                inputChar.Equals('-') || 
                inputChar.Equals('*') ||
                inputChar.Equals('/') ||
                inputChar.Equals('^') ||
                inputChar.Equals('l'))
            {
                return true;
            }
            
            return false;
        }

        public int GetOperationPriority(char inputChar)
        {
            if (inputChar.Equals('+')) return 1;
            if (inputChar.Equals('-')) return 1;
            if (inputChar.Equals('*')) return 2;
            if (inputChar.Equals('/')) return 2;
            if (inputChar.Equals('^')) return 2;
            if (inputChar.Equals('l')) return 3;
            return 0;
        }

        public string PrepareInputString(string inputString)
        {
            inputString = inputString.Replace(" ", "");
            inputString = inputString.Replace("log", "l");

            
            for (int i = 0; i < inputString.Length; i++)
            {
                // если перед скобкой или числом будет минус, то запишем перед этим 0
                if (inputString[i].Equals('-'))
                {
                    if(i == 0)
                    {
                        inputString = inputString.Insert(i, "0");
                        continue;
                    }
                    if(inputString[i - 1].Equals(')') || inputString[i - 1].Equals('('))
                    {
                        inputString = inputString.Insert(i, "0");
                        continue;
                    }
                }
                // если перед открывающей скобкой число или зкарывающая скобка, ставлю знак умножения
                if (inputString[i].Equals('(') && i != 0)
                {
                    if(inputString[i-1].Equals(')') || Char.IsDigit(inputString[i - 1]) || inputString[i - 1].Equals('.'))
                    {
                        inputString = inputString.Insert(i, "*");
                    }
                }
                // если после закрывающей скобкой число, ставлю знак умножения
                if (inputString[i].Equals(')') && i != inputString.Length-1)
                {
                    if (Char.IsDigit(inputString[i + 1]) || inputString[i + 1].Equals('.'))
                    {
                        inputString = inputString.Insert(i+1, "*");
                    }
                }
            }

            return inputString;
        }


    }
}
