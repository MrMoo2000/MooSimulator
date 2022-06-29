using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    interface IAnimation
    {
        public List<IObjectTile[,]> Frames { get; }
        public int Delay { get; }
    }
}
