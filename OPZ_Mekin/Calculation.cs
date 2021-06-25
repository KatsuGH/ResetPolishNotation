using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ_Mekin
{
    public class Calculation
    {
        readonly static char[] symbols = new char[] { '+', '-', '*', '/', '(', ')', '^' };

        public int X { get; private set; }

        public Calculation(int x)
        {
            X = x;
        }

        public double Calculate(char[] str)
        {
            var stack = new Stack<double>();
            foreach (var element in str)
            {
                if(element=='x')
                    stack.Push(X);
                else if (!symbols.Contains(element))
                    stack.Push(double.Parse(element.ToString()));
                else
                {
                    stack.Push(HelpCalculate(stack.Pop(), stack.Pop(), element));
                }
            }
            return stack.Pop();
        }

        private double HelpCalculate(double second, double first, char op)
        {
            switch (op)
            {
                case '+':
                    return first + second;
                case '-':
                    return first - second;
                case '*':
                    return first * second;
                case '/':
                    return first / second;
                case '^':
                    return Math.Pow(first, second);
                default:
                    throw new Exception("Некорректная запись!");
            }
        }
    }
}
