using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace MooTheCow
{
    class Animal : IAnimatable
    {
        public string Name { get; }
        public IDrawable Drawable { get; set; }
        public bool FacingLeft { get; set; }

        public int StomachMin { get; } = 0;
        public int StomachMax { get; }
        public int StomachLevel { get; private set; } = 20;
        public bool Busy { get; private set; }
        public bool Alive { get; private set; } = true;


        public Dictionary<AnimationTypes, IAnimation> Animations { get; } = new Dictionary<AnimationTypes, IAnimation>();
        public Animal(Dictionary<string,string> animalProperties)
        {
            Name = animalProperties["name"];
            StomachMax = int.Parse(animalProperties["stomachMax"]);
            Animations = AnimationLoader.AnimalAnimations[animalProperties["name"]];
            Drawable = new Drawable()
            {
                Boundary = new Rectangle()
                {
                    Width = int.Parse(animalProperties["width"]),
                    Height = int.Parse(animalProperties["height"])
                },
            };
        }
        public void SetLocation(Point location)
        {
            var bound = Drawable.Boundary;
            bound.Location = location;
            Drawable.Boundary = bound;
        }


        public async void Eat()
        {
            Busy = true;
            AnimationTypes animation = (this.FacingLeft) ? AnimationTypes.HeadDownLeft : AnimationTypes.HeadDownRight;

            await Animator.Animation(animation, this);

            var facingOffset = (this.FacingLeft) ? new Point(0, 5) : new Point(11, 5);

            var mouthLocation = Drawable.Boundary.Location;
            mouthLocation.Offset(facingOffset);

            int grassCount = 0;
            await Task.Delay(1000);

            var tileTypes = SceneManager.GetTileTypes(mouthLocation, 4);

            for (int i = 0; i< 4; i++)
            {
                if (tileTypes[i].Equals(typeof(GrassTile)))
                {
                    grassCount++;
                    var point = this.Drawable.Boundary.Location;
                    point.Offset(facingOffset);
                    point.X += i;
                    SceneManager.UpdateSceneTile(point, new DirtTile());
                }
            }
            StomachLevel = (grassCount + StomachLevel) > StomachMax ? StomachMax : grassCount + StomachLevel;

            animation = (this.FacingLeft) ? AnimationTypes.HeadUpLeft : AnimationTypes.HeadUpRight;
            await Animator.Animation(animation, this);
            Busy = false;
        }

        public async void DrawAnimal()
        {
            this.Busy = true;
            AnimationTypes animation = (this.FacingLeft) ? AnimationTypes.WalkLeft : AnimationTypes.WalkRight;
            await Animator.Animation(animation, this);
            this.Busy = false;
        }
        public void EraseAnimal()
        {
            Display.Erase(this.Drawable);
        }
        private Nullable<bool> AnimalMovingLeft(Point moveDirection)
        {
            if (moveDirection.X > 0)
            {
                return false;
            }
            else if (moveDirection.X < 0)
            {
                return true;
            }
            return null;
        }
        public void MoveAnimal(ConsoleKey keyPressed)
        {
            EraseAnimal();
            this.Drawable.AdjustLocation(Program.KeyToPoint[keyPressed]);
            this.FacingLeft = (AnimalMovingLeft(Program.KeyToPoint[keyPressed]) == null) ? this.FacingLeft : (bool)AnimalMovingLeft(Program.KeyToPoint[keyPressed]);
            DrawAnimal();
        }

        public void DecreaseStomachLevel(int amount = 1)
        {
            StomachLevel = (StomachLevel - amount) <= 0 ? 0 : StomachLevel;
            if(StomachLevel == 0)
            {
                Death();
            }
        }

        public void Death()
        {
            Alive = false;
            Animator.AnimateDrawable(AnimationTypes.Death, this);
        }

    }
}
