using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class GameObject
    {
        public List<Component> Components = new List<Component>();

        public bool Active = false;
        public bool Died = false;

        private string _name;

        public float PosX;
        public float PosY;
        public int Width;
        public int Height;
        public float VelX;
        public float CurrentVelX;
        public float VelY;
        public float CurrentVelY;

        public Image Img = new Image();

        public int ImgChange = Globals.Reset;
        public int ImgStep = Globals.Reset;

        protected int Pause = Globals.AnimationPause;
        private int _usedPause = Globals.Reset;


        public GameObject(string name, int w, int h)
        {
            Width = w;
            Height = h;
            VelX = Globals.Velocity;
            VelY = Globals.Velocity;
            CurrentVelX = Globals.Reset;
            CurrentVelY = Globals.Reset;

            Img.SetUp();

            this._name = name;
        }

        public virtual void ChangeImage() { }

        public void ChangeImage(int imgAmount)
        {
            
            
            if(_usedPause > Globals.Reset)
            {
                _usedPause--;
            }
            else {
                ImgStep++;
                if (ImgStep < imgAmount)
                {
                    ImgChange = Globals.NormalImageSize * ImgStep;
                }
                else
                {
                    ImgChange = Globals.NormalImageSize * (imgAmount * Globals.Multiplier - 2 - ImgStep);
                }

                if (ImgStep > imgAmount * Globals.Multiplier - 3)
                {
                    ImgStep = Globals.Reset;
                    _usedPause = Pause;
                }
            }

        }

        internal void AddComponent(Component component)
        {
            Components.Add(component);
            component.GameObject = this;
        }

    }
}
