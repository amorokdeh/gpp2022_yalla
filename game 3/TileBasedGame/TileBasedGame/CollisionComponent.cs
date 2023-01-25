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
            GameObject go1 = colObject.GameObject;
            GameObject go2 = GameObject;

            bool up =    go1.PosY + go1.Height > go2.PosY && go1.PosY < go2.PosY && go1.PosX < go2.PosX + go2.Width && go1.PosX + go1.Width > go1.PosX;
            bool down =  go1.PosY < go2.PosY + go2.Height && go1.PosY + go1.Height > go2.PosY + go2.Height && go1.PosX < go2.PosX + go2.Width && go1.PosX + go1.Width > go1.PosX;
            bool right = go1.PosX < go2.PosX + go2.Width && go1.PosX + go1.Width > go2.PosX && go1.PosY > go2.PosY + go2.Height && go1.PosY + go1.Height < go1.PosY;
            bool left =  go1.PosX + go1.Width > go2.PosX && go1.PosX < go2.PosX && go1.PosY > go2.PosY + go2.Height && go1.PosY + go1.Height < go1.PosY;
            Console.WriteLine("aaa: is " + right);
            if (up && !down && !left && !right)
            {
                Console.WriteLine("Up");
                GameObject.PosY = colObject.GameObject.PosY + colObject.GameObject.Height;
            }
            if (down && !up && !left && !right)
            {
                Console.WriteLine("Down");
                GameObject.PosY = colObject.GameObject.PosY - GameObject.Height;
            }
            if (left && !down && !up && !right)
            {
                Console.WriteLine("Left");
                GameObject.PosX = colObject.GameObject.PosX - GameObject.Width;
            }
            if (right && !up && !left && !down)
            {
                Console.WriteLine("Right");
                GameObject.PosX = colObject.GameObject.PosX + colObject.GameObject.Width;
            }

        }
    }
}
