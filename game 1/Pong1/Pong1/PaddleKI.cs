using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.IO;

namespace Pong
{
    class PaddleKI : PaddleGeneral
    {
        
        public PaddleKI(int mPosX, int mPosY, int width)
        {
            this.mPosX = mPosX;
            this.mPosY = mPosY;
            this.PADDLE_WIDTH = width;
            

            paddle = new SDL.SDL_Rect
            {
                x = mPosX,
                y = mPosY,
                w = PADDLE_WIDTH,
                h = PADDLE_HEIGH
            };


            paddleVel = 4;
        }

        

        public override void move()
        {
            if(ball.mVelX >= 0)
            {
                if(Program.window.heigh/2 > mPosY+PADDLE_HEIGH/2)
                {
                    mVelY = paddleVel;
                }
                else if(Program.window.heigh / 2 < mPosY + PADDLE_HEIGH / 2)
                {
                    mVelY = -paddleVel;
                }
                else
                {
                    mVelY = 0;
                }
            }
            else
            {
                if (ball.mPosY + 5 > mPosY + PADDLE_HEIGH / 2)
                {
                    mVelY = paddleVel;
                }
                else if (ball.mPosY + 5 / 2 < mPosY + PADDLE_HEIGH / 2)
                {
                    mVelY = -paddleVel;
                }
                else
                {
                    mVelY = 0;
                }
            }

            //Move up or down
            mPosY += mVelY;

            //If the paddle went too far up or down
            if ((mPosY < 0) || (mPosY + PADDLE_HEIGH > Program.window.heigh))
            {
                //Move back
                mPosY -= mVelY;
            }
        }

    }
}
