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
        public float PosX { get; set; }
        public float PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float VelX { get; set; }
        public float CurrentVelX { get; set; }
        public float VelY { get; set; }
        public float CurrentVelY { get; set; }


        public Image Img = new Image();



        public GameObject(string name)
        {
            //for testing
            PosX = 200;
            PosY = 200;
            Width = 200;
            Height = 200;
            VelX = 50;
            VelY = 50;
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
