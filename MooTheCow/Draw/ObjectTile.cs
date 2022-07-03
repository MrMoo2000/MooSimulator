using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class ObjectTile : IObjectTile
    {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public char Image { get; set; }
    }
}