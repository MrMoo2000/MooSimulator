using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class Scene
    {
        private int _horizon;
        public ITile[,] Tiles { get; set; }

        public Scene(int horizon)
        {
            _horizon = horizon;
        }

        public void fillTiles()
        {
            Tiles = new ITile[Console.WindowWidth, Console.WindowHeight];

            Random rnd = new Random();
            
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    if(y >= Console.WindowHeight - 2)
                    {
                        Tiles[x, y] = new BlackTile();
                    }
                    else if(y >= _horizon)
                    {
                        var grassTile = new GrassTile();
                        if (rnd.Next(1, 10) < 2)
                        {
                            grassTile.item = new ThickGrass();
                        }
                        if (rnd.Next(1, 20) < 2)
                        {
                            grassTile.item = new Flower();
                        }
                        Tiles[x, y] = grassTile;
                    }
                    else
                    {
                        Tiles[x, y] = new SkyTile();
                    }
                }
            }

            for (int sunY = 3; sunY < 6; sunY++)
            {
                for (int sunX = 0; sunX < 6; sunX++)
                {
                    Tiles[Console.WindowWidth - 12 + sunX, sunY] = new SunTile();
                }
            }

            for (int cloudY = 3; cloudY < 5; cloudY++)
            {
                for (int cloudX = 0; cloudX < 12; cloudX++)
                {
                    Tiles[cloudX + cloudY*7, cloudY] = new CloudTile();
                }
            }
            for (int cloudY = 4; cloudY < 6; cloudY++)
            {
                for (int cloudX = 0; cloudX < 12; cloudX++)
                {
                    Tiles[cloudX + 92 - cloudY * 5, cloudY] = new CloudTile();
                }
            }

            int fenceRows = 0;
            for (int fenceY = _horizon - 3; fenceY < _horizon; fenceY++)
            {
                fenceRows++;
                int gapCount = 4;
                bool drawTwo = false;
                for (int fenceX = 0; fenceX < Console.WindowWidth; fenceX++)
                {
                    if(fenceRows == 2)
                    {
                        Tiles[fenceX, fenceY] = new DirtTile() { item = new Wire()};
                    }
                    else
                    {
                        gapCount++;
                        if(gapCount == 6)
                        {
                            Tiles[fenceX, fenceY] = new DirtTile() { item = new FencePost()};
                            gapCount = 1;
                            drawTwo = true;
                        }
                        else if(drawTwo == true)
                        {
                            Tiles[fenceX, fenceY] = new DirtTile() { item = new FencePost() };
                            drawTwo = false;
                        }
                    }
                    //Tiles[cloudX + 92 - cloudY * 5, cloudY] = new CloudTile();
                }
            }
            
        }

        public void renderTile(int x, int y)
        {
            Console.BackgroundColor = Tiles[x, y].GetColor();
            Console.SetCursorPosition(x, y);
            if (Tiles[x, y].item != null)
            {
                Console.ForegroundColor = Tiles[x,y].item.GetColor();
                Console.Write(Tiles[x, y].item.GetVisual());
            }
            else
            {
                Console.Write(" ");
            }
        }

        public void updateStomachUI(int stomachLevel)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.WindowWidth - 16, Console.WindowHeight - 1);
            Console.Write($"Stomach: {stomachLevel*5}%  ");
        }
    }
}
