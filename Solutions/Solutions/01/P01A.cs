namespace Solutions.Solutions._01;

public class P01A : ISolution
{
    public virtual Task<object> ExecuteAsync()
    {
        (List<int> a, List<int> b) = ([], []);
        
        foreach (var se in Inputs.A1.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var inputs = se.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            a.Add(int.Parse(inputs[0]));
            b.Add(int.Parse(inputs[1]));
        }

        a.Sort();
        b.Sort();

        var diff = a.Select((t, i) => Math.Abs(t - b[i])).Sum();

        return Task.FromResult((object) diff);
    }

    
}