using System;

namespace MooTheCow
{
    interface ITile
    {
        public IItem item { get; set; }
        ConsoleColor GetColor();
    }
}
