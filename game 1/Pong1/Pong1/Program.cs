using SDL2;
using System;
using System.IO;
using System.Text;

namespace Pong
{

    class Program
    {
        private static bool running = true;
        private static bool quit = false;

        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;

        public static IntPtr window;
        public static IntPtr renderer;       
        public static Paddle leftPaddle;
        public static Paddle rightPaddle;
        public static Ball ball;
        public static SDL.SDL_Rect[] centerLine;

        public static SDL.SDL_Event e;

        // music
        private static IntPtr _Music = IntPtr.Zero;

        // sound effects
        public static IntPtr _Scratch = IntPtr.Zero;
        public static IntPtr _High = IntPtr.Zero;
        public static IntPtr _Medium = IntPtr.Zero;
        public static IntPtr _Low = IntPtr.Zero;

        // image
        private static IntPtr pumpkinTexture = IntPtr.Zero;
        private static SDL.SDL_Rect sRect;
        private static SDL.SDL_Rect tRect;

        // font
        public static IntPtr Font = IntPtr.Zero;
        public static IntPtr surfaceMessageR = IntPtr.Zero;
        public static IntPtr MessageR = IntPtr.Zero;
        public static IntPtr surfaceMessageL = IntPtr.Zero;
        public static IntPtr MessageL = IntPtr.Zero;

        private static SDL.SDL_Color Gray;

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

        // <summary>
        // Setup all of the SDL resources we'll need to display a window.
        // </summary>
        public static void Setup()
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
            else
            {
                // SOUND AND MUSIC
                // Initialize SDL_mixer
                if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0)
                {
                    Console.WriteLine("SDL_mixer could not initialize! SDL_mixer Error: {0}", SDL.SDL_GetError());
                }


                var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
                if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
                {
                    Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
                }

