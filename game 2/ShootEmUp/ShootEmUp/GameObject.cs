using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class GameObject
    {
        private string _name { get; set; }
        private int _posX { get; set; }
        private int _posY { get; set; }
        private int _width { get; set; }
        private int _height { get; set; }



        public GameObject(string name)
        {
            this._name = name;

        }

        internal void AddComponent(Component component)
        {
            component.GameObject = this;
        }

    }
}
