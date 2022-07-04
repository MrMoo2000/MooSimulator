using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MooTheCow
{
    public enum AnimationTypes
    {
        FaceRight = 0,
        FaceLeft = 1,
        WalkRight = 2,
        WalkLeft = 3,
        HeadDownRight = 4,
        HeadUpRight = 5,
        HeadDownLeft = 6,
        HeadUpLeft = 7,
        Death = 8
    }
    static class Animator
    {

        public static void DrawSingleFrame(AnimationTypes animationType, IAnimatable animatable)
        {
            IAnimation animation = animatable.Animations[animationType];
            IDrawable drawable = animatable.Drawable;

            var frame = animation.Frames[0];
            drawable.ObjectTiles = frame;
            Display.Draw(drawable);
        }
        public static async Task<bool> Animation(AnimationTypes animationType, IAnimatable animatable)
        {
            IAnimation animation = animatable.Animations[animationType];
            IDrawable drawable = animatable.Drawable;

            int counter = 0;
            foreach (var frame in animation.Frames)
            {
                if(animatable.AnimationStop == true)
                {
                    return true;
                }
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
