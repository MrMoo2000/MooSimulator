using System;

namespace MooTheCow
{
    class SunTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.Yellow;
    }
}
