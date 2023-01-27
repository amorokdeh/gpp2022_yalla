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

        // sound effects
        public IntPtr MenuButtons = IntPtr.Zero;
        public IntPtr MenuClick = IntPtr.Zero;
        public IntPtr GameOver = IntPtr.Zero;
        public IntPtr Shooting = IntPtr.Zero;
        public IntPtr ExplodEnemy = IntPtr.Zero;
        public IntPtr ExplodPlayer = IntPtr.Zero;
        public IntPtr ShootingEnemy = IntPtr.Zero;

        // music effects
        public IntPtr MenuMusic = IntPtr.Zero;
        public IntPtr Level1Music = IntPtr.Zero;
        public IntPtr Level2Music = IntPtr.Zero;
        public IntPtr Level3Music = IntPtr.Zero;

        public int volumeMusic = 50;
        public int volumeSound = 30;

        public AudioComponent()
        {

        }

        public void Setup()
        {
            // Initialize SDL_mixer
            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0)
            {
                Console.WriteLine("SDL_mixer could not initialize! SDL_mixer Error: {0}", SDL.SDL_GetError());
            }
            //Load music
            MenuMusic = LoadMusic("sound/MainMenu Music.wav");
            Level1Music = LoadMusic("sound/Level1 Music.wav");
            Level2Music = LoadMusic("sound/Level2 Music.wav");
            Level3Music = LoadMusic("sound/Level3 Music.wav");

            //Load sound effects
            MenuButtons = LoadSound("sound/Menu buttons.wav");
            MenuClick = LoadSound("sound/Menu click.wav");
            GameOver = LoadSound("sound/game_over.wav");
            Shooting = LoadSound("sound/shooting.wav");
            ExplodEnemy = LoadSound("sound/explod_2.wav");
            ExplodPlayer = LoadSound("sound/explod_1.wav");
            ShootingEnemy = LoadSound("sound/lasergun.wav");

        }

        

        public IntPtr LoadMusic(String source)
        {

            IntPtr music = SDL_mixer.Mix_LoadMUS(source);
            if (music == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return music;
        }

        private IntPtr LoadSound(String source)
        {

            IntPtr sound = SDL_mixer.Mix_LoadWAV(source);
            if (sound == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return sound;
        }

        public void RunSound(IntPtr soundSource)
        {
            SDL_mixer.Mix_VolumeChunk(soundSource, volumeSound);
            SDL_mixer.Mix_PlayChannel(-1, soundSource, 0);
        }

        public void RunMusic(IntPtr soundSource)
        {
            SDL_mixer.Mix_VolumeMusic(volumeMusic);
            SDL_mixer.Mix_PlayMusic(soundSource, -1);
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

        /*
        public static void runSound(string path, uint time)
        {
            SDL.SDL_Init(SDL.SDL_INIT_AUDIO);
            SDL.SDL_AudioSpec spec = new SDL.SDL_AudioSpec();

            //string path = Path.Combine(Directory.GetCurrentDirectory() + "\\sound.wav");

            SDL.SDL_LoadWAV(path, ref spec, out IntPtr audioBuffer, out uint audioLength);
            uint deviceId = SDL.SDL_OpenAudioDevice(null, 0, ref spec, out SDL.SDL_AudioSpec obtained, 0);
            SDL.SDL_QueueAudio(deviceId, audioBuffer, audioLength);
            SDL.SDL_PauseAudioDevice(deviceId, 0);
            SDL.SDL_Delay(time);
            SDL.SDL_CloseAudioDevice(deviceId);
            SDL.SDL_FreeWAV(audioBuffer);
            SDL.SDL_Quit();
        }
        */

        public void stopMusic(IntPtr music)
        {

            //Free the music
            SDL_mixer.Mix_HaltMusic();
        }
        public void cleanUp()
        {
            //Free the sound effects
            SDL_mixer.Mix_FreeChunk(MenuClick);
            SDL_mixer.Mix_FreeChunk(MenuButtons);
            SDL_mixer.Mix_FreeChunk(GameOver);
            SDL_mixer.Mix_FreeChunk(Shooting);
            SDL_mixer.Mix_FreeChunk(ExplodEnemy);
            SDL_mixer.Mix_FreeChunk(ExplodPlayer);
            MenuClick = IntPtr.Zero;
            MenuButtons = IntPtr.Zero;
            GameOver = IntPtr.Zero;
            Shooting = IntPtr.Zero;
            ExplodEnemy = IntPtr.Zero;
            ExplodPlayer = IntPtr.Zero;

            //Free the music
            SDL_mixer.Mix_FreeMusic(MenuMusic);
            SDL_mixer.Mix_FreeMusic(Level1Music);
            SDL_mixer.Mix_FreeMusic(Level2Music);
            SDL_mixer.Mix_FreeMusic(Level3Music);
            MenuMusic = IntPtr.Zero;
            Level1Music = IntPtr.Zero;
            Level2Music = IntPtr.Zero;
            Level3Music = IntPtr.Zero;

            SDL_mixer.Mix_CloseAudio();
        }

    }
}
