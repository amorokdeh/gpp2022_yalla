
namespace TileBasedGame
{
    class EnemyBullet : GameObject
    {
        public GameObject GameObject;

        public EnemyBullet(string name, GameObject enemy, int w, int h) : base(name, w, h)
        {
            GameObject = enemy;
            this.Name = name;
            VelY = 0;
            VelX = 0;
        }
    }
}
