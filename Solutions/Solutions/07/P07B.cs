using System.Text;

namespace Solutions.Solutions._07;

public class P07B : ISolution
{
    private static readonly string[] Operators = ["*", "+", "||"];
    
    public Task<object> ExecuteAsync()
    {
        var inputs = Inputs.Input.Split(Environment.NewLine)
            .Select(s => s.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray())
            .ToArray();

        var validInputs = inputs
            .Where(input => GetValidCombinationCount(input[0], input[1..]) > 0)
            .ToArray();
        
        var total = validInputs.Sum(input => input[0]);
        
        return Task.FromResult<object>(total);
    }

    private static int GetValidCombinationCount(long expectedValue, long[] numbers)
    {
        var permutations = GetPermutations(numbers);
        var validPermutations = GetValidPermutations(expectedValue, permutations);
        return validPermutations.Count;
    }

    private static List<string> GetPermutations(long[] numbers)
    {
        var totalPermutations = Math.Pow(
            Operators.Length,
            numbers.Length - 1
        );
        var expressions = new List<string>();

        for (var i = 0; i < totalPermutations; i++)
        {
            var exprBuilder = new StringBuilder();
            exprBuilder.Append(numbers[0]);
            var tmp = i;
            
            for (var j = 0; j < numbers.Length - 1;)
            {
                var opIdx = tmp % Operators.Length;
                exprBuilder.Append(' ');
                exprBuilder.Append(Operators[opIdx]);
                exprBuilder.Append(' ');
                exprBuilder.Append(numbers[++j]);
                tmp /= Operators.Length;
            }

            expressions.Add(exprBuilder.ToString());
        }

        return expressions;
    }

    private static List<string> GetValidPermutations(long expectedValue, List<string> expressions)
    {
        var validPermutations = new List<string>();

        
        
        foreach (var expr in expressions)
        {
            var array = expr.Split(' ').ToArray();
            var result = long.Parse(array[0]);
            for (var index = 2; index < array.Length; index++)
            {
                var part = array[index];
                if (Operators.Contains(part)) continue;
                var number = long.Parse(part);

                result = array[index - 1] switch
                {
                    "*" => result * number,
                    "+" => result + number,
                    _ => long.Parse($"{result}{number}")
                };
            }

            if (result == expectedValue) validPermutations.Add(expr);
        }

        return validPermutations;
    }
}