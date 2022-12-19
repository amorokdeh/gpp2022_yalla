using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class InfoRenderingComponent : RenderingComponent
    {

        //als Test
        private Text txt = new Text();
        private string text;
        private string lives;
        private string score;
        private IntPtr surfaceMessage;
        private SDL.SDL_Color color;
        private int textSize = 20;
        public InfoRenderingComponent(RenderingManager rm)
        {
            this.RenderingManager = rm;
            txt.setUp();
            txt.loadText(1);
            color = txt.White;
        }

        override public void Render()
        {
            if (this.GameObject is Player)
            {
                Player player = (Player)this.GameObject;
                lives = Convert.ToString(player.Lives);
                score = Convert.ToString(player.Score);
            }
            text = "Lives: " + lives + " Score: " + score;
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width/2 - textSize/2, 10, 100, textSize);


        }
    }
}
