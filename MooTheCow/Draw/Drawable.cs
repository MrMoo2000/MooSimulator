using System.Drawing;
using System.Diagnostics;

namespace MooTheCow
{
    class Drawable : IDrawable
    {
        public Rectangle Boundary { get; set; }
        public IObjectTile[,] ObjectTiles { get; set; }
        public int? LayerOverride { get; set; }
        public bool Contains(Point pt) { return Boundary.Contains(pt); }

        public int GetLayer()
        {
            if(LayerOverride != null)
            {
                return (int)LayerOverride;
            }
            return Boundary.Location.Y + Boundary.Height;
        }
        public void AdjustLocation(Point offset)
        {
            var bound = Boundary;
            var loc = Boundary.Location;
            loc.Offset(offset);
            bound.Location = loc;
            Boundary = bound;
        }
        
    }
}
