
using System.Collections.Generic;

namespace MooTheCow
{
    interface IAnimatable
    {
        public Dictionary<AnimationTypes, IAnimation> Animations { get; }
        public IDrawable Drawable { get; set; }
    }
}
