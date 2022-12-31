using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AnimationManager
    {
        List<AnimationComponent> _animationComponents = new List<AnimationComponent>();

        internal Component CreateComponent()
        {
            AnimationComponent ac = new AnimationComponent(this);
            _animationComponents.Add(ac);
            return ac;
        }


        public void Animate(float deltaT)
        {
            foreach (var component in _animationComponents)
            {
                if (component.GameObject.Active)
                {
                    component.Animate(deltaT);
                }
            }
        }
    }
}
