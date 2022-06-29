using System;

namespace MooTheCow
{
    class SkyTile : ITile
    {
        public IItem item { get; set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Blue;
        }
    }
}
