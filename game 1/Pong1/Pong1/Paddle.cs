using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.IO;


namespace Pong
{
    class Paddle
    {
        public int PADDLE_WIDTH = 10;
        public int PADDLE_HEIGH = 100;

        public int score = 0;

        public int paddleVel = 8;

        //The X and Y offsets
        public float mPosX;
        public float mPosY;

        //The velocity
        public float mVelY = 0;

        SDL.SDL_Rect paddle;

        public Paddle(int mPosX, int mPosY, int width)
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
        }

        public void handleEvent(SDL.SDL_Event e, string upDownButtons)
        {
            if (upDownButtons.Equals("UP_DOWN"))
            {
                //If a key was pressed
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
                {

                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_UP: mVelY -= paddleVel; break;
                        case SDL.SDL_Keycode.SDLK_DOWN: mVelY += paddleVel; break;
                    }
                }
                //If a key was released
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
                {
                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_UP: mVelY += paddleVel; break;
                        case SDL.SDL_Keycode.SDLK_DOWN: mVelY -= paddleVel; break;
                    }
                }
            }

            if (upDownButtons.Equals("W_S"))
            {
                //If a key was pressed
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
                {

                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_w: mVelY -= paddleVel; break;
                        case SDL.SDL_Keycode.SDLK_s: mVelY += paddleVel; break;
                    }
                }
                //If a key was released
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
                {
                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_w: mVelY += paddleVel; break;
                        case SDL.SDL_Keycode.SDLK_s: mVelY -= paddleVel; break;
                    }
                }
            }
        }

        public void move()
        {

            //Move up or down
            mPosY += mVelY;

            //If the paddle went too far up or down
            if ((mPosY < 0) || (mPosY + PADDLE_HEIGH > Program.window.heigh))
            {
                //Move back
                mPosY -= mVelY;
            }
        }

        public void render(IntPtr renderer)
        {

            paddle.y = (int)mPosY;
            SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(renderer, ref paddle);
        }

        public void renderL2(IntPtr renderer)
        {


            

            for (int i = 0; i < 20; i++)
            {
                SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                //SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 200, 2);
                SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 220, (byte)(5 - i/4 ));
                paddle.x = (int)mPosX - i;
                paddle.y = (int)mPosY - i;
                paddle.w = PADDLE_WIDTH + i * 2;
                paddle.h = PADDLE_HEIGH + i * 2;
                SDL.SDL_RenderFillRect(renderer, ref paddle);

            }
            
            paddle.x = (int)mPosX;
            paddle.y = (int)mPosY;
            paddle.w = PADDLE_WIDTH;
            paddle.h = PADDLE_HEIGH;
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.SDL_RenderFillRect(renderer, ref paddle);
        }


    }
}