using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class GameObject
    {
        private string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int VelX { get; set; }
        public int CurrentVelX { get; set; }
        public int VelY { get; set; }
        public int CurrentVelY { get; set; }


        public Image Img = new Image();



        public GameObject(string name)
        {
            //for testing
            PosX = 200;
            PosY = 200;
            Width = 200;
            Height = 200;
            VelX = 2;
            VelY = 2;
            CurrentVelX = 0;
            CurrentVelY = 0;


            Img.setUp();
            Img.loadImage( "image/pumpkin.bmp");


            this.Name = name;

        }

        internal void AddComponent(Component component)
        {
            component.GameObject = this;
        }

    }
}
