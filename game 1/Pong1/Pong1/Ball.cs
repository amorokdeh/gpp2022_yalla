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
        Paddle leftPaddle = new Paddle(0,0);
        Paddle rightPaddle = new Paddle(0,0);
        Random rd = new Random();
        int mPosX = Program.window.width / 2 - 5;
        int mPosY = Program.window.heigh / 2 - 5;

        //The velocity
        int mVelX = 2;
        int mVelY = 1;

        public const int BALL_SIZE = 10;

        SDL.SDL_Rect ball;

        public Ball()
        {
            mVelX = ((int)rd.Next(0, 2) * 2 - 1) * 2;

            ball = new SDL.SDL_Rect
            {
                x = mPosX,
                y = mPosY,
                w = BALL_SIZE,
                h = BALL_SIZE
            };
        }

        private void afterScore()
        {
            SDL_mixer.Mix_PlayChannel(-1, Program.game.level1.sound._Scratch, 0);
            mPosX = Program.window.width / 2 - 5;
            //mVelX = (int)(Math.Pow(-1, rd.Next(1, 2)) );
            // 2 or -2 , ball flies left or right
            mVelX = ((int)rd.Next(0, 2) * 2 - 1) * 2;
            //Console.WriteLine(mVelX + " mVelX ");
        }

        public void move()
        {

            mPosX = mPosX + mVelX;
            mPosY = mPosY + mVelY;

            if (mPosX < 0)
            {
                Program.game.level1.rightPaddle.score++;
                afterScore();

            }
            else if (mPosX + BALL_SIZE > Program.window.width)
            {
                Program.game.level1.leftPaddle.score++;
                afterScore();
            }
            else if ((mPosY < 0) || (mPosY + BALL_SIZE > Program.window.heigh))
            {
                SDL_mixer.Mix_PlayChannel(-1, Program.game.level1.sound._High, 0);
                mVelY = -mVelY;
            }


            if (checkCollisionX())
            {
                SDL_mixer.Mix_PlayChannel(-1, Program.game.level1.sound._Medium, 0);
                mVelX = -mVelX;
                speedUp();
            }
            else if (checkCollisionY())
            {
                SDL_mixer.Mix_PlayChannel(-1, Program.game.level1.sound._Medium, 0);
                mVelY = -mVelY;
            }

        }

        public void speedUp() {
            if (mVelX > 0) {
                mVelX++;
            }
            if (mVelX < 0)
            {
                mVelX--;
            }
        }

        private bool checkCollisionX()
        {


            if ((mPosY + BALL_SIZE > rightPaddle.mPosY) && (mPosY < rightPaddle.mPosY + rightPaddle.PADDLE_HEIGH))
            {
                if (mPosX + BALL_SIZE > rightPaddle.mPosX && mPosX + BALL_SIZE <= rightPaddle.mPosX + Math.Abs(mVelX))
                {
                    return true;
                }
            }

            if ((mPosY + BALL_SIZE > leftPaddle.mPosY) && (mPosY < leftPaddle.mPosY + leftPaddle.PADDLE_HEIGH))
            {
                if (mPosX < leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH && mPosX >= leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH - Math.Abs(mVelX))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkCollisionY()
        {
            if ((mPosX + BALL_SIZE > rightPaddle.mPosX) && (mPosX < rightPaddle.mPosX + rightPaddle.PADDLE_WIDTH))
            {
                if (mPosY + BALL_SIZE > rightPaddle.mPosY && mPosY < rightPaddle.mPosY + rightPaddle.PADDLE_HEIGH)
                {
                    Program.game.level1.rightPaddle.mPosY = rightPaddle.mPosY - rightPaddle.mVelY;
                    return true;
                }
            }

            if ((mPosX + BALL_SIZE > leftPaddle.mPosX) && (mPosX < leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH))
            {
                if (mPosY + BALL_SIZE > leftPaddle.mPosY && mPosY < leftPaddle.mPosY + leftPaddle.PADDLE_HEIGH)
                {
                    Program.game.level1.leftPaddle.mPosY = leftPaddle.mPosY - leftPaddle.mVelY;
                    return true;
                }
            }
            return false;
        }

        public void render(IntPtr renderer)
        {
            leftPaddle = Program.game.level1.leftPaddle;
            rightPaddle = Program.game.level1.rightPaddle;
            ball.x = mPosX;
            ball.y = mPosY;
            SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(renderer, ref ball);
        }

    }
}
