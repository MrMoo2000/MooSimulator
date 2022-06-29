using System;

namespace MooTheCow
{
    class ThickGrass : IItem
    {
        public string GetVisual()
        {
            return "*";
        }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkGreen;
        }
    }
}
