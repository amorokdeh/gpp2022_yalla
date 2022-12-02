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



        
        public MainMenu mainMenu;

        public GameOver gameOver;
        public Control gamePad = new Control();





        public void runLevel1()
        {
            level1 = new Level1();
            level1.run();
        }
        public void runLevel2()
        {
            level2 = new Level2();
            level2.run();
        }
        public void runLevel3()
        {
            level3 = new Level3();
            level3.run();
        }



        public void runMainMenu()
        {
            gamePad.setup();
            mainMenu = new MainMenu();
            mainMenu.run();
        }

        public void runGameOver()
        {
            gameOver = new GameOver();
            gameOver.run();
        }
    }
}
