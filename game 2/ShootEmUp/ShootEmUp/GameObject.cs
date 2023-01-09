using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class GameObject
    {
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

        public int ImgChange = 0;
        public int ImgStep = 0;

        protected int Pause = 5;
        private int _usedPause = 0;


        public GameObject(string name, int w, int h)
        {
            //for testing
            PosX = 200;
            PosY = 200;
            Width = w;
            Height = h;
            VelX = 100;
            VelY = 100;
            CurrentVelX = 0;
            CurrentVelY = 0;

            Img.SetUp();

            this._name = name;
        }

        public virtual void ChangeImage() { }

        public void ChangeImage(int imgAmount)
        {
            
            
            if(_usedPause > 0)
            {
                _usedPause--;
            }
            else {
                ImgStep++;
                if (ImgStep < imgAmount)
                {
                    ImgChange = 16 * ImgStep;
                }
                else
                {
                    ImgChange = 16 * (imgAmount * 2 - 2 - ImgStep);
                }

                if (ImgStep > imgAmount * 2 - 3)
                {
                    ImgStep = 0;
                    _usedPause = Pause;
                }
            }

        }

        internal void AddComponent(Component component)
        {
            component.GameObject = this;
        }

    }
}
