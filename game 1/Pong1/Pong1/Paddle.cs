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

        const int PADDLE_VEL = 4;

        //The X and Y offsets
        public int mPosX;
        public int mPosY;

        //The velocity
        public int mVelY = 0;

        SDL.SDL_Rect paddle;

        public Paddle(int mPosX, int mPosY)
        {
            this.mPosX = mPosX;
            this.mPosY = mPosY;

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
                        case SDL.SDL_Keycode.SDLK_UP: mVelY -= PADDLE_VEL; break;
                        case SDL.SDL_Keycode.SDLK_DOWN: mVelY += PADDLE_VEL; break;
                    }
                }
                //If a key was released
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
                {
                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_UP: mVelY += PADDLE_VEL; break;
                        case SDL.SDL_Keycode.SDLK_DOWN: mVelY -= PADDLE_VEL; break;
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
                        case SDL.SDL_Keycode.SDLK_w: mVelY -= PADDLE_VEL; break;
                        case SDL.SDL_Keycode.SDLK_s: mVelY += PADDLE_VEL; break;
                    }
                }
                //If a key was released
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
                {
                    //Adjust the velocity
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_w: mVelY += PADDLE_VEL; break;
                        case SDL.SDL_Keycode.SDLK_s: mVelY -= PADDLE_VEL; break;
                    }
                }
            }
        }

        public void move()
        {

            //Move up or down
            mPosY += mVelY;

            //If the dot went too far up or down
            if ((mPosY < 0) || (mPosY + PADDLE_HEIGH > Program.window.heigh))
            {
                //Move back
                mPosY -= mVelY;
            }
        }

        public void render(IntPtr renderer)
        {

            paddle.y = mPosY;
            SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(renderer, ref paddle);
        }

    }
}