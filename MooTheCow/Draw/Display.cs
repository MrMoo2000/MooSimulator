using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace MooTheCow
{
    static class Display
    {
        private static List<IDrawable> _drawnDrawables = new List<IDrawable>();
        private static readonly object _cursorLock = new object();


        public static void RenderScene()
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.BackgroundColor = SceneManager.Scene.Tiles[x, y].Color;
                    Console.SetCursorPosition(x, y);
                    if (SceneManager.Scene.Tiles[x, y].Item != null)
                    {
                        Console.ForegroundColor = SceneManager.Scene.Tiles[x, y].Item.Color;
                        Console.Write(SceneManager.Scene.Tiles[x, y].Item.Image);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write("WELCOME TO MOO SIMULATOR");
            Console.SetCursorPosition(5, Console.WindowHeight - 1);
            Console.Write("Press \u2195\u2194 to move | SPACEBAR to Moo | E to eat grass | P to poop | X to exit");
        }

        public static void Draw(IDrawable drawThis)
        {
            lock (_cursorLock)
            {
                var drawablesToDraw = GetOverlappingDrawables(drawThis.Boundary);
                drawablesToDraw.Add(drawThis);
                drawablesToDraw.Sort(CompareOverlappingByClosest);

                var drawing = CreateDrawingWithOverlap(drawThis.Boundary, drawablesToDraw);

                for (int y = 0; y < drawing.GetLength(1); y++)
                {
                    for (int x = 0; x < drawing.GetLength(0); x++)
                    {
                        DrawTile(drawing[x, y], new Point { X = x + drawThis.Boundary.X, Y = y + drawThis.Boundary.Y });
                    }
                }
                _drawnDrawables.Add(drawThis);
            }
        }
        private static IObjectTile[,] CreateDrawingWithOverlap(Rectangle boundary, List<IDrawable> drawablesToDraw)
        {
            var drawing = new IObjectTile[boundary.Width, boundary.Height];

            for (int y = 0; y < boundary.Height; y++)
            {
                for (int x = 0; x < boundary.Width; x++)
                {
                    IObjectTile objectTile = null;
                    foreach (IDrawable drawable in drawablesToDraw)
                    {
                        var pointToCheck = new Point(boundary.X + x, boundary.Y + y);
                        var tileToCheck = drawable.Boundary.Location;
                        tileToCheck = pointToCheck - (Size)tileToCheck;
                        if (drawable.Boundary.Contains(pointToCheck) && drawable.ObjectTiles[tileToCheck.X, tileToCheck.Y] != null)
                        {
                            objectTile = drawable.ObjectTiles[tileToCheck.X, tileToCheck.Y];
                            break;
                        }
                    }
                    drawing[x, y] = objectTile;
                }
            }
            return drawing;
        }

        public static void Erase(IDrawable eraseThis)
        {
            lock (_cursorLock)
            {
                _drawnDrawables.Remove(eraseThis);
                var drawablesToDraw = GetOverlappingDrawables(eraseThis.Boundary);
                drawablesToDraw.Sort(CompareOverlappingByClosest);

                var drawing = CreateDrawingWithOverlap(eraseThis.Boundary, drawablesToDraw);

                for (int y = 0; y < drawing.GetLength(1); y++)
                {
                    for (int x = 0; x < drawing.GetLength(0); x++)
                    {
                        DrawTile(drawing[x, y], new Point { X = x + eraseThis.Boundary.X, Y = y + eraseThis.Boundary.Y });
                    }
                }
            }
        }
        private static List<IDrawable> GetOverlappingDrawables(Rectangle boundary)
        {
            var overlappingDrawables = new List<IDrawable>();
            foreach (Drawable drawn in _drawnDrawables)
            {
                if (drawn.Boundary.IntersectsWith(boundary))
                {
                    overlappingDrawables.Add(drawn);
                }
            }
            return overlappingDrawables;
        }
        private static int CompareOverlappingByClosest(IDrawable drawableOne, IDrawable drawableTwo)
        {
            drawableOne ??= new Drawable { Boundary = new Rectangle(0, 0, 0, 0) };
            drawableTwo ??= new Drawable { Boundary = new Rectangle(0, 0, 0, 0) };

            if(drawableOne.GetLayer() > drawableTwo.GetLayer())
            {
                return -1;
            }
            else if(drawableOne.GetLayer() < drawableTwo.GetLayer())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static void DrawTile(IObjectTile tile, Point drawAt)
        {
            Console.SetCursorPosition(drawAt.X, drawAt.Y);
            if (tile != null)
            {
                Console.BackgroundColor = tile.BackgroundColor;
                Console.ForegroundColor = tile.ForegroundColor;
                Console.Write(tile.Image);
            }
            else
            {
                Console.BackgroundColor = SceneManager.Scene.Tiles[drawAt.X, drawAt.Y].Color;
                if (SceneManager.Scene.Tiles[drawAt.X, drawAt.Y].Item != null)
                {
                    Console.ForegroundColor = SceneManager.Scene.Tiles[drawAt.X, drawAt.Y].Item.Color;
                    Console.Write(SceneManager.Scene.Tiles[drawAt.X, drawAt.Y].Item.Image);
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
        public static bool DrawableCollision(IDrawable drawThis, Point destination)
        {
            lock (_cursorLock)
            {
                var y = destination.Y + drawThis.Boundary.Height;

                var dest = drawThis.Boundary;
                dest.Location = destination;

                var overlappingDrawables = GetOverlappingDrawables(dest);
                overlappingDrawables.Remove(drawThis);

                foreach (IDrawable overlappingDrawable in overlappingDrawables)
                {
                    var olapY = overlappingDrawable.Boundary.Y + overlappingDrawable.Boundary.Height;
                    if (olapY == y)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static void DrawSceneTile(Point location)
        {
            lock (_cursorLock)
            {
                var boundary = new Rectangle(location.X, location.Y, 1, 1);
                IObjectTile[,] objectTile = CreateDrawingWithOverlap(boundary, _drawnDrawables);
                DrawTile(objectTile[0,0], location);
            }
        }
    }

}
