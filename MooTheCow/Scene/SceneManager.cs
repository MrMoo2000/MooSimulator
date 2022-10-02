using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace MooTheCow
{
    static class SceneManager
    {
        public static Scene Scene { get; private set; }
        public static int Horizon { get; set; }

        static SceneManager()
        {
            Horizon = Console.WindowHeight - (Console.WindowHeight / 3 + Config.HorizonOffset);
            Scene = new Scene();
            Display.RenderScene();
        }

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
        public static void RedrawTile(Point location)
        {

        }

        /// <summary>
        /// Set x,y point of scene to specific tile 
        /// </summary>
        /// <param name="location">the x,y cord of scene to set tile</param>
        /// <param name="tile">tile to set to</param>
        public static void UpdateSceneTile(Point location, ITile tile)
        {
            Scene.Tiles[location.X, location.Y] = tile;
            Display.DrawSceneTile(location);
        }
        /// <summary>
        /// Set area of scene to specific tile 
        /// </summary>
        /// <param name="area">area to set a tile to</param>
        /// <param name="tile">Tile to set area to</param>
        public static void UpdateSceneTiles(Rectangle area, ITile tile)
        {
            Scene.Tiles[area.X, area.Y] = tile;
            Display.DrawSceneTile(area.Location);
        }
    }
}
