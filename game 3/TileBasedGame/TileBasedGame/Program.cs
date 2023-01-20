using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Program
    {
        public static Window Window = new Window(768, 1024); //window size
        public static Game Game = new Game(); //create the game
        static void Main(string[] args)
        {

            Window.Setup(); //setup the window
            Game.Run(); //run the game loop


            //Test
            /*
            TiledMap tiledMap = new TiledMap();
            tiledMap.load("image/MiniPixelPack3/Maps/Level1.json", "PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            tiledMap.build();
            while (true)
            {
            }
            */


            //Observable und Observer aus der Vorlesung
            //game.tryHeroStuff();
        }
    }
}
