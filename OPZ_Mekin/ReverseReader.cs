using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ_Mekin
{
    public class ReverseReader
    {
        readonly static char[] symbols = new char[] { '+', '-', '*', '/', '(', ')', '^'};

        public Queue<char> Reverse(char[] str)
        {
            var stack = new Stack<char>();
            var queue = new Queue<char>();
            foreach (var ch in str)
            {
                if(!symbols.Contains(ch) || ch=='x')
                {
                    queue.Enqueue(ch);
                }
                else if(stack.Count==0 || ch=='(' || GetPriority(stack.Peek()) < GetPriority(ch))
                {
                    stack.Push(ch);
                }
                else if(ch==')')
                {
                    while(stack.Peek()!='(')
                    {
                        queue.Enqueue(stack.Pop());
                    }
                    stack.Pop();
                }
                else 
                {
                    while (stack.Count>0 && GetPriority(stack.Peek()) >= GetPriority(ch))
                    {
                        queue.Enqueue(stack.Pop());
                    }
                    stack.Push(ch);
                }
            }
            while (stack.Count != 0)
                queue.Enqueue(stack.Pop());
            return queue;
        }

        private int GetPriority(char ch)
        {
            if (ch == '^')
                return 4;
            if (ch == '*' || ch == '/')
                return 3;
            if (ch == '+' || ch == '-')
                return 2;
            return 1;
        }

        public  string ToString(Queue<char> queue)
        {
            var sb = new StringBuilder();
            foreach (var element in queue)
            {
                sb.Append(element + " ");
            }
            return sb.ToString();
        }
    }
}
