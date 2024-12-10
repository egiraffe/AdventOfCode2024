namespace Solutions.Solutions._08;

public class P08B : ISolution
{
    public Task<object> ExecuteAsync()
    {
        var map = Inputs.Input.Split(Environment.NewLine).ToArray();
        
        var antinodes = new HashSet<(int x, int y)>();
        var antennas = GetAntennas(map);
        
        for(var a = 0; a < antennas.Count; a++)
        for (var b = a + 1; b < antennas.Count; b++)
        {
            var (x1, y1, f1) = antennas[a];
            var (x2 , y2, f2) = antennas[b];
            
            if (f1 != f2) continue;

            var nodes = GetAntinodes((x1, y1), (x2, y2), map);
            antinodes.UnionWith(nodes);
        }
        
        return Task.FromResult<object>(antinodes.Count);
    }

    private static List<(int x, int y, char frequency)> GetAntennas(string[] map)
    {
        var antennas = new List<(int x, int y, char frequency)>();
        for(var y = 0; y < map.Length; y++)
        for(var x = 0; x < map[y].Length; x++)
            if (map[y][x] != '.') antennas.Add((x, y, map[y][x]));
        return antennas;
    }

    private static (int dx, int dy) GetSlope(int x1, int y1, int x2, int y2)
    {
        var dx = x2 - x1;
        var dy = y2 - y1;
        
        var gcd = Math.Abs(dx);
        var b = Math.Abs(dy);

        while (b != 0)
        {
            var tmp = b;
            b = gcd % b;
            gcd = tmp;
        }
        
        return (dx/gcd, dy/gcd);
    }

    private static (int x, int y)[] GetAntinodes(
        (int x, int y) a, 
        (int x, int y) b,
        string[] map
    ) {
        var antinodes = new HashSet<(int x, int y)>();
       
        var slope = GetSlope(a.x, a.y, b.x, b.y);

        foreach (var dir in new[] {-1, 1})
        {
            var posX = a.x;
            var posY = a.y;
            while (posX >= 0 && posY >= 0 && posX < map[0].Length && posY <= map.Length)
            {
                antinodes.Add((posX, posY));
                posX += slope.dx * dir;
                posY += slope.dy * dir;
            }
        }

        antinodes.Add((b.x, b.y));
        
        return antinodes
            .Where(w =>
                w.Item1 >= 0 && w.Item1 < map[0].Length &&
                w.Item2 >= 0 && w.Item2 < map.Length)
            .ToArray();
    }
}