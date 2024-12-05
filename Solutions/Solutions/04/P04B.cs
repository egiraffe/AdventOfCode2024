using System.Xml.Schema;

namespace Solutions.Solutions._04;

public class P04A : ISolution
{
    private const string Xmas = "XMAS";

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
        var startingLetter = Xmas[0];
        
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
        
        foreach (var start in starts)
        {
            var node = nodes[start];

            foreach (var dir in Enum.GetValues<Direction>())
            {
                var workNode = node;
                
                var i = 1;
                var isValid = true;
                do
                {
                    workNode = workNode?[dir];
                    isValid = workNode?.Letter == Xmas[i++];
                } while (i < Xmas.Length && workNode is not null && isValid);

                if (isValid) count++;
            }
        }

        return Task.FromResult<object>(count);
    }
}