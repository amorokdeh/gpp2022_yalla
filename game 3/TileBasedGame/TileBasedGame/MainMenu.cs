using SDL2;
using System;
using static SDL2.SDL;


namespace TileBasedGame
{
    class MainMenu
    {
        public bool Running = true;
        public bool Quit = false;
        public Text Txt = new Text();
        public string Text;
        public int NextText = 0;
        public IntPtr SurfaceMessage;
        public SDL.SDL_Color Color;
        public int TextSize = 30;
        public String MenuSelected = "main menu";

        private GameObjectManager _objects = new GameObjectManager();
        private PhysicsManager _physics = new PhysicsManager();
        private RenderingManager _rendering = new RenderingManager();
        private AIManager _ai = new AIManager();

        private IntPtr _gGameController = new IntPtr();
        private string _axisY = "None";
        private bool _movingY = false;

        private Choices _selected = Choices.StartGame;

        //All menus and topics
        public enum Choices
        {
            StartGame,
            Options,
            Quit,

            Level1,
            Level2,
            Level3,
            BackToMainMenu,

            Window,
            SoundsVolume,
            MusicVolume,
            BackMainMenu,

            SoundUp,
            SoundDown,
            BackToOption,

            MusicUp,
            MusicDown,
            BackToTheOption,

            Screen,
            FpsLimit,
            ShowFPS,
            ScreenSize,
            BackOption
        }
        

        public MainMenu()
        {
            Setup();
        }

        public bool Setup()
        {
            //text
            Txt.SetUp();
            Txt.LoadText(1);
            Color = Txt.White;

            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 60, 20, 255);
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
                _gGameController = SDL.SDL_JoystickOpen(0);
                if (_gGameController == null)
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
        public void Run()
        {
            Program.Game.Audio.RunMusic("Menu music");
            Program.Game.Camera.PosY = Program.Window.Height / 2;
            Program.Game.Camera.PosX = Program.Window.Width / 2;
            while (Running)
            {
                Program.Window.CalculateFPS(); //frame limit start calculating here
                Render();
                Control();
                Program.Window.DeltaFPS(); //frame limit end calculating here
            }
            if (Quit) { CloseAndGoTo(LevelManager.GameState.Quit); } //close the game
        }

        public void Control()
        {
            //Key
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //User requests quit
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Running = false;
                    Quit = true;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: GoTo(); break;
                        case SDL.SDL_Keycode.SDLK_UP: GoUp(); break;
                        case SDL.SDL_Keycode.SDLK_DOWN: GoDown(); break;
                    }
                }

                //Controller
                int y = SDL.SDL_JoystickGetAxis(_gGameController, 1);
                int movingPoint = 16384; //you can change it (It works perfectly on PS5 controller)