                // TEXT
                // Initialize SDL_ttf
                if (SDL_ttf.TTF_Init() == -1)
                {
                    Console.WriteLine("SDL_ttf could not initialize! SDL_ttf Error: {0}", SDL.SDL_GetError());
                }
            }
        }

        /// <summary>
        /// Checks to see if there are any events to be processed.
        /// </summary>
        public static void PollEvents()
        {
            
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //User requests quit
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    quit = true;
                    running = false;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: quit = true; break;
                    }
                }

                // Handle input
                rightPaddle.handleEvent(e, "UP_DOWN");
                leftPaddle.handleEvent(e, "W_S");
            }
        }

        public static bool LoadMedia()
        {
            //Loading success flag
            bool success = true;

            // SOUND AND MUSIC
            //Load music
            _Music = SDL_mixer.Mix_LoadMUS("sound/beat.wav");
            if (_Music == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                success = false;
            }

            //Load sound effects
            _Scratch = SDL_mixer.Mix_LoadWAV("sound/scratch.wav");
            if (_Scratch == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                success = false;
            }

            _High = SDL_mixer.Mix_LoadWAV("sound/high.wav");
            if (_High == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                success = false;
            }

            _Medium = SDL_mixer.Mix_LoadWAV("sound/medium.wav");
            if (_Medium == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                success = false;
            }

            _Low = SDL_mixer.Mix_LoadWAV("sound/low.wav");
            if (_Low == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                success = false;
            }

            //image
            pumpkinTexture = SDL_image.IMG_LoadTexture(renderer, "image/pumpkin.bmp");
            sRect.x = 0;
            sRect.y = 0;
            sRect.w = 128;
            sRect.h = 128;

            tRect.x = SCREEN_WIDTH/2 - 1024 / 2;
            tRect.y = SCREEN_HEIGHT/2 - (1024 -480);
            tRect.w = 1024;
            tRect.h = 1024;

            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref tRect);
            SDL.SDL_RenderCopy(renderer, pumpkinTexture, ref sRect, ref tRect);
            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL.SDL_RenderPresent(renderer);


            // TEXT
            // Open the font
            //Font = SDL_ttf.TTF_OpenFont("font/lazy.ttf", 40);
            Font = SDL_ttf.TTF_OpenFont("font/automati.ttf", 16);
            //Font = SDL_ttf.TTF_OpenFont("font/aerial.ttf", 40);
            //Font = SDL_ttf.TTF_OpenFont("font/Adequate-ExtraLight.ttf", 40);
            if (Font == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load lazy font! SDL_ttf Error: {0}", SDL.SDL_GetError());
                success = false;
            }

            return success;

        }

        public static void LoadElements()
        {
            // Text
            Gray = new SDL.SDL_Color() { r = 150, g = 150, b = 150 };
            

            //center line
            int lines = 13;
            centerLine = new SDL.SDL_Rect[lines];
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

            


            rightPaddle = new Paddle(770, Program.SCREEN_HEIGHT / 2 - 50);
            leftPaddle = new Paddle(20, Program.SCREEN_HEIGHT / 2 - 50);
            // ball
            ball = new Ball();
        }
        public static void Update()
        {         
            rightPaddle.move();
            leftPaddle.move();
            ball.move();
        }

        /// <summary>
        /// Renders to the window.
        /// </summary>
        public static void Render()
        {       
            // Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_RenderCopy(renderer, pumpkinTexture, ref sRect, ref tRect);

            // Draw
            SDL.SDL_SetRenderDrawColor(renderer, 150, 150, 150, 255);
            SDL.SDL_RenderFillRects(renderer, centerLine, 13);


            // Text
            surfaceMessageL = SDL_ttf.TTF_RenderText_Solid(Font, leftPaddle.score.ToString(), Gray);
            MessageL = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessageL);

            SDL.SDL_Rect Message_rect;
            Message_rect.x = 200;
            Message_rect.y = 10;
            Message_rect.w = 100;
            Message_rect.h = 100;
            SDL.SDL_RenderCopy(renderer, MessageL, IntPtr.Zero, ref Message_rect);

            surfaceMessageR = SDL_ttf.TTF_RenderText_Solid(Font, rightPaddle.score.ToString(), Gray);
            MessageR = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessageR);
            SDL.SDL_Rect Message_rect2;
            Message_rect2.x = 500;
            Message_rect2.y = 10;
            Message_rect2.w = 100;
            Message_rect2.h = 100;
            SDL.SDL_RenderCopy(renderer, MessageR, IntPtr.Zero, ref Message_rect2);



            


            rightPaddle.render();
            leftPaddle.render();
            ball.render();

            // Update screen
            SDL.SDL_RenderPresent(renderer);
        }



       

        /// <summary>
        /// Clean up the resources that were created.
        /// </summary>
        public static void CleanUp()
        {

            //Free the sound effects
            SDL_mixer.Mix_FreeChunk(_Scratch);
            SDL_mixer.Mix_FreeChunk(_High);
            SDL_mixer.Mix_FreeChunk(_Medium);
            SDL_mixer.Mix_FreeChunk(_Low);
            _Scratch = IntPtr.Zero;
            _High = IntPtr.Zero;
            _Medium = IntPtr.Zero;
            _Low = IntPtr.Zero;

            //Free the music
            SDL_mixer.Mix_FreeMusic(_Music);
            _Music = IntPtr.Zero;

            // free surface and texture
            //SDL.SDL_FreeSurface(surfaceMessage);
            //SDL.SDL_DestroyTexture(Message);

            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }


        public static void GameOver()
        {
            if (rightPaddle.score == 10)
            { 
                SDL.SDL_SetRenderDrawColor(renderer, 5, 255, 5, 255);
            } 
            else
            {
                SDL.SDL_SetRenderDrawColor(renderer, 255, 5, 5, 255);
            }

            SDL.SDL_RenderClear(renderer);
            SDL.SDL_RenderPresent(renderer);

        }

        // test if one side wins
        // when user presses enter reset data
        public static void Over()
        {
            quit = false;   
            if (rightPaddle.score == 10 || leftPaddle.score == 10)
            {
                while (!quit)
                {
                    PollEvents();
                    GameOver();
                }
                rightPaddle.score = 0;
                leftPaddle.score = 0;
                rightPaddle.mPosY = Program.SCREEN_HEIGHT / 2 - 50;
                leftPaddle.mPosY = Program.SCREEN_HEIGHT / 2 - 50;
            }
           
        }


        




            



    


        static void Main(string[] args)
        {
            
            //runSound("sound.wav", 5000);
            
            Setup();
            
            LoadMedia();
            LoadElements();

            while (running)
            {
                PollEvents();
                Update();
                Render();
                Over();
            }

            CleanUp();



        }
    }
}
