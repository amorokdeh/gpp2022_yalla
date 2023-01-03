using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class LevelManager
    {


        public Level1 level1;
        public Level2 level2;
        public Level3 level3;

        public static int CurrentLevel;




        public MainMenu mainMenu;

        public GameOver gameOver;
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


        public void runLevel1()
        {
            level1 = new Level1();
            CurrentLevel = 1;
            level1.run();
        }
        public void runLevel2()
        {
            level2 = new Level2();
            CurrentLevel = 2;
            level2.run();
        }
        public void runLevel3()
        {
            level3 = new Level3();
            CurrentLevel = 3;
            level3.run();
        }



        public void runMainMenu()
        {
            //gamePad.setup();
            mainMenu = new MainMenu();
            mainMenu.run();
        }

        public void runGameOver()
        {
            gameOver = new GameOver();
            Console.WriteLine("GameOver");
            gameOver.run();
        }


        public void run()
        {
            display = GameState.MainMenu;
            while (true)
            {
                switch (display)
                {
                    case GameState.Quit: Program.game.quit(); return;
                    case GameState.MainMenu: runMainMenu(); break;
                    case GameState.Level1: runLevel1(); break;
                    case GameState.Level2: runLevel2(); break;
                    case GameState.Level3: runLevel3(); break;
                    case GameState.GameOver: runGameOver(); break;
                }
            }
        }
    }
}
