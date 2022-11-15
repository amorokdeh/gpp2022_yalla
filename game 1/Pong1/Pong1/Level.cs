using System;
using SDL2;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Level
    {
        public IntPtr renderer;
        public bool running = true;
        public bool quit = false;

        public Ball ball;
        public Paddle rightPaddle;
        public Paddle leftPaddle;

        public Image img = new Image();
        public Text txt = new Text();
        public int win = 10; //score to win

        public virtual void setup()
        {
            renderer = Program.window.renderer;

        }

        public virtual void update()
        {
            rightPaddle.move();
            leftPaddle.move();
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
                checkWithdrow(e);
            }
        }

        public void checkWithdrow(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0){
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_ESCAPE: running = false; closeAndGoTo(1); break; //quit game
                }
            }
        }

        public virtual void render() 
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);
        }

        private void over()
        {
            if (rightPaddle.score == win)
            {
                Program.game.mainMenu.winner = 2;
                running = false;
                Thread.Sleep(300);
                closeAndGoTo(4); // go to game over
            }
            else if (leftPaddle.score == win)
            {
                Program.game.mainMenu.winner = 2;
                running = false;
                Thread.Sleep(300);
                closeAndGoTo(4); // go to game over
            }
        }


        public void run()
        {

            while (running)
            {
                controll();
                update();
                render();
                over();
                
            }

            if (quit) { closeAndGoTo(0); } //close the game
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
            //SDL.SDL_RenderClear(renderer);
            //SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            Program.game.display = displayNum;
        }
    }
}
