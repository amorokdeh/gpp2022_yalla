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

        private float _gap = 0;
        private float _gapSize = 0.15f;
        
        public AnimationComponent(AnimationManager am)
        {
            this.AnimationManager = am;
        }

        public void Animate(float deltaT)
        {
            _gap += deltaT;
            //Console.WriteLine("Animation");

            if (_gap > _gapSize)
            {
                GameObject.ChangeImage();
                _gap = 0;
            }

        }
    }
}
