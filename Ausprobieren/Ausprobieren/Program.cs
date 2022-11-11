using SDL2;
using System;

//using System.Globalization;
//using System.Threading;




namespace Ausprobieren
{

    class Program
    {
        public const double PI = 3.14159;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_WIDTH = 800;
        public static IntPtr window;
        public static IntPtr renderer;
        public static IntPtr texture;


        // music
        private static IntPtr _Music = IntPtr.Zero;

        // sound effects
        private static IntPtr _Scratch = IntPtr.Zero;
        private static IntPtr _High = IntPtr.Zero;
        private static IntPtr _Medium = IntPtr.Zero;
        private static IntPtr _Low = IntPtr.Zero;

        // font
        public static IntPtr Font = IntPtr.Zero;

        static void Main(string[] args)
        {

            bool running = true;

            Setup();

            while (running)
            {
                PollEvents();
                LoadMedia();
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
                /*else
                {
                    var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
                    if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
                    {
                        Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
                    }
                }

                IntPtr loadedSurface = SDL_image.IMG_Load("pumpkin.bmp");

                texture = SDL_image.IMG_LoadTexture(renderer, "pumpkin.bmp");
                var image = SDL_image.IMG_Load( "pumpkin.bmp");

                */
            }


            
            bool LoadMedia()
            {
                //Loading success flag
                bool success = true;

// SOUND AND MUSIC
                //Load music
                _Music = SDL_mixer.Mix_LoadMUS("beat.wav");
                if (_Music == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                    success = false;
                }

                //Load sound effects
                _Scratch = SDL_mixer.Mix_LoadWAV("scratch.wav");
                if (_Scratch == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                    success = false;
                }

                _High = SDL_mixer.Mix_LoadWAV("high.wav");
                if (_High == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                    success = false;
                }

                _Medium = SDL_mixer.Mix_LoadWAV("medium.wav");
                if (_Medium == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                    success = false;
                }

                _Low = SDL_mixer.Mix_LoadWAV("low.wav");
                if (_Low == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
                    success = false;
                }


// TEXT
                //Open the font
                Font = SDL_ttf.TTF_OpenFont("lazy.ttf", 24);
                if (Font == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to load lazy font! SDL_ttf Error: {0}", SDL.SDL_GetError());
                    success = false;
                }
                else
                {
                    //Render text
                    var textColor = new SDL.SDL_Color();
                    /*if (!_TextTexture.LoadFromRenderedText("The quick brown fox jumps over the lazy dog", textColor))
                    {
                        Console.WriteLine("Failed to render text texture!");
                        success = false;
                    }*/
                }

                return success;
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


                while (running)
                {
                    // Event handler
                    SDL.SDL_Event e;

                    // Handle events on queue
                    while (SDL.SDL_PollEvent(out e) != 0)
                    {
                        // User requests quit
                        if (e.type == SDL.SDL_EventType.SDL_QUIT)
                        {
                            running = false;
                        }
                        // Handle key press
                        else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                        {
                            switch (e.key.keysym.sym)
                            {
                                // Press 1 or up
                                // Play high sound effect
                                case SDL.SDL_Keycode.SDLK_1:
                                case SDL.SDL_Keycode.SDLK_UP:
                                    SDL_mixer.Mix_PlayChannel(-1, _High, 0);
                                    break;

                                // Press 2 or down
                                // Play medium sound effect
                                case SDL.SDL_Keycode.SDLK_2:
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    SDL_mixer.Mix_PlayChannel(-1, _Medium, 0);
                                    break;

                                // Press 3 or left
                                // Play low sound effect
                                case SDL.SDL_Keycode.SDLK_3:
                                case SDL.SDL_Keycode.SDLK_LEFT:
                                    SDL_mixer.Mix_PlayChannel(-1, _Low, 0);
                                    break;

                                // Press 4 or right
                                // Play scratch sound effect
                                case SDL.SDL_Keycode.SDLK_4:
                                case SDL.SDL_Keycode.SDLK_RIGHT:
                                    SDL_mixer.Mix_PlayChannel(-1, _Scratch, 0);
                                    break;

                                // Press 9
                                // play and pause music
                                case SDL.SDL_Keycode.SDLK_9:
                                    // If there is no music playing
                                    if (SDL_mixer.Mix_PlayingMusic() == 0)
                                    {
                                        // Play the music
                                        SDL_mixer.Mix_PlayMusic(_Music, -1);
                                    }
                                    // If music is being played
                                    else
                                    {
                                        // If the music is paused
                                        if (SDL_mixer.Mix_PausedMusic() == 1)
                                        {
                                            // Resume the music
                                            SDL_mixer.Mix_ResumeMusic();
                                        }
                                        // If the music is playing
                                        else
                                        {
                                            // Pause the music
                                            SDL_mixer.Mix_PauseMusic();
                                        }
                                    }
                                    break;

                                // Press 0
                                case SDL.SDL_Keycode.SDLK_0:
                                    //Stop the music
                                    SDL_mixer.Mix_HaltMusic();
                                    break;
                            }
                        }



                    }



                    



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



                    // Clear screen / Background
                    SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
                    SDL.SDL_RenderClear(renderer);

// TEXT
                    //
                    SDL.SDL_Color White = new SDL.SDL_Color() { r = 255, g = 255, b = 255 };
                    var surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Font, "Text", White);
                    var Message = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessage);

                    SDL.SDL_Rect Message_rect; 
                    Message_rect.x = 500; 
                    Message_rect.y = 300; 
                    Message_rect.w = 200; 
                    Message_rect.h = 100; 

                    SDL.SDL_RenderCopy(renderer, Message, IntPtr.Zero, ref Message_rect);

                    // free surface and texture
                    SDL.SDL_FreeSurface(surfaceMessage);
                    SDL.SDL_DestroyTexture(Message);






                    // center line
                    SDL.SDL_SetRenderDrawColor(renderer, 150, 150, 150, 255);
                    SDL.SDL_RenderFillRects(renderer, centerLine, lines);

                    // Set the color to blue
                    SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 255, 255);

