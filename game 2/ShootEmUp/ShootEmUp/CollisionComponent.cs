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
                    if (colObject is Bullet)
                    {
                        Program.game.DespawnPlayerBullet(colObject);
                        Program.game.bulletReloadable = true;
                        Program.game.Player.Score++;
                        Bullet currentBullet = (Bullet)colObject;
                        if (GameObject is Enemy && currentBullet.name == "playerBullet")
                        {
                            GameObject.Died = true;
                            Enemy enemy = (Enemy)GameObject;
                            enemy.Img = enemy.ExplodingImg;
                            Program.game._audio.runSound("Enemy dead");

                        }
                    }

                    if (colObject is Player)
                    {
                        Console.WriteLine("player hit");
                        Player player = (Player)colObject;
                        player.Lives--;
                        Program.game._audio.runSound("Player dead");
                        Program.game.Player.Score++;

                        if (GameObject is Enemy)
                        {
                            GameObject.Died = true;
                            Enemy enemy = (Enemy)GameObject;
                            enemy.Img = enemy.ExplodingImg;

                        }
                    }

                    if (colObject is Enemy)
                    {
                        Program.game.DespawnEnemy(colObject);

                    }
                        
                    //


                    
                    //Program.game.DespawnEnemy(GameObject);


                    

                }
            }

        }
    }
}
