using SDL2;
using System;





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



                //Clear screen / Background
                SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
                SDL.SDL_RenderClear(renderer);

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

            /// <summary>
            /// Clean up the resources that were created.
            /// </summary>
            void CleanUp()
            {
                SDL.SDL_DestroyTexture(texture);
                SDL.SDL_DestroyRenderer(renderer);
                SDL.SDL_DestroyWindow(window);
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
