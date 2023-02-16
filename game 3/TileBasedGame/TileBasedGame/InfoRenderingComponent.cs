using System;
using SDL2;

namespace TileBasedGame
{
    class InfoRenderingComponent : RenderingComponent
    {
        private Text _txt = new Text();
        private string _text;
        private string _lives;
        private string _score;
        private IntPtr _surfaceMessage;
        private SDL.SDL_Color _color;
        private int _textSize = Globals.SmallTextSize;
        public InfoRenderingComponent(RenderingManager rm)
        {
            this.RenderingManager = rm;
            _txt.SetUp();
            _txt.LoadText(1);
            _color = _txt.White;
        }

        override public void Render()
        {
            byte liveColor = 0;
            if (this.GameObject is Player)
            {
                Player player = (Player)this.GameObject;
                _lives = Convert.ToString(player.Lives);
                _score = Convert.ToString(player.Score);
                liveColor = (byte) ((Globals.Lives - player.Lives) * 25);
            }
            
            _text = " Score: " + _score;
            _surfaceMessage = SDL_ttf.TTF_RenderText_Solid(_txt.Font, _text, _color);
            _txt.AddText(Program.Window.Renderer, _surfaceMessage, Program.Window.Width * 90 / 100 - _textSize/2, 10, 100, _textSize);

            SDL.SDL_Rect borderRect = new SDL.SDL_Rect
            {
                x = (Program.Window.Width * 30 / 100) - 5,
                y = (Program.Window.Height * 2 / 100) -5,
                w = (Program.Window.Width * 4 / 100) * (Globals.Lives) + 10,
                h = (Program.Window.Height * 4 / 100) + 10,
            };
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref borderRect);

            SDL.SDL_Rect backgroundRect = new SDL.SDL_Rect
            {
                x = Program.Window.Width * 30 / 100,
                y = Program.Window.Height * 2 / 100,
                w = (Program.Window.Width * 4 / 100) * Globals.Lives,
                h = Program.Window.Height * 4 / 100,
            };
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 0, 0, 0);
            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref backgroundRect);

            SDL.SDL_Rect liveRect = new SDL.SDL_Rect
            {
                x = Program.Window.Width * 30 / 100,
                y = Program.Window.Height * 2 / 100,
                w = (Program.Window.Width * 4 / 100) * Int32.Parse(_lives),
                h = Program.Window.Height * 4 / 100,
            };
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, liveColor, (byte) (255 - liveColor), 0, 255);

            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref liveRect);
        }
    }
}
