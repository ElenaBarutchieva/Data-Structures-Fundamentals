namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                var current = parentheses[i];
                if (stack.Count > 0 && current == '}' && stack.Peek() == '{')
                {
                    stack.Pop();
                }
                else if (stack.Count > 0 && current == ']' && stack.Peek() == '[')
                {
                    stack.Pop();
                }
                else if (stack.Count > 0 && current == ')' && stack.Peek() == '(')
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(current);
                }
            }

            if (stack.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
