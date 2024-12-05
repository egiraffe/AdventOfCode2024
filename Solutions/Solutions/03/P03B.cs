using System.Text.RegularExpressions;

namespace Solutions.Solutions._03;

public partial class P03B : ISolution
{
    public Task<object> ExecuteAsync()
    {
        var mulRegex = MulRegex();
        var controlRegex = ControlRegex();

        var isEnabled = true;
        var sum = 0;
        
        foreach (Match match in controlRegex.Matches(Inputs.B3))
        {
            if (match.Groups["do"].Success)
            {
                isEnabled = true;
            }
            else if (match.Groups["dont"].Success)
            {
                isEnabled = false;
            }
            else if (match.Groups["mul"].Success && isEnabled)
            {
                var mulMatch = mulRegex.Match(match.Groups["mul"].Value);
                if (!mulMatch.Success) continue;
                var d1 = int.Parse(mulMatch.Groups["d1"].Value);
                var d2 = int.Parse(mulMatch.Groups["d2"].Value);
                sum += d1 * d2;
            }
        }

        return Task.FromResult<object>(sum);
    }

    [GeneratedRegex(@"mul\((?<d1>\d+),(?<d2>\d+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(?<mul>mul\(\d+,\d+\))|(?<do>do\(\))|(?<dont>don't\(\))")]
    private static partial Regex ControlRegex();
}