                if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
                {
                    if (y < -movingPoint ) // Moved up
                    {
                        _axisY = "Up";
                    }
                    else if (y > movingPoint ) // Moved down
                    {
                        _axisY = "Down";
                    }
                    else //stoped moving
                    {
                        _axisY = "None";
                        _movingY = false;
                    }
                    if (_axisY == "Up" && !_movingY)   { GoUp(); _movingY = true; }
                    if (_axisY == "Down" && !_movingY) { GoDown(); _movingY = true; }
                }
            }
        }
        public void GoTo()
        {
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Click));
            
            if (MenuSelected.Equals("main menu"))
            {
                switch (_selected)
                {
                    case Choices.StartGame:
                        MenuSelected = "levels";
                        _selected = Choices.Level1;
                        Program.Game.InfoBox.keysInfo();
                        return;
                    case Choices.Options:
                        MenuSelected = "option";
                        _selected = Choices.Window;
                        return;
                    case Choices.Quit:
                        EndGame();
                        return;
                }
            }
            else if (MenuSelected.Equals("levels"))
            {
                switch (_selected)
                {
                    case Choices.Level1:
                        StartLevel1();
                        return;
                    case Choices.Level2:
                        StartLevel2();
                        return;
                    case Choices.Level3:
                        StartLevel3();
                        return;
                    case Choices.BackToMainMenu:
                        MenuSelected = "main menu";
                        _selected = Choices.StartGame;
                        return;
                }
            }
            else if (MenuSelected.Equals("option"))
            {
                switch (_selected)
                {
                    case Choices.Window:
                        MenuSelected = "window";
                        _selected = Choices.Screen;
                        return;
                    case Choices.SoundsVolume:
                        MenuSelected = "soundUpDown";
                        _selected = Choices.SoundUp;
                        return;
                    case Choices.MusicVolume:
                        MenuSelected = "MusicUpDown";
                        _selected = Choices.MusicUp;
                        return;
                    case Choices.BackMainMenu:
                        MenuSelected = "main menu";
                        _selected = Choices.StartGame;
                        return;
                }
            }
            else if (MenuSelected.Equals("soundUpDown"))
            {
                switch (_selected)
                {

                    case Choices.SoundUp:
                        Program.Game.Audio.ChangeVolumeSound(Program.Game.Audio.GetVolumeSound() + Globals.AudioStep);
                        return;
                    case Choices.SoundDown:
                        Program.Game.Audio.ChangeVolumeSound(Program.Game.Audio.GetVolumeSound() - Globals.AudioStep);
                        return;
                    case Choices.BackToOption:
                        MenuSelected = "option";
                        _selected = Choices.Window;
                        return;
                }
            }
            else if (MenuSelected.Equals("MusicUpDown"))
            {
                switch (_selected)
                {

                    case Choices.MusicUp:
                        Program.Game.Audio.ChangeVolumeMusic(Program.Game.Audio.GetVolumeMusic() + Globals.AudioStep);
                        return;
                    case Choices.MusicDown:
                        Program.Game.Audio.ChangeVolumeMusic(Program.Game.Audio.GetVolumeMusic() - Globals.AudioStep);
                        return;
                    case Choices.BackToTheOption:
                        MenuSelected = "option";
                        _selected = Choices.Window;
                        return;
                }
            }
            else if (MenuSelected.Equals("window"))
            {
                switch (_selected)
                {
                    case Choices.Screen:
                        Program.Window.ChangeScreenMode();
                        _selected = Choices.Screen;
                        return;
                    case Choices.FpsLimit:
                        Program.Window.ChangeFPSLimit();
                        return;
                    case Choices.ShowFPS:
                        Program.Window.ShowHideFPS();
                        return;
                    case Choices.ScreenSize:
                        Program.Window.ChangeWindowSize();
                        BuildBackground("main menu");
                        Program.Game.Camera.PosY = Program.Window.Height / 2;
                        Program.Game.Camera.PosX = Program.Window.Width / 2;
                        return;
                    case Choices.BackOption:
                        MenuSelected = "option";
                        _selected = Choices.Window;
                        return;
                }
            }
        }
        public void GoUp()
        {
            if ((MenuSelected.Equals("option")) && (_selected > Choices.Window))
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));               
                return;
            }
            else if ((MenuSelected.Equals("window")) && (_selected > Choices.Screen))
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("levels")) && (_selected > Choices.Level1))
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("soundUpDown")) && _selected > Choices.SoundUp)
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("MusicUpDown")) && _selected > Choices.MusicUp)
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("main menu")) && _selected > Choices.StartGame)
            {
                _selected--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }

        }
        public void GoDown()
        {

            if ((MenuSelected.Equals("option")) && (_selected < Choices.BackMainMenu))
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("window")) && (_selected < Choices.BackOption))
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("levels")) && (_selected < Choices.BackToMainMenu))
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("soundUpDown")) && (_selected < Choices.BackToOption))
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("MusicUpDown")) && (_selected < Choices.BackToTheOption))
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
            else if ((MenuSelected.Equals("main menu")) && _selected < Choices.Quit)
            {
                _selected++;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MenuButton));
                return;
            }
        }

        public void EndGame()
        {
            Running = false;
            CloseAndGoTo(LevelManager.GameState.Quit);

        }

        public void StartLevel1()
        {
            Running = false;
            CloseAndGoTo(LevelManager.GameState.Level1);
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level1));

        }
        public void StartLevel2()
        {
            Running = false;
            CloseAndGoTo(LevelManager.GameState.Level2);
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level2));

        }
        public void StartLevel3()
        {
            Running = false;
            CloseAndGoTo(LevelManager.GameState.Level3);
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level3));

        }
        public int NextTextPos()
        {
            NextText += 50;
            return NextText;
        }

        public void TextPrinter(String textIndex, Choices menu)
        {
            Text = textIndex;
            CheckSelected(menu);
            SurfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, Text, Color);
            Txt.AddText(Program.Window.Renderer, SurfaceMessage, Program.Window.Width / 2 - Text.Length * 10, NextTextPos(), Text.Length * 20, TextSize);
        }

        public void BuildBackground(string source)
        {
            int winW = Program.Window.Width;
            int winH = Program.Window.Height;

            GameObject bg;
            Image backImg = Program.Game.Loader.BackgroundImg;
            bg = _objects.CreateGameBackground(source, winW, winH);
            bg.Active = true;
            bg.AddComponent(_rendering.CreateComponent(backImg, winW, winH, 1920, 1080));
        }
        public void Render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(Program.Window.Renderer);
            //Background
            _rendering.Render();
            
            //calculate FPS
            Program.Window.FPSCalculate();

            //Menu-Texte
            if (MenuSelected.Equals("option"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Window", Choices.Window);
                TextPrinter("Sounds volume", Choices.SoundsVolume);
                TextPrinter("Music volume", Choices.MusicVolume);
                TextPrinter("Back", Choices.BackMainMenu);

            }
            else if (MenuSelected.Equals("window"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Screen: " + Program.Window.ScreenMode, Choices.Screen);
                TextPrinter("FPS limit: " + Program.Window.LimitedFPS, Choices.FpsLimit);
                TextPrinter("Show FPS on display: " + Program.Window.ShowFPSRunning, Choices.ShowFPS);
                TextPrinter("Screen size: " + Program.Window.Width + " x " + Program.Window.Height, Choices.ScreenSize);
                TextPrinter("Back", Choices.BackOption);
            }
            else if (MenuSelected.Equals("main menu"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Start game", Choices.StartGame);
                TextPrinter("Options", Choices.Options);
                TextPrinter("Quit", Choices.Quit);
            }
            else if (MenuSelected.Equals("levels"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Level 1", Choices.Level1);
                TextPrinter("Level 2", Choices.Level2);
                TextPrinter("Level 3", Choices.Level3);
                TextPrinter("Back", Choices.BackToMainMenu);
            }
            else if (MenuSelected.Equals("soundUpDown"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Sound volume: " + Program.Game.Audio.GetVolumeSound().ToString(), Choices.StartGame);
                TextPrinter("+", Choices.SoundUp);
                TextPrinter("-", Choices.SoundDown);
                TextPrinter("Back", Choices.BackToOption);
            }
            else if (MenuSelected.Equals("MusicUpDown"))
            {
                NextText = Program.Window.Height / 3;

                TextPrinter("Music volume: " + Program.Game.Audio.GetVolumeMusic().ToString(), Choices.StartGame);
                TextPrinter("+", Choices.MusicUp);
                TextPrinter("-", Choices.MusicDown);
                TextPrinter("Back", Choices.BackToTheOption);
            }

            SDL.SDL_RenderPresent(Program.Window.Renderer);
        }

        public void CheckSelected(Choices choice)
        {
            if (choice == _selected)
            {
                Color = Txt.Green;
                TextSize = Globals.BigTextSize;
            }
            else
            {
                Color = Txt.White;
                TextSize = Globals.NormalTextSize;
            }
        }

        public void CloseAndGoTo(LevelManager.GameState gs)
        {
            LevelManager.display = gs;
        }
    }
}

