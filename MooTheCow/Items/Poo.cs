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

        public Poo(Scene scene, int x, int y)
        {
            _scene = scene;
            _x = x;
            _y = y;
            DecomposePoo();
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkGray;
        }

        public string GetVisual()
        {
            return "\u058E";
        }

        private async void DecomposePoo()
        {
            await Task.Factory.StartNew(() => Thread.Sleep(15000));
            _scene.Tiles[_x, _y].item = null;
            _scene.renderTile(_x, _y);

            for(int yi = -1; yi < 2; yi++)
            {
                for(int xi = -1; xi<2; xi++)
                {
                    if (_scene.Tiles[_x + xi, _y + yi].GetType() == typeof(DirtTile)){
                        _scene.Tiles[_x + xi, _y + yi] = new GrassTile();
                        _scene.renderTile(_x + xi, _y + yi);
                    }
                }
            }
        }
    }
}
