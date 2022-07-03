using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MooTheCow
{
    class Cow
    {
        public int cowX { get; set; }
        public int cowY { get; set; }
        public int cowWidth { get; } = 15;
        public int cowHeight { get; } = 5;

        private Scene _scene;

        public IDrawable CowDrawable { get; set; }
        public bool FacingLeft { get; set; }

        private const int STOMACH_MIN = 0;
        private const int STOMACH_MAX = 20;
        private int stomachLevel = 0;

        //public ISprite Sprite { get; }
        public Cow(Scene scene)
        {
            _scene = scene;
            //Sprite = new CowSprite(ConsoleColor.White, ConsoleColor.Black);
        }
        public void drawMoo()
        {
            if(FacingLeft == true)
            {
                drawMooLeft();
            }
            else
            {
                drawMooRight();
            }
        }
        private void drawMooRight()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int drawX = cowX + cowWidth;
            int drawY = cowY;
            Console.SetCursorPosition(drawX, drawY + 2);
            Console.BackgroundColor = _scene.Tiles[drawX - 1, drawY + 2].GetColor();
            Console.Write("/");

            Console.BackgroundColor = _scene.Tiles[drawX + 1, drawY + 1].GetColor();
            Console.SetCursorPosition(drawX + 1, drawY + 1);
            Console.Write("M");

            for (int i = 0; i < 4; i++)
            {
                Console.BackgroundColor = _scene.Tiles[drawX + 2 + i, drawY + 1].GetColor();
                Console.SetCursorPosition(drawX + 2 + i, drawY + 1);
                Console.Write("o");
            }
            Thread.Sleep(750);
            Console.BackgroundColor = _scene.Tiles[drawX, drawY + 2].GetColor();
            Console.SetCursorPosition(drawX, drawY + 2);
            if (_scene.Tiles[drawX, drawY + 2].item != null)
            {
                Console.ForegroundColor = _scene.Tiles[drawX, drawY + 2].item.GetColor();
                Console.Write(_scene.Tiles[drawX, drawY + 2].item.GetVisual());
            }
            else
            {
                Console.Write(" ");
            }

            for (int i = 0; i < 5; i++)
            {
                Console.BackgroundColor = _scene.Tiles[drawX + 1 + i, drawY + 1].GetColor();
                Console.SetCursorPosition(drawX + 1 + i, drawY + 1);
                if (_scene.Tiles[drawX + 1 + i, drawY + 1].item != null)
                {
                    Console.ForegroundColor = _scene.Tiles[drawX + 1 + i, drawY + 1].item.GetColor();
                    Console.Write(_scene.Tiles[drawX + 1 + i, drawY + 1].item.GetVisual());
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }

        private void drawMooLeft()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(cowX - 1, cowY + 2);
            Console.BackgroundColor = _scene.Tiles[cowX - 1, cowY + 2].GetColor();
            Console.Write("\\");

            Console.BackgroundColor = _scene.Tiles[cowX - 6, cowY + 1].GetColor();
            Console.SetCursorPosition(cowX - 6, cowY + 1);
            Console.Write("M");

            for(int i = 0; i < 4; i++)
            {
                Console.BackgroundColor = _scene.Tiles[cowX - 5 + i, cowY + 1].GetColor();
                Console.SetCursorPosition(cowX - 5 + i, cowY + 1);
                Console.Write("o");
            }
            Thread.Sleep(750);
            Console.BackgroundColor = _scene.Tiles[cowX - 1, cowY + 2].GetColor();
            Console.SetCursorPosition(cowX - 1, cowY + 2);
            if (_scene.Tiles[cowX - 1, cowY + 2].item != null)
            {
                Console.ForegroundColor = _scene.Tiles[cowX - 1, cowY + 2].item.GetColor();
                Console.Write(_scene.Tiles[cowX - 1, cowY + 2].item.GetVisual());
            }
            else
            {
                Console.Write(" ");
            }

            for (int i = 0; i < 5; i++)
            {
                Console.BackgroundColor = _scene.Tiles[cowX - 6 + i, cowY + 1].GetColor();
                Console.SetCursorPosition(cowX - 6 + i, cowY + 1);
                if (_scene.Tiles[cowX - 6 + i, cowY + 1].item != null)
                {
                    Console.ForegroundColor = _scene.Tiles[cowX - 6 + i, cowY + 1].item.GetColor();
                    Console.Write(_scene.Tiles[cowX - 6 + i, cowY + 1].item.GetVisual());
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }

        public void drawPoo()
        {
            if(stomachLevel == STOMACH_MIN)
            {
                return;
            }

            int tileXOffset = 0;
            int tileYOffset = 4;
            if (FacingLeft)
            {
                tileXOffset = 14;
            }
            else
            {
                tileXOffset = 0;
            }
            _scene.Tiles[cowX + tileXOffset, cowY + tileYOffset].item = new Poo(_scene, cowX + tileXOffset, cowY + tileYOffset);
            //_scene.renderTile(cowX + tileXOffset, cowY + tileYOffset);

            stomachLevel = (stomachLevel - STOMACH_MIN <= 0) ? 0 : stomachLevel - 4;
           // _scene.updateStomachUI(stomachLevel);
        }


        public void drawEat(int eatingX, int eatingY)
        {
            //cleanScene();
            int[] animDelay = new int[]
            {
                125, 750, 125, 0
            };
            int[] animOrder = new int[]
            {
                1,2,1,0
            };

            for (int i = 0; i < 4; i++)
            {
                //cleanScene();

                //drawHead(FacingLeft, animOrder[i]);

                if (FacingLeft)
                {
                }
                else
                {
                   // drawBodyR();
                }
                Thread.Sleep(animDelay[i]);
                if(i == 1)
                {
                    for (int ii = 0; ii < 4; ii++)
                    {
                        _scene.Tiles[eatingX + ii, eatingY] = new DirtTile();
                       // _scene.renderTile(eatingX + ii, eatingY);
                    }
                }
            }
        }
        public void eatGrass()
        {
            int eatingX = 0;
            int eatingY = 0;

            if (FacingLeft)
            {
                eatingX = cowX;
                eatingY = cowY + cowHeight;
            }
            else
            {
                eatingX = cowX + cowWidth - 4;
                eatingY = cowY + cowHeight;
            }
            if(stomachLevel != STOMACH_MAX)
            {
                int grassAteCount = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (_scene.Tiles[eatingX + i, eatingY].GetType() == typeof(GrassTile))
                    {
                        grassAteCount++;
                    }
                }
                drawEat(eatingX, eatingY);
                stomachLevel = (stomachLevel + grassAteCount > STOMACH_MAX) ? STOMACH_MAX : stomachLevel + grassAteCount;
               // _scene.updateStomachUI(stomachLevel);
            }            
        }
    }
}
