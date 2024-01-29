using System.Text;

namespace Snake
{
    internal class Map
    {
        private char[,] Board { get; set; }
        private const char ItemSymbol = 'o';
        private int _score = 0;

        public Map(Snake snake, int size = 32)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
            Board = new char[size, size];
            AddSnake(snake);
            AddItems();
        }


        public bool MoveSnake(Snake snake, Directions direction)
        {
            var rows = Board.GetLength(0) - 1;
            var cols = Board.GetLength(1) - 1;

            var (nextX, nextY) = GetNextPosition(snake.GetPosition(), direction);

            foreach (var (bodyX, bodyY) in snake.GetPath())
            {
                if (nextX == bodyX && nextY == bodyY)
                {
                    return false;
                }
            }
            snake.Move(direction);
            var (x, y) = snake.GetPosition();

            var (snakeCol, snakeRow) = snake.SetPosition((x, y));
            char target;
            try
            {
                target = Board[snakeRow, snakeCol];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            for (var r = 0; r <= rows; r++)
            {
                for (var c = 0; c <= cols; c++)
                {
                    if (Board[r, c] == snake.Symbol)
                    {
                        Board[r, c] = '\0';
                    }
                }
            }

            if (x < 0 || y < 0 || x >= Board.GetLength(1) || y >= Board.GetLength(0))
            {
                return false;
            }

            foreach (var (snakeX, snakeY) in snake.GetPath())
            {
                Board[snakeY, snakeX] = snake.Symbol;
            }

            if (target == ItemSymbol)
            {
                snake.Grow();
                AddItems(1);
                _score += 10;
            }

            Board[snakeRow, snakeCol] = snake.Symbol;
            return true;
        }


        private void AddSnake(Snake snake)
        {
            var (x, y) = snake.GetPosition();
            Board[x, y] = snake.Symbol;
        }

        private void AddItems(int count = 0)
        {
            var size = Board.GetLength(0);
            if (count == 0) count = size / 4;

            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                var (x, y) = (random.Next(0, size), random.Next(0, size));
                var target = Board[x, y].ToString();
                if (target == "\0") Board[x, y] = ItemSymbol;
                else i--;
            }
        }

        private static (int, int) GetNextPosition((int x, int y) position, Directions direction)
        {
            var (x, y) = position;
            return direction switch
            {
                Directions.Up => (x, y - 1),
                Directions.Down => (x, y + 1),
                Directions.Left => (x - 1, y),
                Directions.Right => (x + 1, y),
                _ => (x, y),
            };
        }


        public void Print()
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            Console.WriteLine(Render());
        }

        private string Render()
        {
            var rows = Board.GetLength(0);
            var cols = Board.GetLength(1);


            var output = new StringBuilder();

            output.AppendLine($"Map Size: {rows} x {cols}");
            output.AppendLine($"Score: {_score}");

            var border = new string('=', rows);

            output.AppendLine("+" + border + "+");

            for (var r = 0; r < rows; r++)
            {
                output.Append('|');
                for (var c = 0; c < cols; c++)
                {
                    var piece = Board[r, c];
                    switch (piece)
                    {
                        case '\0':
                            output.Append('.');
                            break;
                        default:
                            output.Append(piece);
                            break;
                    }
                }
                output.AppendLine("|");
            }
            output.AppendLine("+" + border + "+");
            output.AppendLine();
            return output.ToString();
        }

    }
}
