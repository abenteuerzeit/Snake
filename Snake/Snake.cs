using System.Text;

namespace Snake
{
    internal enum Directions
    {
        Up, Down, Left, Right,
    }

    internal class Snake
    {
        private (int x, int y) Position { get; set; }
        private Queue<(int, int)> Path { get; set; }
        private int Length { get; set; }
        internal char Symbol { get; }

        public Snake()
        {
            Position = (0, 0);
            Path = new Queue<(int, int)>();
            Path.Enqueue(Position);
            Length = 1;
            Symbol = 'S';
        }

        public void Move(Directions direction)
        {

            var (dx, dy) = direction switch
            {
                Directions.Up => (0, -1),
                Directions.Down => (0, 1),
                Directions.Left => (-1, 0),
                Directions.Right => (1, 0),
                _ => (0, 0),
            };

            var (x, y) = Position;
            Position = (x + dx, y + dy);
            Path.Enqueue(Position);

            if (Path.Count > Length)
            {
                Path.Dequeue();
            }
        }

        public Queue<(int, int)> GetPath()
        {
            return Path;
        }


        public void Grow()
        {
            Length++;
        }

        public (int, int) GetPosition()
        {
            return Position;
        }

        public (int, int) SetPosition((int x, int y) position)
        {
            this.Position = position;
            return GetPosition();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Snake");

            for (var i = 0; i < Length; i++)
            {
                sb.Append('#');
            }

            sb.AppendLine();
            sb.AppendLine($"position: {Position.x} {Position.y}");

            return sb.ToString();
        }
    }
}
