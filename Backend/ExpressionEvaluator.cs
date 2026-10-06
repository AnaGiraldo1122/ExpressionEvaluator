namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix)
    {
        return EvalutePostfix(ToPostfix(infix));
    }

    private static string ToPostfix(string infix)
    {
        var posfix = string.Empty;
        var stack = new Stack<char>();

        for (int i = 0; i < infix.Length; i++)
        {
            if (char.IsDigit(infix[i]) || infix[i] == '.')
            {
                string numero = "";

                while (i < infix.Length &&
                       (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                    numero += infix[i];
                    i++;
                }

                posfix += numero + " ";
                i--;
            }
            else
            {
                if (IsOperator(infix[i]))
                {
                    if (infix[i] == ')')
                    {
                        var ope = stack.Pop();

                        while (ope != '(')
                        {
                            posfix += ope + " ";
                            ope = stack.Pop();
                        }
                    }
                    else
                    {
                        while (stack.Count > 0 && PriorityInfix(infix[i]) <= PriorityStack(stack.Peek()))
                        {
                            posfix += stack.Pop() + " ";
                        }

                        stack.Push(infix[i]);
                    }
                }
            }
        }

        while (stack.Count != 0)
        {
            posfix += stack.Pop() + " ";
        }

        return posfix;
    }

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();

        for (int i = 0; i < postfix.Length; i++)
        {
            if (char.IsDigit(postfix[i]) || postfix[i] == '.')
            {
                string numero = "";

                while (i < postfix.Length &&
                       (char.IsDigit(postfix[i]) || postfix[i] == '.'))
                {
                    numero += postfix[i];
                    i++;
                }

                stack.Push(double.Parse(numero, System.Globalization.CultureInfo.InvariantCulture));
            }
            else
            {
                if (IsOperator(postfix[i]))
                {
                    var ope2 = stack.Pop();
                    var ope1 = stack.Pop();

                    stack.Push(Calculate(ope1, ope2, postfix[i]));
                }
            }
        }

        return stack.Pop();
    }

    private static bool IsOperator(char item) =>
        item == '^' ||
        item == '*' ||
        item == '/' ||
        item == '+' ||
        item == '-' ||
        item == '(' ||
        item == ')';

    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression."),
    };

    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression."),
    };

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid expression."),
    };
}
/*foreach (var item in infix)
{
    if (IsOperator(item))
    {
        if (item == ')')
        {
            var ope = stack.Pop();
            while(ope != '(')
            {
                posfix += ope;
                ope = stack.Pop();
            }
        }
 else
 {
     if (stack.Count == 0)
     {
         stack.Push(item);
     }
     else
     {
         if (PriorityInfix(item) > PriorityStack(stack.Peek()))
         {
             stack.Push(item);
         }
         else
         {
             posfix += stack.Pop();
             stack.Push(item);
         }
     }
 }
}
else
{
 posfix += item;
}
}
do
{
posfix += stack.Pop();
} while (stack.Count != 0);
return posfix;
}

private static int PriorityStack(char op) => op switch
{
    '^' => 3,
    '*' => 2,
    '/' => 2,
    '+' => 1,
    '-' => 1,
    '(' => 0,
    _ => throw new Exception("Invalid expression."),
};

private static int PriorityInfix(char op) => op switch
{
    '^' => 4,
    '*' => 2,
    '/' => 2,
    '+' => 1,
    '-' => 1,
    '(' => 5,
    _ => throw new Exception("Invalid expression."),
};

private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '+' || item == '-' || item == '(' || item == ')';

private static double EvalutePostfix(string postfix)
{
    var stack = new Stack<double>();
    foreach (var item in postfix)
    {
        if (IsOperator(item))
        {
            var ope2 = stack.Pop();
            var ope1 = stack.Pop();
            stack.Push(Calculate(ope1, ope2, item));
        }
        else
        {
            stack.Push(char.GetNumericValue(item));
        }
    }
    return stack.Pop();
}

private static double Calculate(double ope1, double ope2, char item) => item switch
{
    '*' => ope1 * ope2,
    '/' => ope1 / ope2,
    '+' => ope1 + ope2,
    '-' => ope1 - ope2,
    '^' => Math.Pow(ope1, ope2),
    _ => throw new Exception("Invalid expression."),
};
}*/
