namespace MooTheCow
{
    interface ISprite
    {
        public IObjectTile[,] FacingLeft { get; }
        public IObjectTile[,] FacingRight { get; }
    }
}
