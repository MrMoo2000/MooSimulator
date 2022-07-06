using System;

namespace MooTheCow
{
    class GrassTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.Green;

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Green;
        }
        public GrassTile()
        {
            var itemChance = new Random().Next(1, 20);

            if (itemChance < 2)
            {
                // Flower
                Item = new Flower();
            }
            else if (itemChance < 4)
            {
                // Thick Grass 
                Item = new ThickGrass();
            }
            else
            {
                Item = null;
            }
        }
    }
}
