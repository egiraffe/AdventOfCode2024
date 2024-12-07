namespace Solutions.Solutions._06;

public class P06B : ISolution
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
        var loopCausingPositions = GetPositionsForcingLoop(startingPoint.x, startingPoint.y, map);
        return Task.FromResult<object>(loopCausingPositions.Count);
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

    private static HashSet<(int x, int y)> GetPositionsForcingLoop(int sX, int sY, string[] map)
    {
        var positions = new HashSet<(int x, int y)>();
        
        for (var x = 0; x < map.Length; x++)
        for (var y = 0; y < map[x].Length; y++)
        {
            if(map[x][y] == '#' || (x == sX && y == sY)) continue;

            var rowNum = x;
            var newMap = map
                .Select((r, i) =>
                {
                    var rArr = r.ToCharArray();
                    if (rowNum == i) rArr[y] = '#';
                    return new string(rArr);
                })
                .ToArray();

            if (CheckVisitsCauseLoop(sX, sY, newMap))
                positions.Add((x, y));
        }

        return positions;
    }

    private static bool CheckVisitsCauseLoop(int sX, int sY, string[] map)
    {
        var direction = 0;
        var visited = new HashSet<(int x, int y, int direction)> { (sX, sY, direction) };
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
                if (!visited.Add((x, y, direction))) return true;
                continue;
            }

            direction = ++direction % 4;
        }

        return false;
    }
}