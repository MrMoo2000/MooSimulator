using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class Animation : IAnimation
    {
        public List<IObjectTile[,]> Frames { get; }
        public int Delay { get; }
        public Animation(List<IObjectTile[,]> frames, int delay)
        {
            Frames = frames;
            Delay = delay;
        }
    }
}
