using System;

namespace MooTheCow
{
    class FencePost : IItem
    {
        public char Image { get; } = '|';

        public ConsoleColor Color { get; } = ConsoleColor.Black;
    }
}
