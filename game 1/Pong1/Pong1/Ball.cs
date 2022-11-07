using SDL2;
using System;



namespace Pong
{
    class Ball
    {
        Random rd = new Random();

        int mPosX = Program.SCREEN_WIDTH / 2 - 5;
        int mPosY = Program.SCREEN_HEIGHT / 2 - 5;

        //The velocity
        int mVelX = 2;
        int mVelY = 1;

        public const int BALL_SIZE = 10;


        SDL.SDL_Rect ball;

        public Ball()
        {


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
            SDL_mixer.Mix_PlayChannel(-1, Program._Scratch, 0);
            mPosX = Program.SCREEN_WIDTH / 2 - 5;
            //mVelX = (int)(Math.Pow(-1, rd.Next(1, 2)) );
            // 2 or -2 , ball flies left or right
            mVelX = ((int)rd.Next(0, 2) *2 -1) *2;
            //Console.WriteLine(mVelX + " mVelX ");
        }

        public void move()
        {

            mPosX = mPosX + mVelX;
            mPosY = mPosY + mVelY;

            if (mPosX < 0)
            {
                Program.rightPaddle.score++;
                afterScore();

            }
            else if (mPosX + BALL_SIZE > Program.SCREEN_WIDTH)
            {
                Program.leftPaddle.score++;
                afterScore();
            }
            else if ((mPosY < 0) || (mPosY + BALL_SIZE > Program.SCREEN_HEIGHT))
            {
                SDL_mixer.Mix_PlayChannel(-1, Program._High, 0);
                mVelY = -mVelY;
            }


            if(checkCollisionX())
            {
                SDL_mixer.Mix_PlayChannel(-1, Program._Medium, 0);
                mVelX = -mVelX;
            }
            else if (checkCollisionY())
            {
                SDL_mixer.Mix_PlayChannel(-1, Program._Medium, 0);
                mVelY = -mVelY;
            }

        }

        private bool checkCollisionX()
        {

   
            if ((mPosY + BALL_SIZE > Program.rightPaddle.mPosY) && (mPosY < Program.rightPaddle.mPosY + Paddle.PADDLE_HEIGHT))
            {
                if (mPosX + BALL_SIZE > Program.rightPaddle.mPosX && mPosX + BALL_SIZE <= Program.rightPaddle.mPosX + Math.Abs(mVelX))
                {
                    return true;
                }
            }

            if ((mPosY + BALL_SIZE > Program.leftPaddle.mPosY) && (mPosY < Program.leftPaddle.mPosY + Paddle.PADDLE_HEIGHT))
            {
                if (mPosX < Program.leftPaddle.mPosX + Paddle.PADDLE_WIDTH && mPosX >= Program.leftPaddle.mPosX + Paddle.PADDLE_WIDTH - Math.Abs(mVelX))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkCollisionY()
        {
            if ((mPosX + BALL_SIZE > Program.rightPaddle.mPosX) && (mPosX < Program.rightPaddle.mPosX + Paddle.PADDLE_WIDTH))
            {
                if (mPosY + BALL_SIZE > Program.rightPaddle.mPosY && mPosY < Program.rightPaddle.mPosY + Paddle.PADDLE_HEIGHT )
                {
                    Program.rightPaddle.mPosY = Program.rightPaddle.mPosY - Program.rightPaddle.mVelY;
                    return true;
                }
            }

            if ((mPosX + BALL_SIZE > Program.leftPaddle.mPosX) && (mPosX < Program.leftPaddle.mPosX + Paddle.PADDLE_WIDTH))
            {
                if (mPosY + BALL_SIZE > Program.leftPaddle.mPosY && mPosY < Program.leftPaddle.mPosY + Paddle.PADDLE_HEIGHT)
                {
                    Program.leftPaddle.mPosY = Program.leftPaddle.mPosY - Program.leftPaddle.mVelY;
                    return true;
                }
            }
            return false;
        }

        public void render()
        {
            ball.x = mPosX;
            ball.y = mPosY;
            SDL.SDL_SetRenderDrawColor(Program.renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(Program.renderer, ref ball);
        }

    }

}
