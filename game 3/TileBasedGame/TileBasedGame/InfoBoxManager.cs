using static TileBasedGame.LevelManager;

namespace TileBasedGame
{
    class InfoBoxManager
    {
        public InfoBox infoBox = new InfoBox();
        public bool powerUpInfoRunning = true;
        public bool keysInfoRunning = true;
        public bool levelInfoRunning = true;
        public InfoBoxManager() {}

        public void powerUpInfo() {

            while (powerUpInfoRunning)
            {
                infoBox.RenderBox();
                infoBox.PrintTxt("Power up", (Program.Window.Height * 30 / 100) + Program.Window.Height * 2 / 100, 30);
                infoBox.PrintTxt("You can now shoot faster", (Program.Window.Height * 30 / 100) + Program.Window.Height * 10 / 100, 30);
                infoBox.PrintAnimText("Press Enter");
                powerUpInfoRunning = infoBox.Control();
            }
        }

        public void keysInfo()
        {
            while (keysInfoRunning)
            {
                infoBox.RenderBox();
                infoBox.PrintTxt("Game Info", (Program.Window.Height * 30 / 100) + Program.Window.Height * 2 / 100, 30);
                infoBox.PrintTxt("Left, Right to move", (Program.Window.Height * 30 / 100) + Program.Window.Height * 7 / 100, 25);
                infoBox.PrintTxt("Up to jump", (Program.Window.Height * 30 / 100) + Program.Window.Height * 12 / 100, 25);
                infoBox.PrintTxt("Space to shoot", (Program.Window.Height * 30 / 100) + Program.Window.Height * 17 / 100, 25);
                infoBox.PrintAnimText("Press Enter");
                keysInfoRunning = infoBox.Control();
            }
        }

        public void LevelWinInfo()
        {

            while (levelInfoRunning)
            {
                string text = "Score: " + Program.Game.Player.Score;
                infoBox.RenderBox();
                infoBox.PrintTxt("Level completed", (Program.Window.Height * 30 / 100) + Program.Window.Height * 10 / 100, 30);
                infoBox.PrintTxt(text, (Program.Window.Height * 30 / 100) + Program.Window.Height * 17 / 100, 30);
                infoBox.PrintAnimText("Press Enter");
                levelInfoRunning = infoBox.Control();
            }

            LevelManager.display = GameState.MainMenu;
            LevelManager.ControlQuitRequest = true;
        }
    }
}
