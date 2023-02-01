using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class EnemyPhysicsComponent : PhysicsComponent
    {
        public EnemyPhysicsComponent(PhysicsManager pm) : base(pm)
        {
        }
        public override void OnEvent(Event e)
        {

            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.ChangeDirection)
                changeDirection();
        }
        public override void Move(float deltaT)
        {
            //base.Move(deltaT);
        }

        public void changeDirection() {
            if (GameObject.direction == "left") {
                moveRight();

            } else if (GameObject.direction == "right")
            {
                moveLeft();
            }
        }

        public void moveLeft()
        {
            GameObject.direction = "left";
            GameObject.CurrentVelX -= GameObject.VelX;
            GameObject.VelX *= -1;

        }

        public void moveRight()
        {
            GameObject.direction = "right";
            GameObject.CurrentVelX += GameObject.VelX;
            GameObject.VelX *= -1;
        }

    }

}
