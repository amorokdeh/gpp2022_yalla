using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class BulletUpdateComponent: UpdateComponent
    {
        public BulletUpdateComponent(UpdateManager um):base(um)
        {
        }

        public override void Update()
        {
            Console.WriteLine("BULLET");
            Program.Game.DespawnBullet(GameObject);
        }
    }
}
