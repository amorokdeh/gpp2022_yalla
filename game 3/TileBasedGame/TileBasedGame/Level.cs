using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level
    {
        
        private DateTime _timeBefore = DateTime.Now;
        private DateTime _timeNow = DateTime.Now;
        private float _deltaTime;
        private float _avDeltaTime = -1;
        public Player _player;

        public virtual void Run()
        {
            Program.Game.Cleaner.clean();
            _avDeltaTime = -1;

            buildMap();

            this._player = Program.Game.Player;
            _player.PosY -= 20;
            LevelManager.ControlQuitRequest = false;


            while (true)
            {
                Program.Window.CalculateFPS(); //frame limit start calculating here
                calculateDeltaTime();
                Program.Game.Shoot(_avDeltaTime);
                Program.Game.ControlEnemy();
                Program.Game.ControlPlayer();
                Program.Game.Move(_avDeltaTime);
                Program.Game.Collide(_avDeltaTime);
                Program.Game.DoUpdate();
                if (_player.Lives <= 0)
                {
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.Game.Audio.StopMusic();
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GameOver));
                    Program.Game.SetInactive();
                    return;
                }

                Program.Game.UpdateCamera(_player, _avDeltaTime);
                Program.Game.Render();

                if (Game.Quit)
                {
                    LevelManager.display = LevelManager.GameState.Quit;
                    LevelManager.ControlQuitRequest = true;
                    Program.Game.SetInactive();
                    Program.Game.Audio.StopMusic();

                }
                if (LevelManager.ControlQuitRequest)
                {
                    Program.Game.SetInactive();
                    Program.Game.Audio.StopMusic();
                    return;
                }

                Program.Window.DeltaFPS(); //frame limit end calculating here
            }
        }
        private void calculateDeltaTime() {

            _timeNow = DateTime.Now;
            _deltaTime = (_timeNow.Ticks - _timeBefore.Ticks) / 10000000f;
            if (_avDeltaTime == -1)
            {
                _avDeltaTime = _deltaTime;
            }
            else
            {
                _avDeltaTime = (_deltaTime + _avDeltaTime) / 2f;
            }
            _timeBefore = _timeNow;

            if (_avDeltaTime > 0.02)
                _avDeltaTime = 0.02f;
        }

        public virtual void buildMap()
        {

        }
    }
}
