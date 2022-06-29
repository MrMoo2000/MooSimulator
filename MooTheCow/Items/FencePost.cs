using System;

namespace MooTheCow
{
    class FencePost : IItem
    {
        public ConsoleColor GetColor()
        {
            return ConsoleColor.Black;
        }

        public string GetVisual()
        {
            return "|";
        }
    }
}
