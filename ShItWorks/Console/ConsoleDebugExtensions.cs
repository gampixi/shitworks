using System;

namespace ShItWorks
{
    public static class ConsoleLog
    {
        public static void Message(object m)
        {
            if (string.IsNullOrEmpty(m.ToString())) return;
            Console.WriteLine($"{Game.Current.TotalTime} \t--> {m.ToString()}");
        }

        public static void Warning(object m)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Message(m);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(object m)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Message(m);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
