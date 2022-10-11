using System.Drawing;

namespace MooTheCow
{
    interface IDrawable
    {
        public bool Contains(Point pt);
        public Rectangle Boundary { get; set; }
        public IObjectTile[,] ObjectTiles { get; set; }
        public int? LayerOverride { get; set; }
        public int GetLayer();
        public void AdjustLocation(Point offset);
    }
}
