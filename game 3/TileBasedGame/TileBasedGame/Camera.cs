using System;

namespace TileBasedGame
{
    class Camera : Observable, Observer
    {
        public float PosX;
        public float PosY;

        private int _mapWidth = Program.Game.Maps.currentMap.MapWidth;
        private int _mapHeight = Program.Game.Maps.currentMap.MapHeight;
        private int _winWidth = Program.Window.Width;
        private int _winHeight = Program.Window.Height;

        private float _diffX = Globals.Reset;
        private float _diffY = Globals.Reset;

        private Random _rand = new Random();
        private float _shake = Globals.Reset;
        private float _shakeStep = Globals.Reset;

        public Camera(GameObject player)
        {
            PosX = player.PosX;
            PosY = player.PosY;
            MessageBus.Register(this);
        }

        public void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.Hurt)
            {
                _shake = 0.5f;
            }
        }

        public void UpdateCamera(GameObject player, float deltaT)
        {
            if (_shake > 0)
            {
                _shake -= deltaT;
                _shakeStep += deltaT;
                if(_shakeStep > 0.015)
                {
                    _diffY = _rand.Next(-10, 10);
                    _diffX = _rand.Next(-10, 10);
                    _shakeStep = Globals.Reset;
                }   
            } else
            {
                _diffX = Globals.Reset;
                _diffY = Globals.Reset;
            }

            int view = Program.Window.Width * 10 / 100;
            int camSpeed = 25;
            if (player.direction == "right" && PosX <= player.PosX + view)
            {
                PosX += camSpeed;
                if (PosX > player.PosX + view)
                    PosX = player.PosX + view;
            }
            else if (player.direction == "left" && PosX >= player.PosX - view)
            {
                PosX -= camSpeed;
                if (PosX < player.PosX - view)
                    PosX = player.PosX - view;
            }

            PosX += _diffX;
            PosY = player.PosY + _diffY;

            //Fix position
            if (PosX - (_winWidth / 2) < 0) { PosX = (_winWidth / 2); }
            if (PosX + (_winWidth / 2) > _mapWidth) { PosX = _mapWidth - (_winWidth / 2); }

            if (PosY - (_winHeight / 2) < 0) { PosY = (_winHeight / 2); }
            if (PosY + (_winHeight / 2) > _mapHeight) { PosY = _mapHeight - (_winHeight / 2); }
        }
    }
}
