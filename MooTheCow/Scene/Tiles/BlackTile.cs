using System;

namespace MooTheCow
{
    class BlackTile : ITile
    {
        public IItem item {get;set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Black;
        }

    }
}
