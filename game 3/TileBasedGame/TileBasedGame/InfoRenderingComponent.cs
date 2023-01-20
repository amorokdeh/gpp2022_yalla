using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (this.GameObject is Player)
            {
                Player player = (Player)this.GameObject;
                _lives = Convert.ToString(player.Lives);
                _score = Convert.ToString(player.Score);
            }
            _text = "Lives: " + _lives + " Score: " + _score;
            _surfaceMessage = SDL_ttf.TTF_RenderText_Solid(_txt.Font, _text, _color);
            _txt.AddText(Program.Window.Renderer, _surfaceMessage, Program.Window.Width/2 - _textSize/2, 10, 100, _textSize);


        }
    }
}
