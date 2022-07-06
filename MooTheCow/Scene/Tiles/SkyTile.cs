using System;

namespace MooTheCow
{
    class SkyTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.Blue;
    }
}
