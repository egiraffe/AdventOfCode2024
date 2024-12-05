using System.Xml.Schema;

namespace Solutions.Solutions._04;

public class P04B : ISolution
{
    record Coordinate
    {
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; } 
        public int Column { get; set; }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    }
    
    class Node(Dictionary<Coordinate, Node> nodes)
    {
        public required Coordinate Coordinate { get; init; }
        public required char Letter { get; init; }
        
        public Node? Up => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row - 1, Coordinate.Column));
        public Node? Down => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row + 1, Coordinate.Column));
        public Node? Left  => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row, Coordinate.Column - 1));
        public Node? Right  => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row, Coordinate.Column + 1));
        public Node? UpLeft  => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row - 1, Coordinate.Column - 1));
        public Node? UpRight  => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row - 1, Coordinate.Column + 1));
        public Node? DownLeft  => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row + 1, Coordinate.Column - 1));
        public Node? DownRight => nodes.GetValueOrDefault(new Coordinate(Coordinate.Row + 1, Coordinate.Column + 1));
        
        public Node? this[Direction index]
        {
            get
            {
                return index switch
                {
                    Direction.Up => Up,
                    Direction.Down => Down,
                    Direction.Left => Left,
                    Direction.Right => Right,
                    Direction.UpLeft => UpLeft,
                    Direction.UpRight => UpRight,
                    Direction.DownLeft => DownLeft,
                    Direction.DownRight => DownRight,
                    _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
                };
            }
        }
    }
    
    public Task<object> ExecuteAsync()
    {
        var chars = Inputs.A4
            .Split(Environment.NewLine)
            .Select(s => s.ToArray())
            .ToArray();

        Dictionary<Coordinate, Node> nodes = [];
        List<Coordinate> starts = [];
        const char startingLetter = 'A';
        
        for (var r = 0; r < chars.Length; r++)
        for (var c = 0; c < chars[0].Length; c++)
        {
            var coord = new Coordinate(row: r, column: c);
            var letter = chars[r][c];
            nodes[coord] = new Node(nodes)
            {
                Coordinate = coord,
                Letter = letter
            };
            
            if (letter == startingLetter) starts.Add(coord);
        }
        
        var count = 0;
        char?[] xMaS = ['M', 'S'];
        for (var i = 0; i < starts.Count; i++)
        {
            var start = starts[i];
            var node = nodes[start];

            var ul = node?[Direction.UpLeft]?.Letter;
            var ur = node?[Direction.UpRight]?.Letter;
            var dl = node?[Direction.DownLeft]?.Letter;
            var dr = node?[Direction.DownRight]?.Letter;

            if (ul != dr && ur != dl && !xMaS.Except([ul, dr]).Any() && !xMaS.Except([ur, dl]).Any())
                count++;
        }

        return Task.FromResult<object>(count);
    }
}