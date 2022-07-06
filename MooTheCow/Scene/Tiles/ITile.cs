using System;

namespace MooTheCow
{
    interface ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; }
    }
}
