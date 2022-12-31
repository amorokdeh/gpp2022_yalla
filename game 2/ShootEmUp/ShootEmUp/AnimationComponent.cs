using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AnimationComponent : Component
    {
        AnimationManager AnimationManager;

        protected float gap = 0;
        protected float gapsize = 0.15f;
        
        public AnimationComponent(AnimationManager am)
        {
            this.AnimationManager = am;
        }

        public void Animate(float deltaT)
        {
            gap += deltaT;
            //Console.WriteLine("Animation");

            if (gap > gapsize)
            {
                GameObject.ChangeImage();
                gap = 0;
            }

        }
    }
}
