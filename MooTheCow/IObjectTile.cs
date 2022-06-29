using System;
namespace MooTheCow
{
    interface IObjectTile
    {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public char Image { get; set; }
    }
}
