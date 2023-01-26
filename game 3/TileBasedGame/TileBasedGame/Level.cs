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

        public float Gap = Globals.Reset;
        public float GapSize = Globals.EnemyGap;
        public GameObject GameObject;

        public Random Rand;

        public static int rounds = 0;

        public virtual void Run()
        {

            Program.Game._cleaner.clean();

            //_player.Reset();
            buildMap();
            
            this._player = Program.Game.Player;
            LevelManager.ControlQuitRequest = false;
            Rand = new Random();



            while (true)
            {
                rounds = 0;
                if (this._player.CurrentVelY < 100) {
                    this._player.CurrentVelY += Globals.Gravity;
                }
                

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
                Program.Game.DoUpdate();
                if (_player.Lives <= 0)
                {
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.Game._audio.StopMusic();
                    Program.Game._audio.RunSound("Game Over");
                    Program.Game.SetInactive();
                    return;
                }

                
                Program.Game.UpdateCamera(_player);
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

                if(rounds >0)
                    Console.WriteLine(rounds);
            }
        }


        public virtual void ProduceEnemies(float deltaTime)
        {
            
            Gap += deltaTime;
            if (Gap > GapSize)
            {
                GameObject = Program.Game.RequestEnemyShip();

                GameObject.PosY = Globals.Reset;
                GameObject.PosX = Rand.Next(0, Program.Window.Width); // Enemy Random Position 
                GameObject = Program.Game.RequestEnemyUfo();

                GameObject.PosY = Globals.Reset;
                GameObject.PosX = Rand.Next(0, Program.Window.Width); // Enemy Random Position 

                Gap = Globals.Reset;
            }
        }

        public virtual void buildMap() { 
        
        }
    }
}
