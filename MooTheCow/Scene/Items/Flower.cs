using System;

namespace MooTheCow
{
    class Flower : IItem
    {
        public char Image { get; } = '@';

        public ConsoleColor Color { get; private set; } = ConsoleColor.Black;

        public Flower()
        {
            SetFlowerColor();
        }

        private void SetFlowerColor()
        {
            switch (new Random().Next(1, 4))
            {
                case 1:
                    Color = ConsoleColor.Red;
                    break;

                case 2:
                    Color = ConsoleColor.Blue;
                    break;

                case 3:
                    Color = ConsoleColor.Yellow;
                    break;
            }
        }

    }
}
