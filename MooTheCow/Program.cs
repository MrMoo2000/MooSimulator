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
        public static Dictionary<ConsoleKey, Point> KeyToPoint = new Dictionary<ConsoleKey, Point>
        {
            [ConsoleKey.UpArrow] = new Point(0, -1),
            [ConsoleKey.DownArrow] = new Point(0, 1),
            [ConsoleKey.LeftArrow] = new Point(-2, 0),
            [ConsoleKey.RightArrow] = new Point(2, 0)
        };

        //public static List<Animal> OtherCows = new List<Animal>();

        static Program()
        {
            ConfigureConsole();
        }

        static void Main(string[] args)
        {

            AnimalManager.AddNonPlayerAnimal("cow", new Point(15, 22),true);
            AnimalManager.AddNonPlayerAnimal("cow", new Point(82, 20),true);
            AnimalManager.AddNonPlayerAnimal("cow", new Point(8, 15));
            AnimalManager.AddPlayerAnimal("cow", new Point(45, 22));

            InputLoop();

        }
        public static void Exit()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Environment.Exit(0);
        }
        public static void InputLoop()
        {
            do
            {
                ProcessInput(Console.ReadKey(true).Key);
            } while (0 == 0);
        }
        public static void ProcessInput(ConsoleKey keyPressed)
        {
            var playerAnimal = AnimalManager.Player;
            if (playerAnimal.Busy == true)
            {
                return;
            }
            if (((int)keyPressed) >= 37 && (int)keyPressed <= 40 && Display.ValidateMove(keyPressed,AnimalManager.Player.Drawable))
            {
                playerAnimal.MoveAnimal(keyPressed);
            }
            if (keyPressed == ConsoleKey.Spacebar)
            {
                //theCow.drawMoo();
                //Thread.Sleep(500);
            }
            if (keyPressed == ConsoleKey.P)
            {
                //theCow.drawPoo();
            }
            if (keyPressed == ConsoleKey.E)
            {
                playerAnimal.Eat();
            }
            if (keyPressed == ConsoleKey.X)
            {
                Exit();
            }
        }

        private static void ConfigureConsole()
        {
            // TODO set a minimum window size
            Console.Clear();
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowPosition(0, 0);
        }
    }
}
