using System;
using System.Collections.Generic;
using System.Text;

namespace MooTheCow
{
    class PlayerCow
    {
        private Cow _playerCow;
        public int PlayerCowX { get; }
        public int PlayerCowY { get; }
        public PlayerCow(int playerCowX = 0, int playerCowY = 0)
        {
            PlayerCowX = playerCowX;
            PlayerCowY = playerCowY;
        }
    }
}
