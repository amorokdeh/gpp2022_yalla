using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class CollisionComponent : Component
    {
        CollisionManager CollisionManager;
        public CollisionComponent(CollisionManager cm)
        {
            this.CollisionManager = cm;
        }

        public void Collide(GameObject bullet)
        {
            //TODO genauere Kollision
            if(bullet.PosY+(bullet.Height-6) > GameObject.PosY  && bullet.PosY < GameObject.PosY+(GameObject.Height-6))
            {
                if(bullet.PosX+(bullet.Width-6) > GameObject.PosX && bullet.PosX < GameObject.PosX+(GameObject.Width-6))
                {
                    /*
                    Console.WriteLine("Bullet, Enemy " + bullet.PosY.ToString(".0##")+ GameObject.PosY.ToString(".0##"));
                    Console.WriteLine("Width " + bullet.Width + GameObject.Width);
                    Console.WriteLine("Height " + bullet.Height + GameObject.Height);
                    Console.WriteLine("collision");*/
                    Program.game.DespawnPlayerBullet(bullet);
                    if(GameObject is Ufo)
                        Program.game.DespawnEnemyUfo(GameObject);
                    if(GameObject is Ship)
                        Program.game.DespawnEnemyShip(GameObject);
                }
            }

        }
    }
}
