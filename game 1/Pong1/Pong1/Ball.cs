using SDL2;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Ball
    {
        int mPosX = Program.SCREEN_WIDTH / 2 - 5;
        int mPosY = Program.SCREEN_HEIGHT / 2 - 5;

        //The velocity
        int mVelX = 1;


        SDL.SDL_Rect ball;

        public Ball()
        {

            ball = new SDL.SDL_Rect
            {
                x = mPosX,
                y = mPosY,
                w = 10,
                h = 10
            };
        }

        public void move()
        {
            mPosX = mPosX + mVelX;
            if ((mPosX < 0) || (mPosX + 10 > Program.SCREEN_WIDTH))
            {
                mVelX = -mVelX;
            }
        }

        public void render()
        {

            ball.x = mPosX;
            SDL.SDL_SetRenderDrawColor(Program.renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(Program.renderer, ref ball);
        }

    }

}
