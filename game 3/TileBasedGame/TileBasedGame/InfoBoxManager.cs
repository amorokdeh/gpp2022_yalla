using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TileBasedGame.LevelManager;

namespace TileBasedGame
{
    class InfoBoxManager
    {
        public InfoBox infoBox = new InfoBox();
        public bool powerUpInfoRunning = true;
        public bool keysInfoRunning = true;
        public bool levelInfoRunning = true;
        public InfoBoxManager() { 
        }

        public void powerUpInfo() {

            while (powerUpInfoRunning)
            {
                infoBox.renderBox();
                infoBox.printTxt("Power up", (Program.Window.Height * 30 / 100) + Program.Window.Height * 2 / 100, 30);
                infoBox.printTxt("You can now shoot faster", (Program.Window.Height * 30 / 100) + Program.Window.Height * 10 / 100, 30);
                infoBox.printAnimText("Press Enter");
                powerUpInfoRunning = infoBox.Control();
            }
        }

        public void keysInfo()
        {

            while (keysInfoRunning)
            {
                infoBox.renderBox();
                infoBox.printTxt("Game Info", (Program.Window.Height * 30 / 100) + Program.Window.Height * 2 / 100, 30);
                infoBox.printTxt("Left, Right to move", (Program.Window.Height * 30 / 100) + Program.Window.Height * 7 / 100, 25);
                infoBox.printTxt("Up to jump", (Program.Window.Height * 30 / 100) + Program.Window.Height * 12 / 100, 25);
                infoBox.printTxt("Space to shoot", (Program.Window.Height * 30 / 100) + Program.Window.Height * 17 / 100, 25);
                infoBox.printAnimText("Press Enter");
                keysInfoRunning = infoBox.Control();
            }
        }

        public void LevelWinInfo()
        {

            while (levelInfoRunning)
            {
                infoBox.renderBox();
                infoBox.printTxt("Level completed", (Program.Window.Height * 30 / 100) + Program.Window.Height * 10 / 100, 30);
                infoBox.printAnimText("Press Enter");
                levelInfoRunning = infoBox.Control();
            }

            LevelManager.display = GameState.MainMenu;
            LevelManager.ControlQuitRequest = true;
        }


    }
}
