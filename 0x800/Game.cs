using System;
using System.Collections.Generic;
using System.Text;

namespace _0x800
{
    class Game
    {
        Matrix<int> board;
        bool lose = false;
        internal Game(int width, int height)
        {
            NewGame(width, height);
        }

        private void NewGame(int width, int height)
        {
            board = new Matrix<int>(width, height, 0); // Start a new game by discarding the game data
            this.FeedNumber(board); // Feed a '2' into the board
            Console.Clear();
            Console.WriteLine(board.ToString()); // Print the initial board
        }

        internal void Send(UDLR dir)
        {
            switch (dir)
            {
                case UDLR.UP: SendUp();  break;
                case UDLR.DOWN: SendDown(); break;
                case UDLR.LEFT: SendLeft(); break;
                case UDLR.RIGHT: SendRight(); break;
                case default(int): break;
            }
            FeedNumber(board);
            Console.WriteLine(board.ToString());
            if (lose)
                TriggerLose();
        }

        private void TriggerLose()
        {
            Console.WriteLine("Game over, type 'r' to try again."); // Options to quit or continue
            if (Console.ReadKey().Key == ConsoleKey.R)
            {
                NewGame(board.Width, board.Height); // Reset game
                lose = false; // while loop continues in Program.cs
            }
        }

        private void SendLeft()
        {
            for (int i = 1; i <= board.Height; i++)
            {
                List<int> row = board.GetRow(i);
                SqueezeList(row);
                board[1, board.Width, i, i] = row;
            }
            CrushMatrix(board);
        }
        private void SendRight()
        {
            board.FlipX(); SendLeft(); board.FlipX();
        }
        private void SendUp()
        {
            board.RotateCounterClock(); SendLeft(); board.RotateClock();
        }
        private void SendDown()
        {
            board.RotateClock(); SendLeft(); board.RotateCounterClock();
        }

        private List<Pair<int>> RangePair(int xMin, int xMax, int yMin, int yMax)
        {
            List<Pair<int>> res = new List<Pair<int>>();
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    res.Add(new Pair<int>(x, y));
                }
            }
            return res;
        }

        private void CrushMatrix(Matrix<int> matrix)
        {
            for (int r = 1; r <= matrix.Height; r++)
            {
                List<int> row = matrix.GetRow(r);
                for (int i = 0; i < row.Count - 1; i++)
                {
                    if (row[i] == row[i + 1])
                    {
                        row[i] *= 2;
                        row[i + 1] = 0;
                    }
                }
                SqueezeList(row);
                matrix[1, matrix.Width, r, r] = row;
            }
        }

        private void SqueezeList(List<int> list)
        {
            int len = list.Count;
            list.RemoveAll(x => x == 0);
            while (list.Count != len)
            {
                list.Add(0);
            }
        }

        internal void FeedNumber(Matrix<int> matrix)
        {
            List<int> candidates = new List<int>();
            for (int i = 0; i < matrix.Size; i++)
            {
                if (matrix[i] == 0)
                    candidates.Add(i);
            }

            if (candidates.Count == 0)
                this.lose = true;
            else
                matrix[candidates[new Random(Guid.NewGuid().GetHashCode()).Next(candidates.Count)]] = 2;
        }

        public bool Lose { get { return lose; } }
    }

    enum UDLR
    {
        UP = 1, DOWN = 2, LEFT = 3, RIGHT = 4
    }
}
