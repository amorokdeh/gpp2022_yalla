using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SDL2.SDL;
using static System.Net.Mime.MediaTypeNames;

namespace ShootEmUp
{
    class MainMenu
    {
        public bool running = true;
        public bool quit = false;
        public Text txt = new Text();
        public string text;
        public int nextText = 0;
        public IntPtr surfaceMessage;
        public SDL.SDL_Color color;
        public int textSize = 30;
        public String menuSelected = "main menu";
        private GameObjectManager _objects = new GameObjectManager();
        private PhysicsManager _physics = new PhysicsManager();
        private RenderingManager _rendering = new RenderingManager();
        private AIManager _ai = new AIManager();
        IntPtr gGameController = new IntPtr();
        string axisY = "None";
        bool movingY = false;
        //create a rect
        SDL_Rect rect;

        //All menus and topics
        public enum Choices
        {
            Start_game,
            Options,
            Quit,

            Level1,
            Level2,
            Level3,
            BackToMainMenu,

            Window,
            soundsVolume,
            musicVolume,
            BackMainMenu,

            soundUp,
            soundDown,
            BackToOption,

            musicUp,
            musicDown,
            BackToTheOption,

            Screen,
            FpsLimit,
            showFPS,
            ScreenSize,
            BackOption

        }
        private Choices selected = Choices.Start_game;

        public MainMenu()
        {
            setup();
        }

        public bool setup()
        {
            //text
            txt.setUp();
            txt.loadText(1);
            color = txt.White;

            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 60, 20, 255);
            BuildBackground("main menu");

            //Controller
            bool success = true;
            SDL.SDL_Init(SDL.SDL_INIT_JOYSTICK); //Init SDL and Control


