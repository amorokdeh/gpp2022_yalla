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

        public void Collide(GameObject colObject)
        {
            //TODO genauere Kollision
            if(colObject.PosY+(colObject.Height-6) > GameObject.PosY  && colObject.PosY < GameObject.PosY+(GameObject.Height-6))
            {
                if(colObject.PosX+(colObject.Width-6) > GameObject.PosX && colObject.PosX < GameObject.PosX+(GameObject.Width-6))
                {
                    /*
                    Console.WriteLine("colObject, Enemy " + colObject.PosY.ToString(".0##")+ GameObject.PosY.ToString(".0##"));
                    Console.WriteLine("Width " + colObject.Width + GameObject.Width);
                    Console.WriteLine("Height " + colObject.Height + GameObject.Height);*/
                    Console.WriteLine("collision");
                    if(colObject is Bullet)
                        Program.game.DespawnPlayerBullet(colObject);
                    if (colObject is Player)
                    {
                        Console.WriteLine("player hit");
                        Player player = (Player)colObject;
                        player.Lives--;
                    }


                    if (GameObject is Ufo)
                        Program.game.DespawnEnemyUfo(GameObject);
                    if(GameObject is Ship)
                        Program.game.DespawnEnemyShip(GameObject);
                    
                }
            }

        }
    }
}
