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
                        Program.Game.DespawnPlayerBullet(colObject);
                        

                        if (GameObject is Enemy)
                        {
                            Program.Game.BulletReloadable = true;
                            Program.Game.Player.Score++;
                            GameObject.Died = true;
                            Enemy enemy = (Enemy)GameObject;
                            enemy.Img = enemy.ExplodingImg;
                            Program.Game._audio.RunSound("Enemy dead");
                        }

                        if (GameObject is EnemyBullet)
                        {
                            Program.Game.DespawnEnemyBullet(GameObject);
                        }
                    }

                    if (colObject is Player)
                    {
                        Console.WriteLine("player hit");
                        Player player = (Player)colObject;
                        player.Lives--;
                        Program.Game._audio.RunSound("Player dead");
                        Program.Game.Player.Score++;

                        if (GameObject is Enemy)
                        {
                            GameObject.Died = true;
                            Enemy enemy = (Enemy)GameObject;
                            enemy.Img = enemy.ExplodingImg;
                            Program.Game._audio.RunSound("Enemy dead");
                        }

                        if (GameObject is EnemyBullet)
                        {
                            Program.Game.DespawnEnemyBullet(GameObject);
                        }
                    }

                    if (colObject is Enemy)
                    {
                        if (GameObject is Enemy)
                            Program.Game.DespawnEnemy(colObject);

                    }
                        
                    

                }
            }

        }
    }
}
