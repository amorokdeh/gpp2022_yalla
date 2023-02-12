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
                        collideDirection(colObject);
                    }

                    //collide with spike from top
                    if (this.Role == "player" && colObject.Role == "spike")
                    {
                        if (collideDirection(colObject) == "bottom") {
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

        public string collideDirection(CollisionComponent colObject)
        {
            GameObject block = colObject.GameObject;
            GameObject ob = GameObject;
            string direction = "";

            // Calculate the distance between the player and the object on the x and y axis
            float xDistance = (ob.PosX + (ob.Width / 2)) - (block.PosX + (block.Width / 2));
            float yDistance = (ob.PosY + (ob.Height / 2)) - (block.PosY + (block.Height / 2));
            float xShouldBe = (ob.Width / 2) + (block.Width / 2);
            float yShouldBe = (ob.Height / 2) + (block.Height / 2);

            // Fix position
            int dis = 0;
     
            float newX = ob.PosX;
            float newY = ob.PosY;
            // Check if player is colliding from the left or right
            if (Math.Abs(xDistance) > Math.Abs(yDistance))
            {
                if (xDistance > 0)
                {
                    // Colliding from the left
                    newX = block.PosX + block.Width + dis;
                    direction = "left";
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, GameObject));
                }
                else
                {
                    // Colliding from the right
                    newX = block.PosX - ob.Width - dis;
                    direction = "right";
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeDirection, GameObject));
                }
            }
            // Check if player is colliding from the top or bottom
            else
            {
                if (yDistance > 0)
                {
                    // Colliding from the top
                    newY = block.PosY + block.Height + dis;
                    ob.CurrentVelY = 0;
                    direction = "top";
                }
                else
                {
                    // Colliding from the bottom
                    newY = block.PosY - ob.Height - dis;
                    direction = "bottom";
                    MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.JumpAble, GameObject));
                }

            }


            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.NeutralCollision, GameObject, newX, newY));
            return direction;
        }
    }
}
