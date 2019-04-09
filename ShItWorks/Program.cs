using System;
using OpenTK;

namespace ShItWorks
{
    class Program
    {
        public static Game Game = new Game();

        static void Main(string[] args)
        {
            Game.Current.Run(60.0);
        }
    }
}
