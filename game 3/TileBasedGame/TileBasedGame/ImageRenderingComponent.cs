using System;
using SDL2;

namespace TileBasedGame
{
    class ImageRenderingComponent : RenderingComponent
    {
        private SDL.SDL_Rect _rect;
        private SDL.SDL_Rect _srcRect;
        private int _dstX = 0;

        public Image Img = new Image();
        public SDL.SDL_RendererFlip flipped = SDL.SDL_RendererFlip.SDL_FLIP_NONE; // to flip the image
        public int rotateAngle = 0;

        public int ImgChange = Globals.Reset;
        public int ImgChangeY = Globals.Reset;
        public int ImgStep = Globals.Reset;

        public ImageRenderingComponent(RenderingManager rm, Image image, int x, int y, int w, int h, int dstW, int dstH, int dstX = 0) : base()
        {
            MessageBus.Register(this);
            this.RenderingManager = rm;
            Img = image;
            _srcRect.x = x;
            _srcRect.y = y;
            _srcRect.w = w;
            _srcRect.h = h;
            _rect.w = dstW;
            _rect.h = dstH;
            this._dstX = dstX;
            ImgChange = x;
            ImgChangeY = y;
        }

        public ImageRenderingComponent(RenderingManager rm, int imgFrame,  Image image, int x, int y, int w, int h, int dstW, int dstH, int dstX = 0) : base()
        {
            //set image
            int line;
            int col;

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

            HeroEvent he = e as HeroEvent;

            if(he != null)
            {
                if (he.GameObject == this.GameObject)
                {
                    if (he.EventType == HeroEvent.Type.EnemyDead)
                    {
                        rotateAngle = 90;
                        GameObject.PosY += 32;
                    }
                }
            }
            
            AnimationEvent ae = e as AnimationEvent;
            if (ae == null)
                return;
            if (ae.GameObject == this.GameObject)
            {

                if (ae.EventType == AnimationEvent.Type.Animation)
                {
                    ImgChange = ae.Frame;
                    if (ae.Flipped)
                    {
                        flipped = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                    } else
                    {
                        flipped = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                    }
                }
            }
        }

        public override void Render(){

            int winHeight = Program.Window.Height;
            int winWidth = Program.Window.Width;

            float cameraX = Program.Game.Camera.PosX;
            float cameraY = Program.Game.Camera.PosY;

            float camLeftBorder = cameraX - winWidth / 2;
            float camRightBorder = cameraX + winWidth / 2;
            float camTopBorder = cameraY - winHeight / 2;
            float camBottomBorder = cameraY + winHeight / 2;

            bool objectOnCamera = (GameObject.PosX < camRightBorder) && (GameObject.PosX + GameObject.Width > camLeftBorder) && (GameObject.PosY < camBottomBorder) && (GameObject.PosY + GameObject.Height > camTopBorder);
            
            //render only the objects on camera
            if (objectOnCamera)
            {
                _rect.x = (int)(GameObject.PosX + _dstX - Program.Game.Camera.PosX + Program.Window.Width / 2);
                _rect.y = (int)(GameObject.PosY - Program.Game.Camera.PosY + Program.Window.Height / 2);
                _rect.h = GameObject.Height;


                _srcRect.x = ImgChange;
                _srcRect.y = ImgChangeY;

                //SDL.SDL_RenderCopy(Program.Window.Renderer, Img.ImageTexture, ref _srcRect, ref _rect);
                SDL.SDL_RenderCopyEx(Program.Window.Renderer, Img.ImageTexture, ref _srcRect, ref _rect, rotateAngle, IntPtr.Zero, flipped);

                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
            }
        }
    }
}
