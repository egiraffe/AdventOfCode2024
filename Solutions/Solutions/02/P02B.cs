namespace Solutions.Solutions._02;

public class P02B : ISolution
{
    public Task<object> ExecuteAsync()
    {
        var reports = Inputs.A2
            .Split(Environment.NewLine)
            .Select(l => l
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray())
            .ToArray();

        var safeCount = 0;

        foreach (var levels in reports)
        {
            if (IsSafe(levels))
            {
                safeCount++;
                continue;
            }
            
            var isSafeWithDampener = levels
                .Select((t, j) => levels
                    .Where((_, index) => index != j)
                    .ToArray())
                .Any(IsSafe);

            if (isSafeWithDampener) safeCount++;
        }

        return Task.FromResult<object>(safeCount);
    }

    private static bool IsSafe(int[] levels)
    {
        var direction = '\0';
        int i;
        for (i = 0; i < levels.Length - 1;)
        {
            var lastDirection = direction;
            if (levels[i] == levels[i + 1]) return false;
            var diff = levels[i] - levels[i + 1];
            if (Math.Abs(diff) > 3) return false;
            direction = diff > 0 ? '+' : '-';
            if (i > 0 && direction != lastDirection) return false;
            i++;
        }
        return i == (levels.Length - 1);
    }
}