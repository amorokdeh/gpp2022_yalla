using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class AudioComponent : Component
    {
        public IntPtr music = IntPtr.Zero;
        public IntPtr sound = IntPtr.Zero;

        public int volumeMusic = 50;
        public int volumeSound = 50;

        public AudioComponent() {}

        public IntPtr LoadMusic(String source)
        {
            music = SDL_mixer.Mix_LoadMUS(source);
            if (music == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return music;
        }

        public IntPtr LoadSound(String source)
        {

            sound = SDL_mixer.Mix_LoadWAV(source);
            if (sound == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return sound;
        }

        public void RunSound()
        {
            SDL_mixer.Mix_VolumeChunk(sound, volumeSound);
            SDL_mixer.Mix_PlayChannel(-1, sound, 0);
        }

        public void RunMusic()
        {
            SDL_mixer.Mix_VolumeMusic(volumeMusic);
            SDL_mixer.Mix_PlayMusic(music, -1);
        }
        public void ChangeVolumeMusic(int volume)
        {
            volumeMusic = volume;
            SDL_mixer.Mix_VolumeMusic(volumeMusic);
        }
        public void ChangeVolumeSound(int volume)
        {
            volumeSound = volume;
        }
        public void stopMusic()
        {
            //Free the music
            SDL_mixer.Mix_HaltMusic();
        }

        public void clean()
        {
            SDL_mixer.Mix_FreeChunk(sound);
            SDL_mixer.Mix_FreeMusic(music);
            sound = IntPtr.Zero;
            music = IntPtr.Zero;
            SDL_mixer.Mix_CloseAudio();
        }
    }
}
