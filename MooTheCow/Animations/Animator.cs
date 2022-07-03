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
            Program.InputReady = false;
            IAnimation animation = animatable.Animations[animationType];
            IDrawable drawable = animatable.Drawable;

            int counter = 0;
            foreach(var frame in animation.Frames)
            {
                counter++;
                drawable.ObjectTiles = frame;
                Display.Draw(drawable);
                if(counter != animation.Frames.Count)
                {
                    await Task.Delay(animation.Delay);
                }
            }
            Program.InputReady = true;
        }
        public static async Task<bool> Animation(AnimationTypes animationType, IAnimatable animatable)
        {
            IAnimation animation = animatable.Animations[animationType];
            IDrawable drawable = animatable.Drawable;

            int counter = 0;
            foreach (var frame in animation.Frames)
            {
                counter++;
                drawable.ObjectTiles = frame;
                Display.Draw(drawable);
                if (counter != animation.Frames.Count)
                {
                    await Task.Delay(animation.Delay);
                }
            }
            return true;
        }
    }
}
