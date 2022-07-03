using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;

namespace MooTheCow
{
    // What should control how a cow is moved?
    // Because we want the player cow to move based on input, but we also want the cows to move
    // If we add a chicken, we want the chicken to move.
    // What is moving? It's erasing the IDrawable, then drawing an IDrawable 


    /// <summary>
    ///  big issue is that... Console write is being move around by Async processes, it's being shared, which is causing it to jump around 
    /// </summary>
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
        public static bool InputReady;
        public static Dictionary<ConsoleKey, Point> KeyToPoint = new Dictionary<ConsoleKey, Point>
        {
            [ConsoleKey.UpArrow] = new Point(0, -1),
            [ConsoleKey.DownArrow] = new Point(0, 1),
            [ConsoleKey.LeftArrow] = new Point(-2, 0),
            [ConsoleKey.RightArrow] = new Point(2, 0)
        };

        //public static List<Animal> OtherCows = new List<Animal>();

        static void Main(string[] args)
        {
            ConfigureConsole();

            Config config = new Config();
            AnimationLoader.LoadAnimiations();

            Display.Horizon = Console.WindowHeight - (Console.WindowHeight / 3 + config.HorizonOffset);

            Scene scene = new Scene(Display.Horizon);
            scene.fillTiles();

            Display.SetScene(scene);

            AnimalManager.AddNonPlayerAnimal("cow", new Point(15, 22));
            AnimalManager.AddNonPlayerAnimal("cow", new Point(25, 20));
            AnimalManager.AddNonPlayerAnimal("cow", new Point(8, 18));
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
            if (InputReady == false)
            {
                return;
            }
            if (((int)keyPressed) >= 37 && (int)keyPressed <= 40 && validateMove(keyPressed,AnimalManager.Player))
            {
                MoveAnimal(keyPressed, AnimalManager.Player);
            }
            if (keyPressed == ConsoleKey.Spacebar)
            {
                //theCow.drawMoo();
                //Thread.Sleep(500);
                /*
                foreach(Animal cow in OtherCows)
                {
                    Thread.Sleep(100);
                    TestMoveCow(cow);
                }
                */
            }
            if (keyPressed == ConsoleKey.P)
            {
                //theCow.drawPoo();
            }
            if (keyPressed == ConsoleKey.E)
            {
                EraseAnimal(AnimalManager.Player);
                DrawEat(AnimalManager.Player);
            }
            if (keyPressed == ConsoleKey.X)
            {
                Exit();
            }
        }
        public static bool validateMove(ConsoleKey keyPressed, Animal animal)
        {
            Point validatePoint = animal.Drawable.Boundary.Location;

            validatePoint.Offset(KeyToPoint[keyPressed]);
            if (
                (validatePoint.X >= Console.WindowWidth - animal.Drawable.Boundary.Width + 1) ||
                (validatePoint.X < 0) ||
                (validatePoint.Y <= Display.Horizon - animal.Drawable.Boundary.Height) || 
                (validatePoint.Y >= Console.WindowHeight - 6) ||
                Display.DrawableCollision(animal.Drawable, validatePoint)
                )
            {
                return false;
            }
            return true;
        }
        public static void EraseAnimal(Animal animal)
        {
            Display.Erase(animal.Drawable);
        }
        public static async void DrawAnimal(Animal animal)
        {
            if(animal == AnimalManager.Player)
            {
                InputReady = false;
            }
            AnimationTypes animation = (animal.FacingLeft) ? AnimationTypes.WalkLeft : AnimationTypes.WalkRight;
            await Animator.Animation(animation, animal);
            if (animal == AnimalManager.Player)
            {
                InputReady = true;
            }
        }
        public static async void DrawEat(Animal animal)
        {
            if (animal == AnimalManager.Player)
            {
                InputReady = false;
            }
            AnimationTypes animation = (animal.FacingLeft) ? AnimationTypes.EatLeft : AnimationTypes.EatRight;
            await Animator.Animation(animation, animal);
            if (animal == AnimalManager.Player)
            {
                InputReady = true;
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


        public static void MoveAnimal(ConsoleKey keyPressed, Animal animal)
        {
            EraseAnimal(animal);
            animal.Drawable.AdjustLocation(KeyToPoint[keyPressed]);
            animal.FacingLeft = (AnimalMovingLeft(KeyToPoint[keyPressed]) == null) ? animal.FacingLeft : (bool)AnimalMovingLeft(KeyToPoint[keyPressed]);
            DrawAnimal(animal);
        }

        private static Nullable<bool> AnimalMovingLeft(Point moveDirection)
        {
            if(moveDirection.X > 0)
            {
                return false;
            }
            else if (moveDirection.X < 0)
            {
                return true;
            }
            return null;
        }

        /*
        private async static void TestMoveCow(Animal cow)
        {
            do
            {
                EraseAnimal(cow);
                cow.Drawable.AdjustLocation(KeyToPoint[ConsoleKey.RightArrow]);
                DrawAnimal(cow);
                await Task.Delay(1000);

            } while (validateMove(ConsoleKey.RightArrow, cow) == true);
        }
        */
    }
}
