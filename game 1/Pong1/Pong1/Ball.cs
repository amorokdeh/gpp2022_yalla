﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Ball
    {
        Paddle leftPaddle;
        Paddle rightPaddle;
        Portal bluePortal;
        Portal redPortal;
        Random rd = new Random();
        double mPosX = Program.window.width / 2 - 5;
        double mPosY = Program.window.heigh / 2 - 5;

        //The velocity
        double vel = 6;
        double mVelX;
        double mVelY;

        public const int BALL_SIZE = 10;

        SDL.SDL_Rect ball;


        public Ball(double v, ref Paddle left, ref Paddle right)
        {
            leftPaddle = left;
            rightPaddle = right;

            vel = v;
            mVelX = ((double)rd.Next(0, 2) * 2 - 1) * (vel / 3 * 2);
            mVelY = ((double)rd.Next(0, 2) * 2 - 1) * (vel / 3 * 1);

            ball = new SDL.SDL_Rect
            {
                x = (int)mPosX,
                y = (int)mPosY,
                w = BALL_SIZE,
                h = BALL_SIZE
            };
        }

        public Ball(double v, ref Paddle left, ref Paddle right, ref Portal blue, ref Portal red ) : this(v, ref left, ref right)//den anderen Konstruktor aufrufen
        {
            bluePortal = blue;
            redPortal = red;
        }
        

        private void afterScore()
        {
            SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Scratch, 0);
            // Ball auf die Mittellinie setzen
            mPosX = Program.window.width / 2 - 5;
            mPosY = (float)rd.Next(0, (Program.window.heigh - BALL_SIZE));

            // 2 or -2 , ball flies left or right
            mVelX = ((float)rd.Next(0, 2) * 2 - 1) * (vel / 3 * 2);
            mVelY = ((float)rd.Next(0, 2) * 2 - 1) * (vel / 3 * 1);
        }

        public void moveL1()
        {
            mPosX = mPosX + mVelX;
            mPosY = mPosY + mVelY;

            if (mPosX < 0)
            {
                rightPaddle.score++;
                afterScore();

            }
            else if (mPosX + BALL_SIZE > Program.window.width)
            {
                leftPaddle.score++;
                afterScore();
            }
            else if ((mPosY < 0) || (mPosY + BALL_SIZE > Program.window.heigh))
            {
                SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._High, 0);
                mVelY = -mVelY;
            }


            if (checkCollisionX())
            {
                SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Medium, 0);
                mVelX = -mVelX;
                speedUp();
            }
            else if (checkCollisionY())
            {
                SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Medium, 0);
                mVelY = -mVelY;
            }
        }

        public bool checkPortal()
        {
            if(mPosX > Program.game.level2.bluePortal.leftX && mPosX < Program.game.level2.bluePortal.leftX + 200)
            {
                return true;
            }
            return false;
        }

        public void moveL2()
        {
            mPosX = mPosX + mVelX;
            mPosY = mPosY + mVelY;

            if (mPosX < 0)
            {
                rightPaddle.score++;
                afterScore();

            }
            else if (mPosX + BALL_SIZE > Program.window.width)
            {
                leftPaddle.score++;
                afterScore();
            }
            else if (mPosY < 0)
            {
                
                if (checkPortal())
                {
                    SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Portal, 0);
                    mPosY = Program.window.heigh - 10;
                }
                else
                {
                    SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._High, 0);
                    mVelY = -mVelY;
                }
            }
            else if (mPosY + BALL_SIZE > Program.window.heigh)
            {
                
                if (checkPortal())
                {
                    SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Portal, 0);
                    mPosY = 0;
                }
                else
                {
                    SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._High, 0);
                    mVelY = -mVelY;
                }
            }
            
            if (checkCollisionXL2())
            {
                SDL_mixer.Mix_PlayChannel(-1, MainMenu.sound._Medium, 0);
                //mVelX = -mVelX;
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


        // Je nachdem, wo der Ball am Paddle anstößt, fliegt er in eine bestimmte Richtung
        private bool checkCollisionXL2()
        {
            if ((mPosY + BALL_SIZE > rightPaddle.mPosY) && (mPosY < rightPaddle.mPosY + rightPaddle.PADDLE_HEIGH))
            {
                if (mPosX + BALL_SIZE > rightPaddle.mPosX && mPosX + BALL_SIZE <= rightPaddle.mPosX + Math.Abs(mVelX))

                {
                    double distance = (rightPaddle.mPosY + 50) - (mPosY + 5);
                    double change = 9.0 / 55.0 * distance;
                    mVelY = -change;
                    mVelX = -(vel - Math.Abs(change)); 
                    return true;
                }
            }

            if ((mPosY + BALL_SIZE > leftPaddle.mPosY) && (mPosY < leftPaddle.mPosY + leftPaddle.PADDLE_HEIGH))
            {
                if (mPosX < leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH && mPosX >= leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH - Math.Abs(mVelX))
                {
                    double distance = (leftPaddle.mPosY + 50) - (mPosY + 5);
                    double change = 9.0 / 55.0 * distance;
                    mVelY = -change;
                    mVelX = vel - Math.Abs(change);

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
                    rightPaddle.mPosY = rightPaddle.mPosY - rightPaddle.mVelY;
                    return true;
                }
            }

            if ((mPosX + BALL_SIZE > leftPaddle.mPosX) && (mPosX < leftPaddle.mPosX + leftPaddle.PADDLE_WIDTH))
            {
                if (mPosY + BALL_SIZE > leftPaddle.mPosY && mPosY < leftPaddle.mPosY + leftPaddle.PADDLE_HEIGH)
                {
                    leftPaddle.mPosY = leftPaddle.mPosY - leftPaddle.mVelY;
                    return true;
                }
            }
            return false;
        }

 

        public void render(IntPtr renderer)
        {
            ball.x = (int)mPosX;
            ball.y = (int)mPosY;
            SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(renderer, ref ball);
        }

        public void renderL2(IntPtr renderer)
        {

            // Licht/Schein des Balls zeichenen
            for (int i = 0; i < 20; i++)
            {
                SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 220, (byte)(5 - i / 4));
                ball.x = (int)mPosX - i;
                ball.y = (int)mPosY - i;
                ball.w = 10 + i * 2;
                ball.h = 10 + i * 2;
                SDL.SDL_RenderFillRect(renderer, ref ball);

            }

            // Ball zeichnen
            ball.x = (int)mPosX;
            ball.y = (int)mPosY;
            ball.w = 10;
            ball.h = 10;
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.SDL_RenderFillRect(renderer, ref ball);
        }


    }
}