                    // draw hexagon
                    SDL.SDL_RenderDrawLine(renderer, 200, 200, 250, 250);
                    SDL.SDL_RenderDrawLine(renderer, 250, 250, 250, 300);
                    SDL.SDL_RenderDrawLine(renderer, 250, 300, 200, 350);
                    SDL.SDL_RenderDrawLine(renderer, 200, 350, 150, 300);
                    SDL.SDL_RenderDrawLine(renderer, 150, 300, 150, 250);
                    SDL.SDL_RenderDrawLine(renderer, 150, 250, 200, 200);

                    // set color to light gray, almost white
                    SDL.SDL_SetRenderDrawColor(renderer, 250, 250, 250, 255);

                    // lines to a circle
                    float h = 100;
                    float k = 500;
                    float radius = 20;
                    double step = 0.2;
                    float new_x, new_y;
                    float old_x = 200;
                    float old_y = 400;
                    for (double theta = 0; theta < (PI * 2); theta += step)
                    {
                        new_x = h + (radius * (float)Math.Cos(theta));
                        new_y = k - (radius * (float)Math.Sin(theta));
                        SDL.SDL_RenderDrawLine(renderer, (int)old_x, (int)old_y,
                                             (int)new_x, (int)new_y);
                    }

                    h = 200;
                    k = 100;
                    radius = 20;
                    step = 0.2;
                    old_x = h;
                    old_y = k;
                    for (double theta = 0; theta < (PI * 2); theta += step)
                    {
                        new_x = h + (radius * (float)Math.Cos(theta));
                        new_y = k - (radius * (float)Math.Sin(theta));
                        SDL.SDL_RenderDrawLine(renderer, (int)old_x, (int)old_y,
                                             (int)new_x, (int)new_y);
                    }

                    // circle
                    h = 300;
                    k = h;
                    radius = 20;
                    step = 0.01;
                    old_x = h + radius;
                    old_y = h;

                    for (double theta = 0; theta < (PI * 2); theta += step)
                    {
                        new_x = h + (radius * (float)Math.Cos(theta));
                        new_y = k - (radius * (float)Math.Sin(theta));
                        SDL.SDL_RenderDrawLine(renderer, (int)old_x, (int)old_y,
                                             (int)new_x, (int)new_y);
                        old_x = new_x;
                        old_y = new_y;
                    }

                    // set color to red
                    SDL.SDL_SetRenderDrawColor(renderer, 250, 0, 0, 255);
                    // circle 
                    DrawCircle(50, 150, 20);


                    texture = SDL_image.IMG_LoadTexture(renderer, "pumpkin.bmp");
                    SDL.SDL_Rect sRect;
                    sRect.x = 0;
                    sRect.y = 0;
                    sRect.w = 128;
                    sRect.h = 128;

                    SDL.SDL_Rect tRect;
                    tRect.x = 420;
                    tRect.y = 20;
                    tRect.w = 128;
                    tRect.h = 128;

                    //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref tRect);
                    SDL.SDL_RenderCopy(renderer, texture, ref sRect, ref tRect);
                    //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);

                    // Switches out the currently presented render surface with the one we just did work on.
                    SDL.SDL_RenderPresent(renderer);


                }




                
            }

            /// <summary>
            /// Clean up the resources that were created.
            /// </summary>
            void CleanUp()
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


                SDL.SDL_DestroyTexture(texture);
                SDL.SDL_DestroyRenderer(renderer);
                SDL.SDL_DestroyWindow(window);

                //Quit SDL subsystems
                SDL_mixer.Mix_Quit();
                SDL_image.IMG_Quit();
                SDL.SDL_Quit();
            }



            void DrawCircle(int centreX, int centreY, int radius)
            {
                int diameter = radius * 2;

                int x = radius - 1;
                int y = 0;
                int tx = 1;
                int ty = 1;
                int error = tx - diameter;

                while (x >= y)
                {
                    //  Each of the following renders an octant of the circle
                    SDL.SDL_RenderDrawPoint(renderer, centreX + x, centreY - y);
                    SDL.SDL_RenderDrawPoint(renderer, centreX + x, centreY + y);
                    SDL.SDL_RenderDrawPoint(renderer, centreX - x, centreY - y);
                    SDL.SDL_RenderDrawPoint(renderer, centreX - x, centreY + y);
                    SDL.SDL_RenderDrawPoint(renderer, centreX + y, centreY - x);
                    SDL.SDL_RenderDrawPoint(renderer, centreX + y, centreY + x);
                    SDL.SDL_RenderDrawPoint(renderer, centreX - y, centreY - x);
                    SDL.SDL_RenderDrawPoint(renderer, centreX - y, centreY + x);

                    if (error <= 0)
                    {
                        ++y;
                        error += ty;
                        ty += 2;
                    }

                    if (error > 0)
                    {
                        --x;
                        tx += 2;
                        error += (tx - diameter);
                    }
                }
            }





            }
    }
}
