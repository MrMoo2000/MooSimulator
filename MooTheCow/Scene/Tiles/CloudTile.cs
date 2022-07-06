using System;

namespace MooTheCow
{
    class CloudTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.White;
    }
}
