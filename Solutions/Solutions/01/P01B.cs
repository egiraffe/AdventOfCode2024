namespace Solutions.Solutions._01;

public class P01B : ISolution
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
        
        var cntInB = b.GroupBy(g => g).ToDictionary(g => g.Key, g => g.Count());

        var diff = a.Aggregate(0, (current, itm) => current + itm * cntInB.GetValueOrDefault(itm, 0));

        return Task.FromResult<object>(diff);
    }

    
}