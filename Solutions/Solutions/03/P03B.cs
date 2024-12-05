using System.Text.RegularExpressions;

namespace Solutions.Solutions._03;

public partial class P03A : ISolution
{
    public Task<object> ExecuteAsync()
    {
        var mulRegex = MulRegex();
        var matches = mulRegex.Matches(Inputs.B3);
        var sum = matches.Sum(m => int.Parse(m.Groups["d1"].Value) * int.Parse(m.Groups["d2"].Value));
        return Task.FromResult<object>(sum);
    }

    [GeneratedRegex(@"(mul\((?<d1>\d+),(?<d2>\d+)\))")]
    private static partial Regex MulRegex();
}