using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AudioManager
    {
        AudioComponent audioC = new AudioComponent();
        public AudioManager() {
            audioC.setup();
        }
        public void runSound(string sound)
        {
            audioC.runSound(search(sound));
        }
        public void runMusic(string music)
        {
            audioC.runMusic(search(music));
        }
        public void stopMusic()
        {
            SDL_mixer.Mix_HaltMusic();
        }
        public void changeVolumeMusic(int volume) {
                if  (volume > 250)   { volume = 0; }
            else if (volume < 0  )   { volume = 250; }

            audioC.changeVolumeMusic(volume);
        }
        public int getVolumeMusic()
        {
            return audioC.volumeMusic;
        }
        public void changeVolumeSound(int volume)
        {
                if (volume > 250)   { volume = 0; } 
            else if(volume < 0  )   { volume = 250; }

            audioC.changeVolumeSound(volume);
        }
        public int getVolumeSound() {
            return audioC.volumeSound;
        }
        public void cleanUp()
        {
            audioC.cleanUp();
        }
        public IntPtr search(string sound) {
            switch (sound) {
                case "Menu buttons": return audioC._MenuButtons;
                case "Menu click": return audioC._MenuClick;
                case "Menu music": return audioC._MenuMusic;
                case "Level1 music": return audioC._Level1Music;
                case "Level2 music": return audioC._Level2Music;
                case "Level3 music": return audioC._Level3Music;
                case "Game Over": return audioC._GameOver;
                case "Shooting": return audioC._Shooting;

            }
            return audioC._sound;
        }

    }
}
