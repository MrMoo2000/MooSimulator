using System;

namespace MooTheCow
{
    class BlackTile : ITile
    {
        public IItem Item {get;set; }
        public ConsoleColor Color { get; } = ConsoleColor.Black;

    }
}
