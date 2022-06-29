using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow.Animations
{

    class CowWalkLeftAnimation 
    {
        public List<IObjectTile[,]> Frames;

        public int Delay;

        private ISprite _cowSprite;

        public CowWalkLeftAnimation(ISprite cowSprite)
        {
            _cowSprite = cowSprite;
        }

        public void getFrameOne()
        {
            var objectTiles = _cowSprite.FacingRight;
        }
    }
}
