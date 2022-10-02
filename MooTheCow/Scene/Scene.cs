using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class Scene
    {
        public ITile[,] Tiles { get; set; }

        public Scene()
        {
            fillTiles();
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
                    else if(y >= SceneManager.Horizon)
                    {
                        Tiles[x, y] = new GrassTile();
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
            for (int fenceY = SceneManager.Horizon - 3; fenceY < SceneManager.Horizon; fenceY++)
            {
                fenceRows++;
                int gapCount = 4;
                bool drawTwo = false;
                for (int fenceX = 0; fenceX < Console.WindowWidth; fenceX++)
                {
                    if(fenceRows == 2)
                    {
                        Tiles[fenceX, fenceY] = new DirtTile() { Item = new Wire()};
                    }
                    else
                    {
                        gapCount++;
                        if(gapCount == 6)
                        {
                            Tiles[fenceX, fenceY] = new DirtTile() { Item = new FencePost()};
                            gapCount = 1;
                            drawTwo = true;
                        }
                        else if(drawTwo == true)
                        {
                            Tiles[fenceX, fenceY] = new DirtTile() { Item = new FencePost() };
                            drawTwo = false;
                        }
                    }
                }
            }
            
        }
    }
}
