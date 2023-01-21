using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class CollisionComponent : Component
    {
        CollisionManager CollisionManager;

        public string Role;
        public CollisionComponent(CollisionManager cm, string role)
        {
            Role = role;
            this.CollisionManager = cm;
        }
 

        public void Collide(CollisionComponent colObject)
        {
            //TODO genauere Kollision
            if(colObject.GameObject.PosY+(colObject.GameObject.Height - 6) > GameObject.PosY  && colObject.GameObject.PosY < GameObject.PosY+(GameObject.Height-6))
            {
                if(colObject.GameObject.PosX+(colObject.GameObject.Width-6) > GameObject.PosX && colObject.GameObject.PosX < GameObject.PosX+(GameObject.Width-6))
                {
                    if(this.Role == "good" && colObject.Role == "bad" || this.Role == "bad" && colObject.Role == "good")
                    {
                        //Console.WriteLine("Collision detected");
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Collision, GameObject));
                        if(GameObject is Player)
                        {
                            Console.WriteLine("Player hit");
                        }
                    }
                    

                    /*
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
                       */ 
                    

                }
            }

        }
    }
}
