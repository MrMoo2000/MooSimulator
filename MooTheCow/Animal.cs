using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace MooTheCow
{
    class Animal : IAnimatable
    {
        public string Name { get; }
        public IDrawable Drawable { get; set; }
        public bool FacingLeft { get; set; }

        public int StomachMin { get; } = 0;
        public int StomachMax { get; }

        private int stomachLevel = 0;

        public Dictionary<AnimationTypes, IAnimation> Animations { get; } = new Dictionary<AnimationTypes, IAnimation>();

        public ISprite Sprite { get; }
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
                ObjectTiles = Animations[AnimationTypes.FaceRight].Frames[0]
            };
        }
        public void SetLocation(Point location)
        {
            var bound = Drawable.Boundary;
            bound.Location = location;
            Drawable.Boundary = bound;
        }
    }
}
