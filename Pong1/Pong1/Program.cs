using SDL2;
using System;





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

    class RightPaddle
    {
        const int RIGHT_PADDLE_WIDTH = 10;
        const int RIGHT_PADDLE_HEIGHT = 100;

        const int RIGHT_PADDLE_VEL = 10;

        //The X and Y offsets
        int mPosX = 770;
        int mPosY = Program.SCREEN_HEIGHT / 2 - 50;

        //The velocity
        int mVelY = 0;

        SDL.SDL_Rect rightPaddle;

        public RightPaddle() 
        {
            rightPaddle = new SDL.SDL_Rect
            {
                x = mPosX,
                y = mPosY,
                w = RIGHT_PADDLE_WIDTH,
                h = RIGHT_PADDLE_HEIGHT
            };
        }

        public void handleEvent(SDL.SDL_Event e)
        {
            //If a key was pressed
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: mVelY -= RIGHT_PADDLE_VEL; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: mVelY += RIGHT_PADDLE_VEL; break;
                }
            }
            //If a key was released
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: mVelY += RIGHT_PADDLE_VEL; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: mVelY -= RIGHT_PADDLE_VEL; break;
                }
            }
        }

        public void move()
        {

            //Move up or down
            mPosY += mVelY;

            //If the dot went too far up or down
            if ((mPosY < 0) || (mPosY + RIGHT_PADDLE_HEIGHT > Program.SCREEN_HEIGHT))
            {
                //Move back
                mPosY -= mVelY;
            }
        }

        public void render()
        {

            rightPaddle.y = mPosY;
            SDL.SDL_SetRenderDrawColor(Program.renderer, 250, 250, 250, 255);
            SDL.SDL_RenderFillRect(Program.renderer, ref rightPaddle);
        }

    }

    class Program
    {
        public const double PI = 3.14159;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;
        public static IntPtr window;
        public static IntPtr renderer;

        static void Main(string[] args)
        {

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

                // left paddle
                var leftPaddle = new SDL.SDL_Rect
                {
                    x = 20,
                    y = 20,
                    w = 10,
                    h = 100
                };
                
                // ball
                Ball ball = new Ball();
                
                



                SDL.SDL_Event e;
                RightPaddle rightPaddle = new RightPaddle();

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
                        rightPaddle.handleEvent(e);
                    }
                    //ball.x = ball.x + 1;
                    ball.move();

                    


                    rightPaddle.move();


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
                    SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);
                    SDL.SDL_RenderFillRect(renderer, ref leftPaddle);
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
