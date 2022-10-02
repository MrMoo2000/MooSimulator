using System;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace MooTheCow
{
    class Poo : IItem
    {
        public char Image { get; } = '\u058E';
        public ConsoleColor Color { get; } = ConsoleColor.Black;

        public Poo()
        {
        }


        public async void DecomposePoo(Point location)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(10000));
            SceneManager.Scene.Tiles[location.X, location.Y].Item = null;
            Display.DrawSceneTile(location);

            for (int yi = -1; yi < 2; yi++)
            {
                for(int xi = -1; xi<2; xi++)
                {
                    if (SceneManager.Scene.Tiles[location.X + xi, location.Y + yi].GetType() == typeof(DirtTile)){
                        SceneManager.UpdateSceneTile(new Point(location.X + xi, location.Y + yi), new GrassTile());   
                    }
                }
            }
        }
    }
}
