using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MooTheCow
{
    class StringToObjectTile
    {
        public IObjectTile[,] ObjectTiles { get; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public int WriteX { get; set; } = 0;
        public int WriteY { get; set; } = 0;

        public StringToObjectTile(int width, int height)
        {
            ObjectTiles = new IObjectTile[width, height];
        }

        public void Write(string objectLine)
        {
            char colorEscapeBegin = '['; // TODO add the escape to config 
            char colorEscapeEnd = ']';
            const string FG_TAG = "fg=";
            const string BG_TAG = ";bg=";
            ConsoleColor backgroundColor = ConsoleColor.Black;
            ConsoleColor foregroundColor = ConsoleColor.White;


            for (int counter = 0; counter < objectLine.Length; counter++)
            {
                if (objectLine[counter].Equals('?'))
                {
                    ObjectTiles[WriteX, WriteY] = null;
                    WriteX++;
                }
                else if (objectLine[counter] == colorEscapeBegin)
                {
                    int colorEscapeEndIndex = objectLine.IndexOf(colorEscapeEnd, counter);
                    string subs = objectLine.Substring(counter, colorEscapeEndIndex - counter);
                    string fg = subs.Substring(subs.IndexOf(FG_TAG) + FG_TAG.Length, subs.IndexOf(BG_TAG) - BG_TAG.Length);
                    string bg = subs.Substring(subs.IndexOf(BG_TAG) + BG_TAG.Length);

                    backgroundColor = (ConsoleColor)int.Parse(bg);
                    foregroundColor = (ConsoleColor)int.Parse(fg);
                    counter = objectLine.IndexOf(colorEscapeEnd, counter);
                }
                else
                {
                    ObjectTiles[WriteX, WriteY] = new ObjectTile()
                    {
                        BackgroundColor = backgroundColor,
                        ForegroundColor = foregroundColor,
                        Image = objectLine[counter]
                    };
                    WriteX++;
                }
            }
        }

        public void nextLine()
        {
            WriteX = 0;
            WriteY++;
        }

    }
}
