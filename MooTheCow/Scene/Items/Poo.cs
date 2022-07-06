using System;
using System.Threading;
using System.Threading.Tasks;

namespace MooTheCow
{
    class Poo : IItem
    {
        private Scene _scene;
        private int _x;
        private int _y;
        public char Image { get; } = '\u058E';
        public ConsoleColor Color { get; } = ConsoleColor.DarkGray;

        public Poo(Scene scene, int x, int y)
        {
            _scene = scene;
            _x = x;
            _y = y;
            DecomposePoo();
        }


        private async void DecomposePoo()
        {
            await Task.Factory.StartNew(() => Thread.Sleep(15000));
            _scene.Tiles[_x, _y].Item = null;
            //_scene.renderTile(_x, _y);

            for(int yi = -1; yi < 2; yi++)
            {
                for(int xi = -1; xi<2; xi++)
                {
                    if (_scene.Tiles[_x + xi, _y + yi].GetType() == typeof(DirtTile)){
                        _scene.Tiles[_x + xi, _y + yi] = new GrassTile();
                        //_scene.renderTile(_x + xi, _y + yi);
                    }
                }
            }
        }
    }
}
