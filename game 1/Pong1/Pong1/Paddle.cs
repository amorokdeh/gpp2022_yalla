using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.IO;


namespace Pong
{
    class Paddle : PaddleGeneral
    {


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

        



        public override void handleEvent(SDL.SDL_Event e, string upDownButtons)
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
    }

}