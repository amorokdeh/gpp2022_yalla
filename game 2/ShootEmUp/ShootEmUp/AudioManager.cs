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
        public void stopSound(string sound)
        {
            audioC.stopSound(search(sound));
        }
        public void stopMusic(string music)
        {
            audioC.stopMusic(search(music));
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
            }
            return audioC._sound;
        }

    }
}
