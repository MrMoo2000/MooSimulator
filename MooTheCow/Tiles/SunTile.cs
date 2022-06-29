using System;

namespace MooTheCow
{
    class SunTile : ITile
    {
        public IItem item { get; set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Yellow;
        }
    }
}
