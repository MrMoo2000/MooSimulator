using System;

namespace MooTheCow
{
    class GrassTile : ITile
    {
        public IItem item { get; set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Green;
        }
    }
}
