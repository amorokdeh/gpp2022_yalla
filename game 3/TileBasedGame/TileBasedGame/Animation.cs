using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Animation
    {
        public int[] Frames;
        public float Duration;

        private float _time;
        private int _step;
        private int _steps;

        private int _frame;

        public Animation() {}
        public Animation(int[] frames, float duration)
        {
            Frames = frames;
            Duration = duration;
        }

        public void Setup()
        {
            _steps = Frames.Length;
            _step = 0;
            _time = 0;
        }

        public int Animate(float deltaT)
        {
            _time += deltaT;
            if(_time > Duration/_steps)
            {
                if(_step < _steps)
                {
                    _frame = Frames[_step];
                    _step++;
                }
                else
                {
                    _step = 0;
                }
  
                _time = 0;
            }
            return _frame;
        }
    }
}
