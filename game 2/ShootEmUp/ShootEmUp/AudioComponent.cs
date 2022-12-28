using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AudioComponent : Component
    {
        // music
        public IntPtr _Music = IntPtr.Zero;

        // sound effects
        public IntPtr _Scratch = IntPtr.Zero;
        public IntPtr _High = IntPtr.Zero;
        public IntPtr _Medium = IntPtr.Zero;
        public IntPtr _MenuButtons = IntPtr.Zero;
        public IntPtr _MenuClick = IntPtr.Zero;
        public IntPtr _MenuMusic = IntPtr.Zero;
        public IntPtr _sound = IntPtr.Zero;
        public IntPtr _GameOver = IntPtr.Zero;
        public IntPtr _Shooting = IntPtr.Zero;

        public int volumeMusic = 20;

        public AudioComponent() { 
        
        }

        public void setup()
        {
            // Initialize SDL_mixer
            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0)
            {
                Console.WriteLine("SDL_mixer could not initialize! SDL_mixer Error: {0}", SDL.SDL_GetError());
            }
            //Load music
            _Music = loadMusic("sound/beat.wav");
            _MenuMusic = loadMusic("sound/MainMenu Music.wav");
            //Load sound effects
            _Scratch = loadSound("sound/scratch.wav");
            _High = loadSound("sound/high.wav");
            _Medium = loadSound("sound/medium.wav");
            _MenuButtons = loadSound("sound/Menu buttons.wav");
            _MenuClick = loadSound("sound/Menu click.wav");
            _GameOver = loadSound("sound/game_over.wav");
            _Shooting = loadSound("sound/shooting.wav");

        }

        public IntPtr loadMusic(String source) {

            IntPtr music = SDL_mixer.Mix_LoadMUS(source);
            if (music == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return music;
        }

        private IntPtr loadSound(String source)
        {

            IntPtr sound = SDL_mixer.Mix_LoadWAV(source);
            if (sound == IntPtr.Zero)
            {
                Console.WriteLine("Failed to load! {0}", SDL.SDL_GetError());
            }
            return sound;
        }

        public void runSound(IntPtr soundSource) {
            SDL_mixer.Mix_PlayChannel(-1, soundSource, 0);
        }

        public void runMusic(IntPtr soundSource)
        {
            SDL_mixer.Mix_VolumeMusic(volumeMusic);
            SDL_mixer.Mix_PlayMusic(soundSource, -1);
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

        public void stopSound(IntPtr sound)
        {
            //Free the sound effects
            SDL_mixer.Mix_FreeChunk(sound);
            sound = IntPtr.Zero;
        }

        public void stopMusic(IntPtr music)
        {

            //Free the music
            SDL_mixer.Mix_FreeMusic(music);
            music = IntPtr.Zero;
        }
        public void cleanUp()
        {
            //Free the sound effects
            SDL_mixer.Mix_FreeChunk(_Scratch);
            SDL_mixer.Mix_FreeChunk(_High);
            SDL_mixer.Mix_FreeChunk(_Medium);
            SDL_mixer.Mix_FreeChunk(_MenuClick);
            SDL_mixer.Mix_FreeChunk(_MenuButtons);
            SDL_mixer.Mix_FreeChunk(_GameOver);
            SDL_mixer.Mix_FreeChunk(_Shooting);
            _Scratch = IntPtr.Zero;
            _High = IntPtr.Zero;
            _Medium = IntPtr.Zero;
            _MenuClick = IntPtr.Zero;
            _MenuButtons = IntPtr.Zero;
            _GameOver = IntPtr.Zero;
            _Shooting = IntPtr.Zero;

            //Free the music
            SDL_mixer.Mix_FreeMusic(_Music);
            _Music = IntPtr.Zero;
        }

    }
}
