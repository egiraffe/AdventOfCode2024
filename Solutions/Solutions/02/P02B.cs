namespace Solutions.Solutions._02;

public class P02A : ISolution
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
            var direction = '\0';
            var i = 0;
            for (i = 0; i < levels.Length - 1;)
            {
                var lastDirection = direction;
                if (levels[i] == levels[i + 1]) break;
                var diff = levels[i] - levels[i + 1];
                if (Math.Abs(diff) > 3) break;
                direction = diff > 0 ? '+' : '-';
                if (i > 0 && direction != lastDirection) break;
                i++;
            }

            if (i == (levels.Length - 1)) safeCount++;
        }

        return Task.FromResult<object>(safeCount);
    }
}