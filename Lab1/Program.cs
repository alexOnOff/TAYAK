using System;
using System.Collections;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // для работы с точкой вместо запятой в формате double
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            while (true)
            {
                Calculator calculator = new Calculator();
                calculator.ShowResult();
                Console.WriteLine("Exit? - 0");
                if (Console.ReadLine().Equals("0")) break;
            }
        }
    }
}
