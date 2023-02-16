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
        public Player Player;


        public virtual void Run()
        {
            Program.Game.Cleaner.clean();
            _avDeltaTime = -1;

            BuildMap();

            this.Player = Program.Game.Player;
            Player.PosY -= 20;
            LevelManager.ControlQuitRequest = false;


            while (true)
            {
                Program.Window.CalculateFPS(); //frame limit start calculating here
                CalculateDeltaTime();
                Program.Game.Shoot(_avDeltaTime);
                Program.Game.ControlEnemy();
                Program.Game.ControlPlayer();
                Program.Game.Move(_avDeltaTime);
                Program.Game.Collide(_avDeltaTime);
                Program.Game.DoUpdate();
                if (Player.Lives <= 0)
                {
                    Die();
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.Game.Audio.StopMusic();
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GameOver));
                    Program.Game.SetInactive();
                    return;
                }

                Program.Game.UpdateCamera(Player, _avDeltaTime);
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
        private void CalculateDeltaTime() {

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


            // set _avDeltaTime down if _avDeltaTime becomes so large that it would become jerky
            if (_avDeltaTime > 0.05)
                _avDeltaTime = 0.05f;
        }

        public void Die()
        {
            Program.Game.SetUpRedBlend();
            float height = (float)Player.Height;
            while ( Player.Height > 0) 
            {
                CalculateDeltaTime();                
                Program.Game.Render();
                Program.Game.RedBlend(_avDeltaTime);
                height -= _avDeltaTime*60f;
                Player.PosY += _avDeltaTime * 60f;
                Player.Height = (int)height;
            }
            Program.Game.SetOpacity(0);
        }

        public virtual void BuildMap() {}
    }
}
