
namespace TileBasedGame
{
    class Tile:GameObject
    {
        public Tile(string name, int w, int h, int x, int y) : base(name, w, h)
        {
            this.Name = name;
            PosX = x;
            PosY = y;
        }
    }
}
