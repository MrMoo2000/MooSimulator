using System;

namespace MooTheCow
{
    class CloudTile : ITile
    {
        public IItem item { get; set; }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}
