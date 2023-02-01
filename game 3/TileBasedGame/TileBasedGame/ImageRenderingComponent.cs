using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class ImageRenderingComponent : RenderingComponent
    {
        private SDL.SDL_Rect _rect;
        private SDL.SDL_Rect _srcRect;
        private int _dstX = 0;

        public Image Img = new Image();

        public int ImgChange = Globals.Reset;
        public int ImgChangeY = Globals.Reset;
        public int ImgStep = Globals.Reset;

        public ImageRenderingComponent(RenderingManager rm, Image image, int x, int y, int w, int h, int dstW, int dstH, int dstX = 0) : base()
        {
            this.RenderingManager = rm;
            Img = image;
            _srcRect.x = x;
            _srcRect.y = y;
            _srcRect.w = w;
            _srcRect.h = h;
            _rect.w = dstW;
            _rect.h = dstH;
            this._dstX = dstX;
        }

        public ImageRenderingComponent(RenderingManager rm, int imgFrame,  Image image, int x, int y, int w, int h, int dstW, int dstH, int dstX = 0) : base()
        {
            //set image
            int line = 0;
            int col = 0;

            int i = 0;
            while (i < (imgFrame - 10))
            {
                i += 10;
            }
            line = (i / 10);
            col = (imgFrame - i) - 1;

            ImgChange = 32 * col;
            ImgChangeY = 32 * line;


            this.RenderingManager = rm;
            Img = image;
            _srcRect.x = x;
            _srcRect.y = y;
            _srcRect.w = w;
            _srcRect.h = h;
            _rect.w = dstW;
            _rect.h = dstH;
            this._dstX = dstX;
        }

        public override void OnEvent(Event e)
        {
            //Console.WriteLine("Change Image");
            HeroEvent he = e as HeroEvent;
            if (he.GameObject == GameObject)
            {
                if (he == null)
                    return;
                if (he.EventType == HeroEvent.Type.FlyLeft)
                {
                    ImgChange = Globals.MediumImageSize * 3;
                    Img.flipped = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                }
                else if (he.EventType == HeroEvent.Type.FlyRight)
                {
                    ImgChange = Globals.MediumImageSize * 3;
                    Img.flipped = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                }
                else if (he.EventType == HeroEvent.Type.FlyStraight) { }

                else if (he.EventType == HeroEvent.Type.FlyUp)
                    ImgChange = Globals.MediumImageSize * 2;
                else if (he.EventType == HeroEvent.Type.ChangeImage)
                {
                    ImgStep++;
                    if (ImgStep < 3)
                    {
                        ImgChange = Globals.MediumImageSize * ImgStep;
                    }
                    else
                    {
                        ImgChange = Globals.Reset;
                    }

                }
                
            }
        }

        override public void Render()
        {
            _rect.x = (int)(GameObject.PosX + _dstX - Program.Game.Camera.PosX + Program.Window.Width / 2);
            _rect.y = (int)(GameObject.PosY - Program.Game.Camera.PosY + Program.Window.Height / 2);

            //_srcRect.x = GameObject.ImgChange;
            //_srcRect.y = GameObject.ImgChangeY;

            _srcRect.x = ImgChange;
            _srcRect.y = ImgChangeY;

            //SDL.SDL_RenderCopy(Program.Window.Renderer, Img.ImageTexture, ref _srcRect, ref _rect);
            Img.render(ref _srcRect, ref _rect);
            
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
        }
    }
}
