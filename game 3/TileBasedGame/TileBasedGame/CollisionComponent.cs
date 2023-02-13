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


        public void Collide(CollisionComponent colObject, float deltaT)
        {

            
            if (colObject.GameObject.PosY + (colObject.GameObject.Height) > GameObject.PosY && colObject.GameObject.PosY < GameObject.PosY + (GameObject.Height))
            {
                if (colObject.GameObject.PosX + (colObject.GameObject.Width) > GameObject.PosX && colObject.GameObject.PosX < GameObject.PosX + (GameObject.Width))
                {

                    if (this.Role == "good" && colObject.Role == "bad" || this.Role == "bad" && colObject.Role == "good")
                    {
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Collision, GameObject));
                    }

                    if (colObject.Role == "bad" && this.Role == "bad")
                    {
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, this.GameObject));
                    }

                    if (colObject.Role == "block" || colObject.Role == "spike")
                    {
                        collideDirection(colObject, deltaT);
                    }

                    //collide with spike from top
                    if (this.Role == "player" && colObject.Role == "spike")
                    {
                        if (collideDirection(colObject, deltaT) == "bottom") {
                            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Collision, GameObject));
                            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Hurt, GameObject));
                        }

                    }

                    if (this.Role == "player" && colObject.Role == "bad")
                    {
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Hurt, GameObject));
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Collision, GameObject));
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, colObject.GameObject));
                    }
                    //player take coins
                    if (this.Role == "player" && colObject.Role == "coin")
                    {
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.takeCoin, colObject.GameObject));
                    }
                    //player take power
                    if (this.Role == "player" && colObject.Role == "power")
                    {
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.takePower, colObject.GameObject));
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.powerUp));
                    }
                }
            }

        }

        public string collideDirection(CollisionComponent colObject, float deltaT)
        {
            GameObject block = colObject.GameObject;
            GameObject ob = GameObject;
            string direction = "";

            // Fix position
            int dis = 1;
     
            float newX = ob.PosX;
            float newY = ob.PosY;
                

            if (ob.PosY + ob.Height >= block.PosY && ob.PosY - ob.CurrentVelY * deltaT + ob.Height <= block.PosY)
            {
                if (GameObject is Player)
                    Console.WriteLine("collision bottom");
                // Colliding from the bottom
                newY = block.PosY - ob.Height - dis;
                direction = "bottom";
                MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.JumpAble, GameObject));
            }

            else if (ob.PosY <= block.PosY + block.Height && ob.PosY - ob.CurrentVelY * deltaT >= block.PosY + block.Height)
            {
                if (GameObject is Player)
                    Console.WriteLine("collision top");
                // Colliding from the top
                newY = block.PosY + block.Height + dis;
                ob.CurrentVelY = 0;
                direction = "top";
            }
            else if (ob.PosX <= block.PosX + block.Width && ob.PosX - ob.CurrentVelX * deltaT >= block.PosX + block.Width)
            {
                if (GameObject is Player)
                    Console.WriteLine("collision left");
                // Colliding from the left
                newX = block.PosX + block.Width + dis;
                direction = "left";
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, GameObject));
            }
            else if (ob.PosX + ob.Width >= block.PosX && ob.PosX - ob.CurrentVelX * deltaT + ob.Width <= block.PosX)
            {
                if (GameObject is Player)
                    Console.WriteLine("collision right");
                // Colliding from the right
                newX = block.PosX - ob.Width - dis;
                direction = "right";
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, GameObject));
            }


            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.NeutralCollision, GameObject, newX, newY));
            return direction;
        }
    }
}
