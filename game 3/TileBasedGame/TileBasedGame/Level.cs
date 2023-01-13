using System;
using System.Collections.Generic;
using System.Linq;
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
        private Player _player;

        public float Gap = 0;
        public float GapSize = 1f;
        public GameObject GameObject;

        public Random Rand;

        public virtual void Run()
        {
            this._player = Program.Game.Player;
            _player.Reset();

            LevelManager.ControlQuitRequest = false;
            Rand = new Random();



            while (true)
            {
                Program.Window.CalculateFPS(); //frame limit start calculating here
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
                //Console.WriteLine(deltaTime);
                //Console.WriteLine(avDeltaTime);

                ProduceEnemies(_avDeltaTime);
                //produceBullets(avDeltaTime);
                // produceBulletEnemy(avDeltaTime);

                Program.Game.Shoot(_avDeltaTime);
                Program.Game.ControlEnemy();
                Program.Game.ControlPlayer();
                Program.Game.Move(_avDeltaTime);
                Program.Game.Animate(_avDeltaTime);
                Program.Game.Collide();
                if (_player.Lives <= 0)
                {
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.Game._audio.StopMusic();
                    Program.Game._audio.RunSound("Game Over");
                    Program.Game.SetInactive();
                    return;
                }
                Program.Game.Render();

                if (Game.Quit)
                {
                    LevelManager.display = LevelManager.GameState.Quit;
                    LevelManager.ControlQuitRequest = true;
                    Program.Game.SetInactive();
                    Program.Game._audio.StopMusic();

                }
                if (LevelManager.ControlQuitRequest)
                {
                    Program.Game.SetInactive();
                    Program.Game._audio.StopMusic();
                    return;
                }  // press escape to quit
                Program.Window.DeltaFPS(); //frame limit end calculating here

            }
        }


        public virtual void ProduceEnemies(float deltaTime)
        {
            
            Gap += deltaTime;
            if (Gap > GapSize)
            {
                GameObject = Program.Game.RequestEnemyShip();

                GameObject.PosY = 0;
                GameObject.PosX = Rand.Next(0, Program.Window.Width); // Enemy Random Position 
                GameObject = Program.Game.RequestEnemyUfo();

                GameObject.PosY = 0;
                GameObject.PosX = Rand.Next(0, Program.Window.Width); // Enemy Random Position 

                Gap = 0;
            }
        }
    }
}
