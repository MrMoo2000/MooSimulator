using System;

namespace MooTheCow
{
    class DirtTile : ITile
    {
        public IItem item { get; set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkYellow;
        }
    }
}
