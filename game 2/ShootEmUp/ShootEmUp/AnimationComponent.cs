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

        float gap = 0;
        float gapsize = 0.15f;
        float pause = 10;
        public AnimationComponent(AnimationManager am)
        {
            this.AnimationManager = am;
        }

        public void Animate(float deltaT)
        {
            gap += deltaT;
            //Console.WriteLine("Animation");


            if (gap < gapsize * pause)
                GameObject.ImgChange = 16 * 0;
            else if(gap < gapsize * (pause +1))
                GameObject.ImgChange = 16 * 1;
            else if(gap < gapsize * (pause + 2))
                GameObject.ImgChange = 16 * 2;
            else if (gap < gapsize * (pause + 3))
                GameObject.ImgChange = 16 * 3;
            else if (gap < gapsize * (pause + 4))
                GameObject.ImgChange = 16 * 2;
            else if (gap < gapsize * (pause + 5))
                GameObject.ImgChange = 16 * 1;
            else
            {
                gap = 0;
                GameObject.ImgChange = 16 * 0;
            }




        }

    }
}
