using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class AnimationComponent : Component
    {
        AnimationManager AnimationManager;

        private float _gap = 0;
        private float _gapSize = Globals.AnimationGap;

        public int ImgStep = Globals.Reset;

        protected int Pause = Globals.AnimationPause;


        public AnimationComponent(AnimationManager am)
        {
            this.AnimationManager = am;
        }

        public void Animate(float deltaT)
        {
            if (GameObject.CurrentVelX > 0)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.FlyRight, GameObject));
            }
            else if (GameObject.CurrentVelX < 0)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.FlyLeft, GameObject));
            }
            else if (GameObject.CurrentVelY < 0)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.FlyUp, GameObject));
            }
            else
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.FlyStraight, GameObject));
            }
            
            _gap += deltaT;

            if (_gap > _gapSize)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ChangeImage, GameObject));
                _gap = 0;
            }

        }
    }
}
