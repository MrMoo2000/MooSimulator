using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MooTheCow
{
    class Validation
    {
        public static bool ValidateMove(ConsoleKey keyPressed, IDrawable drawable)
        {
            Point validatePoint = drawable.Boundary.Location;

            validatePoint.Offset(InputManager.KeyToPoint[keyPressed]);
            if (
                (validatePoint.X >= Console.WindowWidth - drawable.Boundary.Width + 1) ||
                (validatePoint.X < 0) ||
                (validatePoint.Y <= SceneManager.Horizon - drawable.Boundary.Height) ||
                (validatePoint.Y >= Console.WindowHeight - 6) ||
                Display.DrawableCollision(drawable, validatePoint)
                )
            {
                return false;
            }
            return true;
        }
        public static bool ValidateDrawableInWindow(IDrawable drawable)
        {
            Point validatePoint = drawable.Boundary.Location;

            if (
                (validatePoint.X >= Console.WindowWidth - drawable.Boundary.Width) ||
                (validatePoint.X < 0) ||
                (validatePoint.Y <= 0) ||
                (validatePoint.Y >= Console.WindowHeight)
                )
            {
                return false;
            }
            return true;
        }
        public static bool ValidateCreateItem(Point location, IItem item)
        {
            var tiles = SceneManager.Scene.Tiles;

            if (location.X < tiles.GetLength(0) && location.Y < tiles.GetLength(1))
            {
                var existingItem = tiles[location.X, location.Y];
                if (existingItem != null && existingItem.GetType().Equals(item))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
