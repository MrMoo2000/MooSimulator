using System;

namespace MooTheCow
{
    class GrassTile : ITile
    {
        public IItem Item { get; set; }
        public ConsoleColor Color { get; } = ConsoleColor.Green;

        private readonly int _flowerChance = 5;
        private readonly int _thickGrassChance = 15;
        private Func<int> _itemChance = () => new Random().Next(1, 100);

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Green;
        }
        public GrassTile()
        {
            Item = ChooseDecoration();
        }

        private IItem ChooseDecoration()
        {
            IItem item = null;

            if (_itemChance() <= _flowerChance) { item = new Flower(); }
            else if (_itemChance() <= _thickGrassChance) { item = new ThickGrass(); }

            return item;
        }
    }
}
