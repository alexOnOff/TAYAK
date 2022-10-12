using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Calculator
    {
        public string InputString;
        public string RPNString;
        public ConverterToRPN Converter;
        public Calculator()
        {
            Console.WriteLine("Enter string :");
            InputString = Console.ReadLine();
            Converter = new ConverterToRPN();
            RPNString = Converter.ConvertToRPN(InputString);
        }

        public void ShowRPN()
        {
            Console.WriteLine(RPNString);
        }

        public void ShowResult()
        {
            ShowRPN();
            Console.WriteLine(CalculateRPN());
        }

        public string CalculateRPN()
        {
            if (RPNString.Equals("")) return "Error!";
            Stack<double> numbersStack = new Stack<double>();
            double op1, op2;
            string curNumber = "";


            foreach(var letter in RPNString)
            {
                // Если символ - цифра, помещаем его в стек,
                if (Char.IsDigit(letter) || letter.Equals('.'))
                {
                    curNumber += letter;
                }
                else if (letter.Equals(' '))
                {
                    if(!curNumber.Equals(""))
                    {
                        numbersStack.Push(Convert.ToDouble(curNumber));
                        curNumber = "";
                    }
                    continue;
                }
                // иначе (символ - операция), выполняем эту операцию
                // для двух последних значений, хранящихся в стеке.
                // Результат помещаем в стек
                else
                {
                    try
                    {
                        op2 = numbersStack.Pop();
                        op1 = numbersStack.Pop();
                        numbersStack.Push(Double.Parse(DoOperation(letter, op1, op2)));
                    }
                    catch
                    {
                        return "Error!";
                    }

                }
            }

            // Возвращаем результат
            return numbersStack.Pop().ToString();
        }

        private string DoOperation(char operation, double operand1, double operand2)
        {
            if(operation.Equals('+')) return (operand1 + operand2).ToString();
            if(operation.Equals('-')) return (operand1 - operand2).ToString();
            if(operation.Equals('*')) return (operand1 * operand2).ToString();
            if(operation.Equals('/'))
            {
                if(operand2 != 0) return (operand1 / operand2).ToString();
                else Console.WriteLine("Division of by zero");
            }
            if (operation.Equals('^')) return Math.Pow(operand1,operand2).ToString();
            if (operation.Equals('l'))
            {
                if(operand1 > 0 && operand2 > 0 && operand2 != 1)
                {
                    return Math.Log(operand1, operand2).ToString();
                }
                else
                {
                    Console.WriteLine("Error with LOG operands");
                    return "Error!";
                }
            } 
                
            return "Error!";
        }
    }
}
