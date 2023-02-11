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

        public string Name;

        public float PosX;
        public float PosY;
        public int Width;
        public int Height;
        public int GeneralHeight;
        public float VelX;
        public float CurrentVelX;
        public float VelY;
        public float CurrentVelY;
        public string direction;
        public int JumpPossibility;
        public bool Shoot;
        public bool CanShoot;
        public float ShootingSpeed;

        public bool Hurt;
        public int HurtAmount;

        public State State;


        //! an andere Stelle im Programm
        public CharacterData CharData;
        public int ImgChange;





        public GameObject(string name, int w, int h)
        {
            //! an andere Stelle im Programm
            CharData = Program.Game.Loader.PlayerAnimation;
            State = new Running();
            State.SetDirection("stand");
            State.Enter(this);


            Width = w;
            Height = h;
            GeneralHeight = h;
            VelX = Globals.Velocity;
            VelY = Globals.Velocity;
            CurrentVelX = Globals.Reset;
            CurrentVelY = Globals.Reset;
            direction = "right";
            JumpPossibility = 2;
            Shoot = false;
            CanShoot = true;
            ShootingSpeed = 0;
            Hurt = false;
            HurtAmount = 0;

            this.Name = name;
            Program.Game.Maps.currentMap.Tiles.Add(this);
        }

        internal void AddComponent(Component component)
        {
            Components.Add(component);
            component.GameObject = this;
        }



    }
}
