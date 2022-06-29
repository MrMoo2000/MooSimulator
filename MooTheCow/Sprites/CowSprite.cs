using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class CowSprite : ISprite
    {
        private ConsoleColor _cowBackgroundColor;
        private ConsoleColor _cowForegroundColor;
        public IObjectTile[,] FacingLeft { get; }
        public IObjectTile[,] FacingRight { get; }

        public CowSprite(ConsoleColor cowBackgroundColor, ConsoleColor cowForegroundColor)
        {
            _cowBackgroundColor = cowBackgroundColor;
            _cowForegroundColor = cowForegroundColor;
            FacingLeft = getFacingLeft();
            FacingRight = getFacingRight();
        }

        private IObjectTile[,] getFacingLeft()
        {
            var stringToObjectTile = new StringToObjectTile(15, 5);

            stringToObjectTile.BackgroundColor = ConsoleColor.DarkGray;
            stringToObjectTile.ForegroundColor = ConsoleColor.White;

            stringToObjectTile.Write("^??^");

            stringToObjectTile.nextLine();
            stringToObjectTile.BackgroundColor = _cowBackgroundColor;
            stringToObjectTile.ForegroundColor = _cowForegroundColor;

            stringToObjectTile.Write("(oo)\\");

            stringToObjectTile.nextLine();
            stringToObjectTile.BackgroundColor = ConsoleColor.Red;
            stringToObjectTile.ForegroundColor = ConsoleColor.Black;
            stringToObjectTile.Write("(__)");
            stringToObjectTile.BackgroundColor = _cowBackgroundColor;
            stringToObjectTile.ForegroundColor = _cowForegroundColor;
            stringToObjectTile.Write("\\       )\\");

            stringToObjectTile.nextLine();
            stringToObjectTile.Write("????||----w |?\\");
            stringToObjectTile.nextLine();
            stringToObjectTile.Write("????||?????||");

            return stringToObjectTile.ObjectTiles;
        }
        private IObjectTile[,] getFacingRight()
        {
            var stringToObjectTile = new StringToObjectTile(15, 5);

            stringToObjectTile.BackgroundColor = ConsoleColor.DarkGray;
            stringToObjectTile.ForegroundColor = ConsoleColor.White;

            stringToObjectTile.Write("???????????^??^");

            stringToObjectTile.nextLine();
            stringToObjectTile.BackgroundColor = _cowBackgroundColor;
            stringToObjectTile.ForegroundColor = _cowForegroundColor;

            stringToObjectTile.Write("??????????/(oo)");

            stringToObjectTile.nextLine();
            stringToObjectTile.BackgroundColor = _cowBackgroundColor;
            stringToObjectTile.ForegroundColor = _cowForegroundColor;
            stringToObjectTile.Write("?/(       /");
            stringToObjectTile.BackgroundColor = ConsoleColor.Red;
            stringToObjectTile.ForegroundColor = ConsoleColor.Black;
            stringToObjectTile.Write("(__)");


            stringToObjectTile.BackgroundColor = _cowBackgroundColor;
            stringToObjectTile.ForegroundColor = _cowForegroundColor;
            stringToObjectTile.nextLine();
            stringToObjectTile.Write("/?| w----||????");
            stringToObjectTile.nextLine();
            stringToObjectTile.Write("??||?????||");

            return stringToObjectTile.ObjectTiles;
        }
    }
}
