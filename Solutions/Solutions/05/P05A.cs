using System.Collections.Concurrent;

namespace Solutions.Solutions._05;

public class P05A : ISolution
{
    public class Node
    {
        public int Number { get; init; }
        public HashSet<Node> Before { get; set; } = [];
        public HashSet<Node> After { get; set; } = [];

        public override string ToString() => Number.ToString();
        
        public static implicit operator int(Node n) => n.Number;
    }
    
    public Task<object> ExecuteAsync()
    {
        var ruleStrings = Inputs.Rules
            .Split(Environment.NewLine)
            .Select(x => x.Split('|').Select(int.Parse).ToArray())
            .ToArray();
        
        var nodes = new ConcurrentDictionary<int, Node>();

        foreach (var rule in ruleStrings)
        {
            var beforeNode = nodes.GetOrAdd(rule[0], i => new Node { Number = i });
            var afterNode = nodes.GetOrAdd(rule[1], i => new Node { Number = i });
            beforeNode.After.Add(afterNode);
            afterNode.Before.Add(beforeNode);
        }

        var inputs = Inputs.A5
            .Split(Environment.NewLine)
            .Select(s => 
                s.Split(',')
                    .Select(n => nodes[int.Parse(n)])
                    .ToArray())
            .ToArray();

        var sumOfCorrectMiddle = 0;
        foreach (var input in inputs)
        {
            var sorted = SortNodes(input);
            if (!sorted.SequenceEqual(input))
            {
                continue;
            }
            var middleDigit = sorted[sorted.Length / 2];
            sumOfCorrectMiddle += middleDigit;
        }
        
        return Task.FromResult<object>(sumOfCorrectMiddle);
        
        Node[] SortNodes(Node[] toSort)
        {
            var pageOrder = new int[toSort.Length];
            for (var i = 0; i < toSort.Length; i++)
            {
                var n = toSort[i];
                var afterNodes = nodes[n].After.Count(toSort.Contains);
                var beforeNodes = nodes[n].Before.Count(toSort.Contains);
                pageOrder[i] = beforeNodes - afterNodes;
            }

            return toSort
                .Zip(pageOrder, (node, order) => new { node, order })
                .OrderBy(o =>o.order)
                .Select(s => s.node)
                .ToArray();
        }
    }
}