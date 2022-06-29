﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MooTheCow
{
    static class Display
    {
        private static Scene _currentScene;
        private static List<IDrawable> _drawnDrawables = new List<IDrawable>();

        public static int Horizon { get; set; }

        public static void SetScene(Scene scene)
        {
            _currentScene = scene;
            RenderScene();
        }

        private static void RenderScene()
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.BackgroundColor = _currentScene.Tiles[x, y].GetColor();
                    Console.SetCursorPosition(x, y);
                    if (_currentScene.Tiles[x, y].item != null)
                    {
                        Console.ForegroundColor = _currentScene.Tiles[x, y].item.GetColor();
                        Console.Write(_currentScene.Tiles[x, y].item.GetVisual());
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write("WELCOME TO MOO SIMULATOR 2000!");
            Console.SetCursorPosition(5, Console.WindowHeight - 1);
            Console.Write("Press \u2195\u2194 to move | SPACEBAR to Moo | E to eat grass | P to poop | X to exit");

            UpdateStomachUI(0);
        }

        public static void UpdateStomachUI(int stomachLevel)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.WindowWidth - 16, Console.WindowHeight - 1);
            Console.Write($"Stomach: {stomachLevel * 5}%  ");
        }

        public static void Draw(IDrawable drawThis)
        {
            var drawablesToDraw = getOverlappingDrawables(drawThis);
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
            _drawnDrawables.Remove(eraseThis);
            var drawablesToDraw = getOverlappingDrawables(eraseThis);
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
        private static List<IDrawable> getOverlappingDrawables(IDrawable drawableToCheck, Point newLocation = new Point())
        {
            var bound = drawableToCheck.Boundary;
            if (!newLocation.IsEmpty)
            {
                bound.Location = newLocation;
            }
            var overlappingDrawables = new List<IDrawable>();
            foreach(Drawable drawn in _drawnDrawables)
            {
                if (!ReferenceEquals(drawn, drawableToCheck) && drawn.Boundary.IntersectsWith(bound))
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
            var drawableOneMaxY = drawableOne.Boundary.Y + drawableOne.Boundary.Height;
            var drawableTwoMaxY = drawableTwo.Boundary.Y + drawableTwo.Boundary.Height;

            if(drawableOneMaxY > drawableTwoMaxY)
            {
                return -1;
            }
            else if(drawableOneMaxY < drawableTwoMaxY)
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
                Console.BackgroundColor = _currentScene.Tiles[drawAt.X, drawAt.Y].GetColor();
                if (_currentScene.Tiles[drawAt.X, drawAt.Y].item != null)
                {
                    Console.ForegroundColor = _currentScene.Tiles[drawAt.X, drawAt.Y].item.GetColor();
                    Console.Write(_currentScene.Tiles[drawAt.X, drawAt.Y].item.GetVisual());
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
        public static bool DrawableCollision(IDrawable drawThis, Point destination)
        {
            var y = destination.Y + drawThis.Boundary.Height;

            var overlappingDrawables = getOverlappingDrawables(drawThis, destination);

            foreach(IDrawable overlappingDrawable in overlappingDrawables)
            {
                var olapY = overlappingDrawable.Boundary.Y + overlappingDrawable.Boundary.Height;
                if(olapY == y)
                {
                    return true;
                }
            }
            return false;
        }
    }

}