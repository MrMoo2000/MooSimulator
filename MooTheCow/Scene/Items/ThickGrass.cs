using System;

namespace MooTheCow
{
    class ThickGrass : IItem
    {
        public char Image { get; } = '*';
        public ConsoleColor Color { get; } = ConsoleColor.DarkGreen;
    }
}
