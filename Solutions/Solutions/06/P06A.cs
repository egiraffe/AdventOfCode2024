namespace Solutions.Solutions._06;

public class P06A : ISolution
{
    private static readonly (int dx, int dy)[] Directions = [
        (-1, 0), // ^
        (0, 1), // >
        (1, 0), // V
        (0, -1) // <
    ];
    
    public Task<object> ExecuteAsync()
    {
        var map = Inputs.Input.Split(Environment.NewLine);
        var startingPoint = GetStartingPoint(map);
        var visited = GetVisited(startingPoint.x, startingPoint.y, map);
        return Task.FromResult<object>(visited.Count);
    }

    private static (int x, int y) GetStartingPoint(string[] map)
    {
        int x = 0, y = 0;
        for (var i = 0; i < map.Length; i++)
        {
            var index = map[i].IndexOf('^');
            if (index == -1) continue;
            x = i;
            y = index;
            break;
        }
        
        return (x, y);
    }

    private static HashSet<(int x, int y)> GetVisited(int sX, int sY, string[] map)
    {
        var visited = new HashSet<(int x, int y)> { (sX, sY) };
        var direction = 0;
        var (x, y) = (sX, sY);

        while (true)
        {
            var (nX, nY) = (
                x + Directions[direction].dx, 
                y + Directions[direction].dy
            );
            
            if (nX < 0 || nY < 0 || nX >= map.Length || nY >= map.Length) 
                break;

            if (map[nX][nY] != '#')
            {
                x = nX;
                y = nY;
                visited.Add((x, y));
                continue;
            }
            
            direction = ++direction % 4;
        }

        return visited;
    }
}