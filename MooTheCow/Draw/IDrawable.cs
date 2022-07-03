using System.Drawing;

namespace MooTheCow
{
    interface IDrawable
    {
        public Rectangle Boundary { get; set; }
        public IObjectTile[,] ObjectTiles { get; set; }
        public int GetLayer();
        public void AdjustLocation(Point offset);
    }
}
