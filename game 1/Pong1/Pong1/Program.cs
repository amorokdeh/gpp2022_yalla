using SDL2;
using System;
using System.IO;
using System.Text;

namespace Pong
{

    class Ball
    {
        int mPosX = Program.SCREEN_WIDTH / 2 - 5;
        int mPosY = Program.SCREEN_HEIGHT / 2 - 5;

        //The velocity
        int mVelX = 1;


        SDL.SDL_Rect ball;

        public Ball() 
        {

            ball = new SDL.SDL_Rect
            {
                x = mPosX,
                y = mPosY,
                w = 10,
                h = 10
            };
        }

        public void move()
        {
            mPosX = mPosX + mVelX;
            if ((mPosX < 0) || (mPosX + 10 > Program.SCREEN_WIDTH))
            {
                mVelX = -mVelX;
            }
        }

        public void render()
        {

            ball.x = mPosX;
            SDL.SDL_SetRenderDrawColor(Program.renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(Program.renderer, ref ball);
        }

    }

    class Paddle
    {
        const int PADDLE_WIDTH = 10;
        const int PADDLE_HEIGHT = 100;

        const int PADDLE_VEL = 10;

        //The X and Y offsets
        int mPosX;
        int mPosY;

        //The velocity
        int mVelY = 0;

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
                h = PADDLE_HEIGHT
            };
        }

        public void handleEvent(SDL.SDL_Event e, string upDownButtons)
        {
            if (upDownButtons.Equals("UP_DOWN")) { 
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

            if (upDownButtons.Equals("W_S")) { 
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
            if ((mPosY < 0) || (mPosY + PADDLE_HEIGHT > Program.SCREEN_HEIGHT))
            {
                //Move back
                mPosY -= mVelY;
            }
        }

        public void render()
        {

            paddle.y = mPosY;
            SDL.SDL_SetRenderDrawColor(Program.renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(Program.renderer, ref paddle);
        }

    }

    class Program
    {
        public const double PI = 3.14159;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;
        public static IntPtr window;
        public static IntPtr renderer;
        
        public static void runSound(string path, uint time) { 
            SDL.SDL_Init(SDL.SDL_INIT_AUDIO);
            SDL.SDL_AudioSpec spec = new SDL.SDL_AudioSpec();

            //string path = Path.Combine(Directory.GetCurrentDirectory() + "\\sound.wav");

            SDL.SDL_LoadWAV(path, ref spec, out IntPtr audioBuffer, out uint audioLength);
            uint deviceId = SDL.SDL_OpenAudioDevice(null, 0, ref spec, out SDL.SDL_AudioSpec obtained, 0);
            SDL.SDL_QueueAudio(deviceId, audioBuffer, audioLength);
            SDL.SDL_PauseAudioDevice(deviceId, 0);
            SDL.SDL_Delay(time);
            SDL.SDL_CloseAudioDevice(deviceId);
            SDL.SDL_FreeWAV(audioBuffer);
            SDL.SDL_Quit();
        }

        static void Main(string[] args)
        {
            
            runSound("sound.wav", 5000);
            bool running = true;

            Setup();

            while (running)
            {
                PollEvents();
                Render();
            }

            CleanUp();

            // <summary>
            // Setup all of the SDL resources we'll need to display a window.
            // </summary>
            void Setup()
            {
                // Initilizes SDL.
                if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
                {
                    Console.WriteLine($"There was an issue initializing SDL. {SDL.SDL_GetError()}");
                }

                // Create a new window given a title, size, and passes it a flag indicating it should be shown.
                window = SDL.SDL_CreateWindow(
                    "SDL .NET 6 Tutorial",
                    SDL.SDL_WINDOWPOS_UNDEFINED,
                    SDL.SDL_WINDOWPOS_UNDEFINED,
                    SCREEN_WIDTH,
                    SCREEN_HEIGHT,
                    SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

                if (window == IntPtr.Zero)
                {
                    Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
                }

                // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
                renderer = SDL.SDL_CreateRenderer(
                    window,
                    -1,
                    SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                    SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

                if (renderer == IntPtr.Zero)
                {
                    Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
                }
            }

            /// <summary>
            /// Checks to see if there are any events to be processed.
            /// </summary>
            void PollEvents()
            {
                // Check to see if there are any events and continue to do so until the queue is empty.
                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            running = false;
                            break;
                    }
                }
            }

            /// <summary>
            /// Renders to the window.
            /// </summary>
            void Render()
            {

                //center line
                int lines = 13;
                SDL.SDL_Rect[] centerLine = new SDL.SDL_Rect[lines];
                int line = 0;
                for (int i = 0; i < lines; i++)
                {
                    centerLine[line] =
                        new SDL.SDL_Rect
                        {
                            x = SCREEN_WIDTH / 2 - 2,
                            y = line * 48,
                            w = 4,
                            h = 24
                        };
                    line++;
                }

                // ball
                Ball ball = new Ball();
                
                SDL.SDL_Event e;
                Paddle rightPaddle = new Paddle(770, Program.SCREEN_HEIGHT / 2 - 50);
                Paddle leftPaddle = new Paddle(20, 60);

                while (running)
                {
                    // Handle events on queue
                    while (SDL.SDL_PollEvent(out e) != 0)
                    {
                        //User requests quit
                        if (e.type == SDL.SDL_EventType.SDL_QUIT)
                        {
                            running = false;
                        }

                        // Handle input
                        rightPaddle.handleEvent(e, "UP_DOWN");
                        leftPaddle.handleEvent(e, "W_S");
                    }
                    //ball.x = ball.x + 1;
                    ball.move();

                    rightPaddle.move();
                    leftPaddle.move();

                    //Clear screen
                    SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
                    SDL.SDL_RenderClear(renderer);

                    // ----draw----
                    // center line
                    SDL.SDL_SetRenderDrawColor(renderer, 150, 150, 150, 255);
                    SDL.SDL_RenderFillRects(renderer, centerLine, lines);
                    // right paddle
                    rightPaddle.render();
                    // left paddle
                    leftPaddle.render();
                    //ball
                    ball.render();
                
                    //SDL.SDL_Surface 
                    
                    // Update screen
                    SDL.SDL_RenderPresent(renderer);
                }

                // Switches out the currently presented render surface with the one we just did work on.
                SDL.SDL_RenderPresent(renderer);
            }

            /// <summary>
            /// Clean up the resources that were created.
            /// </summary>
            void CleanUp()
            {
                SDL.SDL_DestroyRenderer(renderer);
                SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();
            }

        }
    }
}
