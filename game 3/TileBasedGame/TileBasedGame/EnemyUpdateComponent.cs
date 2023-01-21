using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class EnemyUpdateComponent : UpdateComponent
    {
        public EnemyUpdateComponent(UpdateManager um) : base(um)
        {
            
        }

        public override void Update()
        {
            Console.WriteLine("ENEMY");
            Program.Game.BulletReloadable = true;
            Program.Game.Player.Score++;
            GameObject.Died = true;

            //TODO überarbeiten
            Enemy enemy = (Enemy)GameObject;
            enemy.Img = enemy.ExplodingImg;
            Program.Game._audio.RunSound("Enemy dead");
        }
    }
}
