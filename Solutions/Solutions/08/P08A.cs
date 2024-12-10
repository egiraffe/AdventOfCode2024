namespace Solutions.Solutions._08;

public class P08A : ISolution
{
    public Task<object> ExecuteAsync()
    {
        var map = Inputs.Input.Split(Environment.NewLine).ToArray();
        
        var antinodes = new List<(int x, int y)>();
        var antennas = GetAntennas(map);
        
        for(var a = 0; a < antennas.Count; a++)
        for (var b = a + 1; b < antennas.Count; b++)
        {
            var (x1, y1, f1) = antennas[a];
            var (x2 , y2, f2) = antennas[b];
            
            if (f1 != f2) continue;

            var (dx, dy) = GetDistance(x1, y1, x2, y2);
            
            antinodes.AddRange(GetAntinodes((x1, y1), (x2, y2), dx, dy, map));
        }
        
        return Task.FromResult<object>(antinodes.ToHashSet().Count);
    }

    private static List<(int x, int y, char frequency)> GetAntennas(string[] map)
    {
        var antennas = new List<(int x, int y, char frequency)>();
        for(var y = 0; y < map.Length; y++)
        for(var x = 0; x < map[y].Length; x++)
            if (map[y][x] != '.') antennas.Add((x, y, map[y][x]));
        return antennas;
    }

    private static (int dx, int dy) GetDistance(int x1, int y1, int x2, int y2)
    {
        var dx = x2 - x1;
        var dy = y2 - y1;
        return (dx, dy);
    }

    private static (int x, int y)[] GetAntinodes(
        (int x, int y) a, 
        (int x, int y) b,
        int dx, 
        int dy, 
        string[] map
    ) {
        var antinodes = new List<(int x, int y)>
        {
            (a.x - dx, a.y - dy),
            (b.x + dx, b.y + dy),
        };
        
        return antinodes
            .Where(w =>
                w.Item1 >= 0 && w.Item1 < map[0].Length &&
                w.Item2 >= 0 && w.Item2 < map.Length)
            .ToArray();
    }
}