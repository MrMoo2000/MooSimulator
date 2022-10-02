using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;

namespace MooTheCow
{
    class Program
    {
        static Program()
        {
            ConfigureConsole();
        }

        static void Main(string[] args)
        {
            if (!SkipSplash(args))
            {
                SplashScreen();
            }
            Console.Clear();
            Console.SetWindowPosition(0, 0);

            AnimalManager.AddNonPlayerAnimal("cow", true);
            AnimalManager.AddNonPlayerAnimal("cow", true);
            AnimalManager.AddNonPlayerAnimal("cow");
            AnimalManager.AddPlayerAnimal("blackCow");
            InputManager.InputLoop();
        }

        public static void Exit()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Environment.Exit(0);
        }

        private static void ConfigureConsole()
        {
            if (Console.WindowHeight < 30 && Console.WindowHeight < 120)
            {
                Console.SetWindowSize(120, 30);
            }
            else if(Console.WindowHeight < 30)
            {
                Console.SetWindowSize(Console.WindowWidth, 30);
            }
            else if (Console.WindowWidth < 120)
            {
                Console.SetWindowSize(120, Console.WindowHeight);
            }
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        private static bool SkipSplash(string[] args)
        {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals("nosplash"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void SplashScreen()
        {
            var rnd = new Random();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            int writeDelay = 100;

            for(int i = 0; i < 100; i++)
            {
                var locX = rnd.Next(0, Console.WindowWidth - 3);
                var locY = rnd.Next(0, Console.WindowHeight - 3);
                Console.SetCursorPosition(locX, locY);
                Console.Write("MOO");
                if(writeDelay >= 30 && i > 30)
                {
                    writeDelay -= 10;
                }
                if(i < 30)
                {
                    writeDelay = 10;
                }
                Thread.Sleep(writeDelay);

            }
        }
    }
}
