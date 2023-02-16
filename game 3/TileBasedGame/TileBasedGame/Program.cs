
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
        }
    }
}
