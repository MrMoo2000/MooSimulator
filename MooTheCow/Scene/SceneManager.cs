using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace MooTheCow
{
    static class SceneManager
    {
        public static Scene Scene;

        public static Type[] GetTileTypes(Point location, int length = 1)
        {
            var tiles = Scene.Tiles;
            Type[] types = new Type[length];

            for (int counter = 0; counter < length; counter++)
            {
                types[counter] = Scene.Tiles[counter+location.X, location.Y].GetType();
            }
            return types;
        }
        // Set tile at location
        public static void UpdateSceneTile(Point location, ITile tile)
        {
            Scene.Tiles[location.X, location.Y] = tile;
            Display.UpdateSceneTile(location);
        }
    }
}
