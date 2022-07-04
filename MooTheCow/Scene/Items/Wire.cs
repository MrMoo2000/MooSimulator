using System;

namespace MooTheCow
{
    class Wire : IItem
    {
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Black;
        }

        public string GetVisual()
        {
            return "=";
        }
    }
}
