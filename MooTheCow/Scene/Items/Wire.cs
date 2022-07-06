using System;

namespace MooTheCow
{
    class Wire : IItem
    {
        public char Image { get; } = '=';

        public ConsoleColor Color { get; } = ConsoleColor.Black;
    }
}