            //Check for joysticks
            if (SDL.SDL_NumJoysticks() <= 1)
            {
                Console.WriteLine("Warning: No Controller connected!\n");
                success = false;
            }
            else
            {
                //Load joystick
                gGameController = SDL.SDL_JoystickOpen(0);
                if (gGameController == null)
                {
                    Console.WriteLine("Warning: Unable to open game controller! SDL Error: %s\n", SDL.SDL_GetError());
                    success = false;
                }
            }
            if (success)
            {
                Console.WriteLine("Controller connected!");
            }
            return success;

        }
        public void run()
        {
            Program.game._audio.runMusic("Menu music");
            while (running)
            {
                Program.window.calculateFPS(); //frame limit start calculating here
                render();
                controll();
                Program.window.deltaFPS(); //frame limit end calculating here

            }

            if (quit) { closeAndGoTo(LevelManager.GameState.Quit); } //close the game
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
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: goTo(); break;
                        case SDL.SDL_Keycode.SDLK_UP: goUp(); break;
                        case SDL.SDL_Keycode.SDLK_DOWN: goDown(); break;
                    }
                }

                //Controller
                int y = SDL.SDL_JoystickGetAxis(gGameController, 1);
                int movingPoint = 16384; //you can change it (It works perfectly on PS5 controller)

                if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
                {
                    if (y < -movingPoint ) // Moved up
                    {
                        axisY = "Up";
                    }
                    else if (y > movingPoint ) // Moved down
                    {
                        axisY = "Down";
                    }
                    else //stoped moving
                    {
                        axisY = "None";
                        movingY = false;
                    }
                    if (axisY == "Up" && !movingY)   { goUp(); movingY = true; }
                    if (axisY == "Down" && !movingY) { goDown(); movingY = true; }
                }
            }
        }
        public void goTo()
        {
            Program.game._audio.runSound("Menu click");
            if (menuSelected.Equals("main menu"))
            {
                switch (selected)
                {
                    case Choices.Start_game:
                        menuSelected = "levels";
                        selected = Choices.Level1;
                        return;
                    case Choices.Options:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                    case Choices.Quit:
                        endGame();
                        return;
                }
            }
            else if (menuSelected.Equals("levels"))
            {
                switch (selected)
                {
                    case Choices.Level1:
                        startLevel1();
                        return;
                    case Choices.Level2:
                        startLevel2();
                        return;
                    case Choices.Level3:
                        startLevel3();
                        return;
                    case Choices.BackToMainMenu:
                        menuSelected = "main menu";
                        selected = Choices.Start_game;
                        return;
                }
            }
            else if (menuSelected.Equals("option"))
            {
                switch (selected)
                {
                    case Choices.Window:
                        menuSelected = "window";
                        selected = Choices.Screen;
                        return;
                    case Choices.soundsVolume:
                        menuSelected = "soundUpDown";
                        selected = Choices.soundUp;
                        return;
                    case Choices.musicVolume:
                        menuSelected = "MusicUpDown";
                        selected = Choices.musicUp;
                        return;
                    case Choices.BackMainMenu:
                        menuSelected = "main menu";
                        selected = Choices.Start_game;
                        return;
                }
            }
            else if (menuSelected.Equals("soundUpDown"))
            {
                switch (selected)
                {

                    case Choices.soundUp:
                        Program.game._audio.changeVolumeSound(Program.game._audio.getVolumeSound() + 10);
                        return;
                    case Choices.soundDown:
                        Program.game._audio.changeVolumeSound(Program.game._audio.getVolumeSound() - 10);
                        return;
                    case Choices.BackToOption:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                }
            }
            else if (menuSelected.Equals("MusicUpDown"))
            {
                switch (selected)
                {

                    case Choices.musicUp:
                        Program.game._audio.changeVolumeMusic(Program.game._audio.getVolumeMusic() + 10);
                        return;
                    case Choices.musicDown:
                        Program.game._audio.changeVolumeMusic(Program.game._audio.getVolumeMusic() - 10);
                        return;
                    case Choices.BackToTheOption:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                }
            }
            else if (menuSelected.Equals("window"))
            {
                switch (selected)
                {
                    case Choices.Screen:
                        Program.window.changeScreenMode();
                        selected = Choices.Screen;
                        return;
                    case Choices.FpsLimit:
                        Program.window.changeFPSLimit();
                        return;
                    case Choices.showFPS:
                        Program.window.showHideFPS();
                        return;
                    case Choices.ScreenSize:
                        Program.window.changeWindowSize();
                        return;
                    case Choices.BackOption:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                }
            }
        }
        public void goUp()
        {
            if ((menuSelected.Equals("option")) && (selected > Choices.Window))
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("window")) && (selected > Choices.Screen))
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("levels")) && (selected > Choices.Level1))
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("soundUpDown")) && selected > Choices.soundUp)
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("MusicUpDown")) && selected > Choices.musicUp)
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("main menu")) && selected > Choices.Start_game)
            {
                selected--;
                Program.game._audio.runSound("Menu buttons");
                return;
            }

        }
        public void goDown()
        {

            if ((menuSelected.Equals("option")) && (selected < Choices.BackMainMenu))
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("window")) && (selected < Choices.BackOption))
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("levels")) && (selected < Choices.BackToMainMenu))
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("soundUpDown")) && (selected < Choices.BackToOption))
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("MusicUpDown")) && (selected < Choices.BackToTheOption))
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
            else if ((menuSelected.Equals("main menu")) && selected < Choices.Quit)
            {
                selected++;
                Program.game._audio.runSound("Menu buttons");
                return;
            }
        }

        public void endGame()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Quit);

        }

        public void startLevel1()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level1);
            Program.game._audio.stopMusic();
            Program.game._audio.runMusic("Level1 music");


        }
        public void startLevel2()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level2);
            Program.game._audio.stopMusic();
            Program.game._audio.runMusic("Level2 music");

        }
        public void startLevel3()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level3);
            Program.game._audio.stopMusic();
            Program.game._audio.runMusic("Level3 music");

        }
        public int nextTextPos()
        {
            nextText += 50;
            return nextText;
        }

        public void textPrinter(String textIndex, Choices menu)
        {
            text = textIndex;
            checkSelected(menu);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width / 2 - text.Length * 10, nextTextPos(), text.Length * 20, textSize);
        }

        public void BuildBackground(string source)
        {
            int winW = Program.window.width;
            int winH = Program.window.height;

            GameObject bg;


            for (int i = -1; i < (winH / 128 * 4); i++)
            {
                bg = _objects.CreateGameBackground(source, 128 * 4, 64 * 4, 0, 64 * 4 * i);
                bg.Active = true;

                for (int j = 0; j < winW / (64 * 4); j++)
                {
                    bg.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4, 128 * 4 * j));
                }
            }
        }
        public void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(Program.window.renderer);
            //Background
            _rendering.Render();

            //Rect position
            rect.x = (Program.window.width / 2) - 300;
            rect.y = (Program.window.height / 2) - 200;
            rect.w = 600;
            rect.h = 400;
            //draw and fill rect
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 0, 0, 255);
            SDL.SDL_RenderDrawRect(Program.window.renderer, ref rect);
            SDL.SDL_RenderFillRect(Program.window.renderer, ref rect);
            //calculate FPS
            Program.window.fpsCalculate();

            //Menu-Texte
            if (menuSelected.Equals("option"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Window", Choices.Window);
                textPrinter("Sounds volume", Choices.soundsVolume);
                textPrinter("Music volume", Choices.musicVolume);
                textPrinter("Back", Choices.BackMainMenu);

            }
            else if (menuSelected.Equals("window"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Screen: " + Program.window.screenMode, Choices.Screen);
                textPrinter("FPS limit: " + Program.window.limitedFPS, Choices.FpsLimit);
                textPrinter("Show FPS on display: " + Program.window.showFPSRunning, Choices.showFPS);
                textPrinter("Screen size: " + Program.window.width + " x " + Program.window.height, Choices.ScreenSize);
                textPrinter("Back", Choices.BackOption);
            }
            else if (menuSelected.Equals("main menu"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Start game", Choices.Start_game);
                textPrinter("Options", Choices.Options);
                textPrinter("Quit", Choices.Quit);
            }
            else if (menuSelected.Equals("levels"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Level 1", Choices.Level1);
                textPrinter("Level 2", Choices.Level2);
                textPrinter("Level 3", Choices.Level3);
                textPrinter("Back", Choices.BackToMainMenu);
            }
            else if (menuSelected.Equals("soundUpDown"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Sound volume: " + Program.game._audio.getVolumeSound().ToString(), Choices.Start_game);
                textPrinter("+", Choices.soundUp);
                textPrinter("-", Choices.soundDown);
                textPrinter("Back", Choices.BackToOption);
            }
            else if (menuSelected.Equals("MusicUpDown"))
            {
                nextText = Program.window.height / 3;

                textPrinter("Music volume: " + Program.game._audio.getVolumeMusic().ToString(), Choices.Start_game);
                textPrinter("+", Choices.musicUp);
                textPrinter("-", Choices.musicDown);
                textPrinter("Back", Choices.BackToTheOption);
            }

            SDL.SDL_RenderPresent(Program.window.renderer);
        }

        public void checkSelected(Choices choice)
        {
            if (choice == selected)
            {
                color = txt.Green;
                textSize = 40;
            }
            else
            {
                color = txt.White;
                textSize = 30;
            }
        }

        public void closeAndGoTo(LevelManager.GameState gs)
        {
            LevelManager.display = gs;

        }
    }
}

