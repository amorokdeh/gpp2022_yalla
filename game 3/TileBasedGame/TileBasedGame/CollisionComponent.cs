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

                    if (colObject.Role == "block" && this.Role == "good")
                    {
                        collideDirection(colObject);
                        
                    }
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
                    // Colliding from the right
                    //ob.PosX = block.PosX + block.Width + dis;
                    newX = block.PosX + block.Width + dis;
                }
                else
                {
                    // Colliding from the left
                    //ob.PosX = block.PosX - ob.Width - dis;
                    newX = block.PosX - ob.Width - dis;
                }
            }
            // Check if player is colliding from the top or bottom
            else
            {
                if (yDistance > 0)
                {
                    // Colliding from the bottom
                    //ob.PosY = block.PosY + block.Height + dis;
                    newY = block.PosY + block.Height + dis;
                }
                else
                {
                    // Colliding from the top
                    //ob.PosY = block.PosY - ob.Height - dis;
                    newY = block.PosY - ob.Height - dis;
                    ob.CurrentVelY = 0;
                }

            }


            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.NeutralCollision, GameObject, newX, newY));

        }
    }
}
