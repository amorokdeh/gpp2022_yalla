﻿using SDL2;
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
    class Level2
    {

        public int lines = 13;
        public static int linesStatic = 13;
        public SDL.SDL_Rect[] centerLine = new SDL.SDL_Rect[linesStatic];
        public int line = 0;

        public IntPtr renderer;
        public bool running = true;
        public bool quit = false;

        public Ball ball = new Ball(10);
        public Paddle rightPaddle = new Paddle(Program.window.width - 30, Program.window.heigh / 2 - 50);
        public Paddle leftPaddle = new Paddle(20, Program.window.heigh / 2 - 50);


        public Text txt = new Text();
        public int win = 10; //score to win
        public Level2()
        {
            setup();
            map();
        }

        private void map()
        {
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
        private void setup()
        {
            renderer = SDL.SDL_CreateRenderer(
                Program.window.show,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }
            /*
            // SOUND AND MUSIC
            sound.setup();
            //Load music
            sound._Music = sound.loadMusic("sound/beat.wav");
            //Load sound effects
            sound._Scratch = sound.loadSound("sound/scratch.wav");
            sound._High = sound.loadSound("sound/high.wav");
            sound._Medium = sound.loadSound("sound/medium.wav");
            sound._Low = sound.loadSound("sound/low.wav");*/



            //Text
            txt.setUp();
            txt.loadText(2);
        }

        private void update()
        {
            rightPaddle.move();
            leftPaddle.move();
            ball.moveL2();
        }

        private void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);

           
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
            ball.renderL2(renderer);
            // Update screen
            SDL.SDL_RenderPresent(renderer);

        }

        public void controll()
        {
            //Key
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //User requests quit
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    running = false;
                    quit = true;
                }

                // Handle input
                rightPaddle.handleEvent(e, "UP_DOWN");
                leftPaddle.handleEvent(e, "W_S");
            }
        }

        public void run()
        {

            while (running)
            {
                controll();
                update();
                //Thread.Sleep(100);
                render();
                //Thread.Sleep(100);
                over();
            }

            if (quit) { closeAndGoTo(0); } //close the game
        }

        private void over()
        {
            if (rightPaddle.score == win)
            {
                Program.game.mainMenu.winner = 2;
                running = false;
                closeAndGoTo(4); // go to game over
            }
            else if (leftPaddle.score == win)
            {
                Program.game.mainMenu.winner = 2;
                running = false;
                closeAndGoTo(4); // go to game over
            }

        }

        public void closeAndGoTo(int displayNum)
        {
            /*
            //Free the sound effects
            SDL_mixer.Mix_FreeChunk(sound._Scratch);
            SDL_mixer.Mix_FreeChunk(sound._High);
            SDL_mixer.Mix_FreeChunk(sound._Medium);
            SDL_mixer.Mix_FreeChunk(sound._Low);
            sound._Scratch = IntPtr.Zero;
            sound._High = IntPtr.Zero;
            sound._Medium = IntPtr.Zero;
            sound._Low = IntPtr.Zero;
            //Free the music
            SDL_mixer.Mix_FreeMusic(sound._Music);
            sound._Music = IntPtr.Zero;
            */
            //clear renderer
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            Program.game.display = displayNum;
        }
    }
}
