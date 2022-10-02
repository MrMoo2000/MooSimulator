using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MooTheCow
{
    // Knows of existence of all animals. Keeps track of their status.
    class AnimalManager
    {
        public static Animal Player { get; private set; }
        private static List<Animal> _nonPlayerAnimals = new List<Animal>();
        private static AnimalFactory _animalFactory = new AnimalFactory();
        private static Random _rnd = new Random();

        public static void AddNonPlayerAnimal(string animalType, bool facingLeft = false)
        {
            var location = GetRandomAnimalPlacement();
            Animal animal = CreateAnimal(animalType, location, facingLeft);
            _nonPlayerAnimals.Add(animal);

            // Kick off an ASYNC which will cause the cow to move random directions at random intervals
            AnimalWander(animal);
            GiveHunger(animal);
        }
        public static void AddPlayerAnimal(string animalType, bool facingLeft = false)
        {
            if(Player != null) { throw new Exception("Animal Manager adding Player Animal but player already exists"); }
            var location = GetRandomAnimalPlacement();
            Animal animal = CreateAnimal(animalType, location, facingLeft);
            Player = animal;
        }

        private static Animal CreateAnimal(string animalType, Point location, bool facingLeft = false)
        {
            Animal animal = _animalFactory.GetAnimal(animalType);
            animal.SetLocation(location);
            animal.FacingLeft = facingLeft;
            AnimationTypes animation = (animal.FacingLeft) ? AnimationTypes.FaceLeft : AnimationTypes.FaceRight;
            Animator.DrawSingleFrame(animation, animal);
            return animal;
        }

        private static async void AnimalWander(Animal animal)
        {
            do
            {
                int wanderDelay = animal.IsStarving() ? 500 : 5000;
                await Task.Delay(_rnd.Next(250, wanderDelay));
                if (animal.Busy == false && animal.Alive == true)
                {
                    var direction = _rnd.Next(37, 41);
                    var key = (ConsoleKey)direction;
                    var pooping = _rnd.Next(0, 10);
                    if (pooping > 8)
                    {
                        animal.Poop();
                    }
                    var eating = _rnd.Next(0, 10);
                    if((eating > 6 || animal.IsStarving()) && animal.CanEat() && !animal.StomachIsFull())
                    {
                        animal.Eat();
                    }
                    else if (Validation.ValidateMove(key, animal.Drawable))
                    {
                        animal.MoveAnimal(key);
                    }
                }
            } while (animal.Alive == true);
        }

        private static async void GiveHunger(Animal animal)
        {
            do
            {
                await Task.Delay(_rnd.Next(500, 4000));
                if (animal.Alive == true)
                {
                    animal.DecreaseStomachLevel();
                }
            } while (animal.Alive == true);
        }
        private static Point GetRandomAnimalPlacement()
        {
            List<Animal> allAnimals = new List<Animal>(_nonPlayerAnimals);
            if(Player != null)
            {
                allAnimals.Add(Player);
            }
            bool placementFound;
            var point = new Point();
            do
            {
                placementFound = true;
                point = new Point(GetRandomXAnimalPlacement(), GetRandomYAnimalPlacement());

                foreach (Animal placedAnimal in allAnimals)
                {
                    if (placedAnimal.Drawable.Boundary.Contains(point))
                    {
                        placementFound = false;
                    }
                }
            } while (placementFound == false);
            return point;
        }
        private static int GetRandomXAnimalPlacement()
        {
            var rnd = new Random();
            int xLoc = rnd.Next(0, Console.WindowWidth - 20); // - Cow Width 
            return xLoc;
        }
        private static int GetRandomYAnimalPlacement()
        {
            var rnd = new Random();
            int yLoc = rnd.Next(SceneManager.Horizon, Console.WindowHeight - 10); // - Cow Height 
            return yLoc;
        }
    }
}
