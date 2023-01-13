using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class LevelManager
    {


        public Level1 Level1;
        public Level2 Level2;
        public Level3 Level3;

        public static int CurrentLevel;

        public MainMenu MainMenu;

        public GameOver GameOver;
        //public JoystickComponent gamePad = new JoystickComponent();
        public static bool ControlQuitRequest = false;


        public enum GameState
        {
            Quit,
            MainMenu,
            Level1,
            Level2,
            Level3,
            GameOver
        }

        public static GameState display;


        public void RunLevel1()
        {
            Level1 = new Level1();
            CurrentLevel = 1;
            Level1.Run();
        }
        public void RunLevel2()
        {
            Level2 = new Level2();
            CurrentLevel = 2;
            Level2.Run();
        }
        public void RunLevel3()
        {
            Level3 = new Level3();
            CurrentLevel = 3;
            Level3.Run();
        }

        public void RunMainMenu()
        {
            //gamePad.setup();
            MainMenu = new MainMenu();
            MainMenu.Run();
        }

        public void RunGameOver()
        {
            GameOver = new GameOver();
            Console.WriteLine("GameOver");
            GameOver.Run();
        }

        public void Run()
        {
            display = GameState.MainMenu;
            while (true)
            {
                switch (display)
                {
                    case GameState.Quit: Program.Game.QuitGame(); return;
                    case GameState.MainMenu: RunMainMenu(); break;
                    case GameState.Level1: RunLevel1(); break;
                    case GameState.Level2: RunLevel2(); break;
                    case GameState.Level3: RunLevel3(); break;
                    case GameState.GameOver: RunGameOver(); break;
                }
            }
        }
    }
}
