using SDL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Level1 : Level
    {

        public int lines = 13;
        public static int linesStatic = 13;
        public SDL.SDL_Rect[] centerLine = new SDL.SDL_Rect[linesStatic];
        public int line = 0;

        
        public Level1() 
        {
            ball = new Ball(6);
            rightPaddle = new Paddle(Program.window.width - 30, Program.window.heigh / 2 - 50, 10);
            leftPaddle = new Paddle(20, Program.window.heigh / 2 - 50, 10);

            setup();
            map();
        }

        private void map() {
            for (int i = 0; i < lines; i++)
            {
                centerLine[line] =
                    new SDL.SDL_Rect
                    {
                        x = Program.window.width / 2 - 2,
                        y = line * 48,
                        w = 4,
                        h = 24
                    };
                line++;
            }
        }
        public override void setup()
        {
            // setup der Oberklasse aufrufen
            base.setup();
            
            //Image
            img.setUp();
            img.loadImage(renderer, "image/pumpkin.bmp");

            //Text
            txt.setUp();
            txt.loadText(2);
        }

        public override void update() {
            base.update();
            ball.moveL1();         
        }

        public override void render()
        {
            base.render();

            // ----draw----
            SDL.SDL_RenderCopy(renderer, img.pumpkinTexture, ref img.sRect, ref img.tRect);
            // center line
            SDL.SDL_SetRenderDrawColor(renderer, 150, 150, 150, 255);
            SDL.SDL_RenderFillRects(renderer, centerLine, lines);
            //Text
            //left score
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, leftPaddle.score.ToString(), txt.Gray);
            txt.addText(renderer, surfaceMessage, 200, 10, 100, 100);
            //right score
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, rightPaddle.score.ToString(), txt.Gray);
            txt.addText(renderer, surfaceMessage, 500, 10, 100, 100);

            // right paddle
            rightPaddle.render(renderer);
            // left paddle
            leftPaddle.render(renderer);
            //ball
            ball.render(renderer);
            // Update screen
            SDL.SDL_RenderPresent(renderer);

        }

    }
}
