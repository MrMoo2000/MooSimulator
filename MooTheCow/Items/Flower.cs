using System;

namespace MooTheCow
{
    class Flower : IItem
    {
        private ConsoleColor _flowerColor;

        public ConsoleColor GetColor()
        {
            if(_flowerColor == ConsoleColor.Black) // Default ConsoleColor
            {
                Random rnd = new Random();
                int color = rnd.Next(1, 4);
                switch (color)
                {
                    case 1:
                        _flowerColor = ConsoleColor.Red;
                        break;

                    case 2:
                        _flowerColor = ConsoleColor.Blue;
                        break;

                    case 3:
                        _flowerColor = ConsoleColor.Yellow;
                        break;
                }
                    
            }
            return _flowerColor;
        }

        public string GetVisual()
        {
            return "@";
        }
    }
}
