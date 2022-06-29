using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MooTheCow
{
    static class Animator
    {
        public static async void AnimateDrawable(AnimationTypes animationType, IAnimatable animatable)
        {
            IAnimation animation = animatable.Animations[animationType];
            IDrawable drawable = animatable.Drawable;

            foreach(var frame in animation.Frames)
            {
                drawable.ObjectTiles = frame;
                Display.Draw(drawable);
                await Task.Delay(animation.Delay);
            }
        }
    }
}
