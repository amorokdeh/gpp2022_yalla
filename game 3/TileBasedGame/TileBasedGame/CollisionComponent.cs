using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            if (colObject.GameObject.PosY + (colObject.GameObject.Height) > GameObject.PosY && colObject.GameObject.PosY < GameObject.PosY + (GameObject.Height))
            {
                if (colObject.GameObject.PosX + (colObject.GameObject.Width) > GameObject.PosX && colObject.GameObject.PosX < GameObject.PosX + (GameObject.Width))
                {
                    if (this.Role == "good" && colObject.Role == "bad" || this.Role == "bad" && colObject.Role == "good")
                    {
                        //Console.WriteLine("Collision detected");
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Collision, GameObject));
                        if (GameObject is Player)
                        {
                            Console.WriteLine(GameObject._name + " with " + colObject.GameObject._name);
                        }
                    }

                    if (colObject.Role == "block")
                    {
                        if (GameObject is Player)
                        {
                            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.CollisionWithBlock, GameObject));
                            Console.WriteLine(colObject.GameObject._name + " is here");
                            collideDirection(colObject);
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

        public void collideDirection(CollisionComponent colObject)
        {
            GameObject block = colObject.GameObject;
            GameObject ob = GameObject;

            // Calculate the distance between the player and the object on the x and y axis
            float xDistance = (ob.PosX + (ob.Width / 2)) - (block.PosX + (block.Width / 2));
            float yDistance = (ob.PosY + (ob.Height / 2)) - (block.PosY + (block.Height / 2));

            // Fix position
            int dis = 0;
            // Check if player is colliding from the left or right
            if (Math.Abs(xDistance) > Math.Abs(yDistance))
            {
                if (xDistance > 0)
                {
                    // Colliding from the right
                    ob.PosX = block.PosX + block.Width + dis;
                }
                else
                {
                    // Colliding from the left
                    ob.PosX = block.PosX - ob.Width - dis;
                }
            }
            // Check if player is colliding from the top or bottom
            else
            {
                if (yDistance > 0)
                {
                    // Colliding from the bottom
                    ob.PosY = block.PosY + block.Height + dis;
                }
                else
                {
                    // Colliding from the top
                    ob.PosY = block.PosY - ob.Height - dis;
                    ob.CurrentVelY = 0;
                    Console.WriteLine(ob.CurrentVelY);
                }

            }

        }
    }
}
