using System;

namespace TileBasedGame
{
    class LevelManager
    {
        public Level CurrentLevel;
        public MainMenu MainMenu;
        public GameOver GameOver;
        public static bool ControlQuitRequest = false;
        public static GameState display;

        public enum GameState
        {
            Quit,
            MainMenu,
            Level1,
            Level2,
            Level3,
            GameOver
        }

        public void RunLevel1()
        {
            CurrentLevel = new Level1();
            CurrentLevel.Run();
        }
        public void RunLevel2()
        {
            CurrentLevel = new Level2();
            CurrentLevel.Run();
        }
        public void RunLevel3()
        {
            CurrentLevel = new Level3();
            CurrentLevel.Run();
        }

        public void RunMainMenu()
        {
            MainMenu = new MainMenu();
            MainMenu.Run();
        }

        public void RunGameOver()
        {
            GameOver = new GameOver();
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
