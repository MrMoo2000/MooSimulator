using System;

namespace MooTheCow
{
    class DirtTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.DarkYellow;
    }
}
