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

        public string _name;

        public float PosX;
        public float PosY;
        public int Width;
        public int Height;
        public float VelX;
        public float CurrentVelX;
        public float VelY;
        public float CurrentVelY;
        public string direction;
        public int jumpPossibility;


        public GameObject(string name, int w, int h)
        {
            Width = w;
            Height = h;
            VelX = Globals.Velocity;
            VelY = Globals.Velocity;
            CurrentVelX = Globals.Reset;
            CurrentVelY = Globals.Reset;
            direction = "right";
            jumpPossibility = 2;

            this._name = name;
        }

        internal void AddComponent(Component component)
        {
            Components.Add(component);
            component.GameObject = this;
        }

    }
}
