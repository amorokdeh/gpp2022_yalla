using SDL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pong
{
    class Level2 : Level
    {


        public Portal bluePortal;
        public Portal redPortal;

        public Level2()
        {
            ball = new Ball(10);
            rightPaddle = new Paddle(Program.window.width - 23, Program.window.heigh / 2 - 50, 3);
            leftPaddle = new Paddle(20, Program.window.heigh / 2 - 50, 3);
            bluePortal = new Portal(11, 57, 250, 0);
            redPortal = new Portal(233, 7, 48, Program.window.heigh - 1);
            setup();
        }


        public override void setup()
        {
            // setup der Oberklasse aufrufen
            base.setup();

            //Text
            txt.setUp();
            txt.loadText(3);
        }


        public override void update()
        {
            base.update();
            ball.moveL2();
        }


        public override void render()
        {
            base.render();

           
            // center line
            SDL.SDL_SetRenderDrawColor(renderer, 235, 235, 240, 255);
            SDL.SDL_RenderDrawLine(renderer, Program.window.width/2, 0, Program.window.width / 2, Program.window.heigh);
            //Text
            //left score
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, leftPaddle.score.ToString(), txt.LightGray);
            txt.addText(renderer, surfaceMessage, 200, 10, 20, 100);
            //right score
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, rightPaddle.score.ToString(), txt.LightGray);
            txt.addText(renderer, surfaceMessage, 580, 10, 20, 100);

            // right paddle
            rightPaddle.renderL2(renderer);
            // left paddle
            leftPaddle.renderL2(renderer);
            // blue Portal
            bluePortal.render(renderer);
            redPortal.render(renderer);
            //ball
            ball.renderL2(renderer);
            // Update screen
            SDL.SDL_RenderPresent(renderer);

        }

        

        

        
    }
}

