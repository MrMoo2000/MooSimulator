using System;
using System.Collections.Generic;
using System.Drawing;

namespace MooTheCow
{
    class InputManager
    {
        private static bool isProcessingInput = false;

        public static Dictionary<ConsoleKey, Point> KeyToPoint = new Dictionary<ConsoleKey, Point>
        {
            [ConsoleKey.UpArrow] = new Point(0, -1),
            [ConsoleKey.DownArrow] = new Point(0, 1),
            [ConsoleKey.LeftArrow] = new Point(-2, 0),
            [ConsoleKey.RightArrow] = new Point(2, 0)
        };

        public static void InputLoop()
        {
            if(isProcessingInput) { throw new Exception("InputLoop called after already processing input"); }
            if(AnimalManager.Player == null) { throw new Exception("Player animal not yet set when InputLoop called"); }

            isProcessingInput = true;
            do
            {
                ProcessInput(Console.ReadKey(true).Key);
            } while (0 == 0);
        }
        private static void ProcessInput(ConsoleKey keyPressed)
        {
            var playerAnimal = AnimalManager.Player;
            if (playerAnimal.Busy == true)
            {
                return;
            }
            if (((int)keyPressed) >= 37 && (int)keyPressed <= 40 && Validation.ValidateMove(keyPressed, playerAnimal.Drawable))
            {
                playerAnimal.MoveAnimal(keyPressed);
            }
            if (keyPressed == ConsoleKey.Spacebar)
            {
                playerAnimal.Emote();
            }
            if (keyPressed == ConsoleKey.P)
            {
                playerAnimal.Poop();
            }
            if (keyPressed == ConsoleKey.E)
            {
                playerAnimal.Eat();
            }
            if (keyPressed == ConsoleKey.X)
            {
                Program.Exit();
            }
        }
    }
}
