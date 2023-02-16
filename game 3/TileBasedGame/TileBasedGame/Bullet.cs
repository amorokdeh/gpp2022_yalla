
namespace TileBasedGame
{
    class Bullet:GameObject
    {
        public GameObject GameObject;
       
        public Bullet(string name, GameObject player, int w, int h):base(name, w, h)
        {
            GameObject = player;
            this.Name = name;

            VelY = 0;
            VelX = 0;
        }
    }
}
