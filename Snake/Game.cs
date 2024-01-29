namespace Snake
{
    internal class Game
    {
        private Map GameMap { get; set; }
        private Snake Player { get; set; }
        private Directions Direction { get; set; } = Directions.Right;
        private int Speed { get; set; }

        public Game()
        {
            Speed = 400;
            Player = new Snake();
            GameMap = new Map(Player);
        }


        public void Play()
        {
            var isRunning = true;
            GameMap.Print();
            while (isRunning)
            {
                if (!GameMap.MoveSnake(Player, Direction))
                {
                    Console.WriteLine("Game Over!");
                    break;
                }

                Console.Clear();
                GameMap.Print();

                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey();
                    switch (input.Key)
                    {
                        case ConsoleKey.D1:
                            Speed += 100;
                            break;
                        case ConsoleKey.D2:
                            Speed -= 100;
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            Direction = Directions.Up;
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            Direction = Directions.Left;
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            Direction = Directions.Down;
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            Direction = Directions.Right;
                            break;
                        default:
                            isRunning = false;
                            break;
                    }

                }


                Console.WriteLine(Player.ToString());
                Console.WriteLine("Speed {0} Press 1 to slow dow, 2 to speed up", Speed);

                Task.Delay((int)Speed).Wait();
            }
        }

    }
}
