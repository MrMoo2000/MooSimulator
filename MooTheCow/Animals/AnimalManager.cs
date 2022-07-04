using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MooTheCow
{
    // Knows of existence of all animals. Keeps track of their status.
    class AnimalManager
    {
        public static Animal Player;
        private static List<Animal> _nonPlayerAnimals = new List<Animal>();
        private static AnimalFactory _animalFactory = new AnimalFactory();

        public static void AddNonPlayerAnimal(string animalType, Point location, bool facingLeft = false)
        {
            // Start anything else needed the animal manager will keep track of. 

            Animal animal = CreateAnimal(animalType, location, facingLeft);
            _nonPlayerAnimals.Add(animal);

            // Kick off an ASYNC which will cause the cow to move random directions at random intervals
            CowWander(animal);
        }
        public static void AddPlayerAnimal(string animalType, Point location, bool facingLeft = false)
        {
            Animal animal = CreateAnimal(animalType, location, facingLeft);
            Player = animal;
        }

        private static Animal CreateAnimal(string animalType, Point location, bool facingLeft = false)
        {
            Animal animal = _animalFactory.GetAnimal(animalType);
            animal.SetLocation(location);
            animal.FacingLeft = facingLeft;
            AnimationTypes animation = (animal.FacingLeft) ? AnimationTypes.FaceLeft : AnimationTypes.FaceRight;
            Animator.AnimateDrawable(animation, animal);
            return animal;
        }

        private static async void CowWander(Animal animal)
        {
            Random rnd = new Random();
            do
            {
                await Task.Delay(rnd.Next(250, 5000));
                if (animal.Busy == false && animal.Alive == true)
                {
                    var direction = rnd.Next(37, 41);
                    var key = (ConsoleKey)direction;

                    var eating = rnd.Next(0, 10);
                    if (eating > 6)
                    {
                        animal.Eat();
                    }
                    else if (Display.ValidateMove(key, animal.Drawable))
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
                
            } while (animal.Alive == true);
        }

    }
}
