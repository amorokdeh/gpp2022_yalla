using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class AudioManager
    {
        private AudioComponent _audioC = new AudioComponent();
        private int _volumMin = 0;
        private int _volumMax = 250;
        public AudioManager()
        {
            _audioC.Setup();
        }
        public void RunSound(string sound)
        {
            _audioC.RunSound(Search(sound));
        }
        public void RunMusic(string music)
        {
            _audioC.RunMusic(Search(music));
        }
        public void StopMusic()
        {
            SDL_mixer.Mix_HaltMusic();
        }
        public void ChangeVolumeMusic(int volume)
        {
            if (volume > _volumMax) { volume = _volumMin; }
            else if (volume < _volumMin) { volume = _volumMax; }

            _audioC.ChangeVolumeMusic(volume);
        }
        public int GetVolumeMusic()
        {
            return _audioC.volumeMusic;
        }
        public void ChangeVolumeSound(int volume)
        {
            if (volume > _volumMax) { volume = _volumMin; }
            else if (volume < _volumMin) { volume = _volumMax; }

            _audioC.ChangeVolumeSound(volume);
        }
        public int GetVolumeSound()
        {
            return _audioC.volumeSound;
        }
        public void CleanUp()
        {
            _audioC.cleanUp();
        }
        public IntPtr Search(string sound)
        {
            switch (sound)
            {
                case "Menu buttons": return _audioC.MenuButtons;
                case "Menu click": return _audioC.MenuClick;
                case "Menu music": return _audioC.MenuMusic;
                case "Level1 music": return _audioC.Level1Music;
                case "Level2 music": return _audioC.Level2Music;
                case "Level3 music": return _audioC.Level3Music;
                case "Game Over": return _audioC.GameOver;
                case "Shooting": return _audioC.Shooting;
                case "Enemy dead": return _audioC.ExplodEnemy;
                case "Player dead": return _audioC.ExplodPlayer;

            }
            return IntPtr.Zero;
        }

    }
}
