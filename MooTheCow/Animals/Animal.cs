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
        public bool AnimationStop { get; set; } = false;


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

        public Point GetMouthLocation()
        {
            var facingOffset = (this.FacingLeft) ? new Point(0, Drawable.Boundary.Height) : new Point(Drawable.Boundary.Width - 4, Drawable.Boundary.Height);
            var mouthLocation = Drawable.Boundary.Location;
            mouthLocation.Offset(facingOffset);
            return mouthLocation;
        }


        public async void Eat()
        {
            Busy = true;
            AnimationTypes animation = (this.FacingLeft) ? AnimationTypes.HeadDownLeft : AnimationTypes.HeadDownRight;

            await Animator.Animation(animation, this);
            int grassCount = 0;
            await Task.Delay(1000);

            var tileTypes = SceneManager.GetTileTypes(GetMouthLocation(), 4);

            var facingOffset = (this.FacingLeft) ? new Point(0, Drawable.Boundary.Height) : new Point(Drawable.Boundary.Width-4, Drawable.Boundary.Height); // Duplicated in GetMouthLocation... 

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
            this.Drawable.AdjustLocation(InputManager.KeyToPoint[keyPressed]);
            this.FacingLeft = (AnimalMovingLeft(InputManager.KeyToPoint[keyPressed]) == null) ? this.FacingLeft : (bool)AnimalMovingLeft(InputManager.KeyToPoint[keyPressed]);
            DrawAnimal();
        }

        public void DecreaseStomachLevel(int amount = 1)
        {
            StomachLevel = (StomachLevel - amount) <= 0 ? 0 : StomachLevel - amount;
            if(StomachLevel == 0)
            {
                Death();
            }
        }

        public void Death()
        {
            Alive = false;
            AnimationStop = true;
            Animator.DrawSingleFrame(AnimationTypes.Death, this);
        }

        public void Poop()
        {
            int poopXOffset = (FacingLeft) ? Drawable.Boundary.Width : -1;
            int poopYOffset = Drawable.Boundary.Height - 1;
            Point poopOffset = new Point(poopXOffset, poopYOffset);
            Point createLocation = Drawable.Boundary.Location;
            createLocation.Offset(poopOffset);
            var poo = new Poo();
            if(Validation.ValidateCreateItem(createLocation, poo))
            {
                var tile = SceneManager.Scene.Tiles[createLocation.X, createLocation.Y];
                tile.Item = poo;
                SceneManager.UpdateSceneTile(createLocation, tile);
                poo.DecomposePoo(createLocation);
            }
        }

        public async void Emote()
        {
            var emoteWord = "Moooo";
            IObjectTile[,] objectTiles = new IObjectTile[emoteWord.Length+1, 1];
            for (int i = 1; i < emoteWord.Length+1; i++)
            {
                objectTiles[i, 0] = new ObjectTile()
                {
                    ForegroundColor = ConsoleColor.Black,
                    BackgroundColor = ConsoleColor.White,
                    Image = emoteWord[i-1]
                };
            }            

            var emoteXOffset = (FacingLeft) ? -1 * (emoteWord.Length + 2) : Drawable.Boundary.Width;
            var emoteYOffset = Drawable.Boundary.Height - 4;
            Point emoteOffset = new Point(emoteXOffset, emoteYOffset);
            Point createLocation = Drawable.Boundary.Location;
            createLocation.Offset(emoteOffset);

            IDrawable emoteDrawable = new Drawable()
            {
                Boundary = new Rectangle(createLocation.X, createLocation.Y, objectTiles.GetLength(0), objectTiles.GetLength(1)),
                ObjectTiles = objectTiles,
                LayerOverride = 999
            };
            if (Validation.ValidateDrawableInWindow(emoteDrawable))
            {
                Display.Draw(emoteDrawable);
                await Task.Delay(1000);
                Display.Erase(emoteDrawable);

            }
        }

        public bool CanEat()
        {
            var tileTypes = SceneManager.GetTileTypes(GetMouthLocation(), 4);
            for (int i = 0; i < 4; i++)
            {
                if (tileTypes[i].Equals(typeof(GrassTile)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStarving()
        {
            if (StomachLevel <= StomachMax / 4)
            {
                return true;
            }
            return false;
        }

        public bool StomachIsFull()
        {
            if(StomachLevel == StomachMax)
            {
                return true;
            }
            return false;
        }

    }
}
