using System;
using System.Collections.Generic;
using System.Text;

namespace _0x800
{
    class Program
    {
        static void Main(string[] args)
        {
            Game mygame = new Game(4, 4);

            while (!mygame.Lose)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: mygame.Send(UDLR.UP); break;
                    case ConsoleKey.DownArrow: mygame.Send(UDLR.DOWN); break;
                    case ConsoleKey.LeftArrow: mygame.Send(UDLR.LEFT); break;
                    case ConsoleKey.RightArrow: mygame.Send(UDLR.RIGHT); break;
                    case default(int): break;
                }
            }
        }
    }
}
