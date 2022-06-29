using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using System.Diagnostics;

namespace MooTheCow
{
    // What should control how a cow is moved?
    // Because we want the player cow to move based on input, but we also want the cows to move
    // If we add a chicken, we want the chicken to move.
    // What is moving? It's erasing the IDrawable, then drawing an IDrawable 
    public enum AnimationTypes
    {
        FaceRight = 0,
        FaceLeft = 1,
        WalkRight = 2,
        WalkLeft = 3,
        EatRight = 4,
        EatLeft = 5
    }


    class Program
    {
        public static Dictionary<ConsoleKey, Point> KeyToPoint = new Dictionary<ConsoleKey, Point>
        {
            [ConsoleKey.UpArrow] = new Point(0, -1),
            [ConsoleKey.DownArrow] = new Point(0, 1),
            [ConsoleKey.LeftArrow] = new Point(-1, 0),
            [ConsoleKey.RightArrow] = new Point(1, 0)
        };

        public static Animal theCow;
        static void Main(string[] args)
        {
            Config config = new Config();
            AnimationLoader.LoadAnimiations();
            AnimalFactory animalFactory = new AnimalFactory();

            Animal cowz = animalFactory.GetAnimal("cow");
            cowz.SetLocation(new Point(15, 22));



            theCow = animalFactory.GetAnimal("cow");
            theCow.SetLocation(new Point(45, 22));

            ConfigureConsole();

            Display.Horizon = Console.WindowHeight - (Console.WindowHeight / 3 + config.HorizonOffset);
            // Can create Scene Manager. That Scene Manager can Load a scene, allowing for different scenes 
            Scene scene = new Scene(Display.Horizon);
            scene.fillTiles();
            Display.SetScene(scene);

            /*
            theCow = new Cow(scene)
            {
                cowX = Console.WindowWidth / 2,
                cowY = Display.Horizon + 5
            };
            var cowLeft = new Drawable()
            {
                Boundary = new Rectangle(theCow.cowX, theCow.cowY,theCow.cowWidth, theCow.cowHeight),
                ObjectTiles = theCow.Sprite.FacingLeft
            };
            */
            //theCow.CowDrawable = cowLeft;
            //Display.Draw(cowLeft);


            Animator.AnimateDrawable(AnimationTypes.FaceRight, cowz);
            Animator.AnimateDrawable(AnimationTypes.FaceRight, theCow);

            ProcessInput();

        }
        public static void Exit()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Environment.Exit(0);
        }
        public static void ProcessInput()
        {
            do
            {
                var keyPressed = Console.ReadKey(true).Key;

                if (keyPressed == ConsoleKey.LeftArrow && validateMove(ConsoleKey.LeftArrow))
                {
                    EraseCow();
                    theCow.Drawable.AdjustLocation(KeyToPoint[ConsoleKey.LeftArrow]);
                    theCow.FacingLeft = true;
                    DrawCow();
                }
                if (keyPressed == ConsoleKey.RightArrow && validateMove(ConsoleKey.RightArrow))
                {
                    EraseCow();
                    theCow.Drawable.AdjustLocation(KeyToPoint[ConsoleKey.RightArrow]);
                    theCow.FacingLeft = false;
                    DrawCow();
                }
                if (keyPressed == ConsoleKey.UpArrow && validateMove(ConsoleKey.UpArrow))
                {
                    EraseCow();
                    theCow.Drawable.AdjustLocation(KeyToPoint[ConsoleKey.UpArrow]);
                    DrawCow();
                }
                if (keyPressed == ConsoleKey.DownArrow && validateMove(ConsoleKey.DownArrow))
                {
                    EraseCow();
                    theCow.Drawable.AdjustLocation(KeyToPoint[ConsoleKey.DownArrow]);
                    DrawCow();
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
                    //theCow.eatGrass();
                }
                if (keyPressed == ConsoleKey.X)
                {
                    Exit();
                }
            } while (0 == 0);
        }
        public static bool validateMove(ConsoleKey direction)
        {
            Point validatePoint = theCow.Drawable.Boundary.Location;

            validatePoint.Offset(KeyToPoint[direction]);
            if (
                (validatePoint.X >= Console.WindowWidth - theCow.Drawable.Boundary.Width + 1) ||
                (validatePoint.X < 0) ||
                (validatePoint.Y <= Display.Horizon - theCow.Drawable.Boundary.Height) || 
                (validatePoint.Y >= Console.WindowHeight - 6) ||
                Display.DrawableCollision(theCow.Drawable, validatePoint)
                )
            {
                return false;
            }
            return true;
        }
        public static void EraseCow()
        {
            Display.Erase(theCow.Drawable);
        }
        public static void DrawCow()
        {
            //theCow.Drawable.ObjectTiles = (theCow.FacingLeft) ? theCow.Sprite.FacingLeft : theCow.Sprite.FacingRight;
            //Display.Draw(theCow.Drawable);
            Animator.AnimateDrawable(AnimationTypes.FaceRight, theCow);
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
