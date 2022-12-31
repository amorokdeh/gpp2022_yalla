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

        private string Name { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float VelX { get; set; }
        public float CurrentVelX { get; set; }
        public float VelY { get; set; }
        public float CurrentVelY { get; set; }


        public Image Img = new Image();

        public int ImgChange = 0;
        public int ImgStep = 0;


        public virtual void ChangeImage() { }


        private int pause = 5;
        public void ChangeImage(int imgAmount)
        {
            
            
            if(pause > 0)
            {
                pause--;
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
                    pause = 5;
                }
            }

        }



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


            Img.setUp();

            


            this.Name = name;

        }

        internal void AddComponent(Component component)
        {
            component.GameObject = this;
        }

    }
}
