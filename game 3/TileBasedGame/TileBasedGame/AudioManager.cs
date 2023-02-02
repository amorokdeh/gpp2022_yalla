﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class AudioManager : Observer
    {
        private List<AudioComponent> _audioC = new List<AudioComponent>();

        private int volumeMusic = 50;
        private int volumeSound = 30;

        public int _volumMin = 0;
        public int _volumMax = 250;

        public AudioManager()
        {
            // Initialize SDL_mixer
            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0)
            {
                Console.WriteLine("SDL_mixer could not initialize! SDL_mixer Error: {0}", SDL.SDL_GetError());
            }
            MessageBus.Register(this);
        }

        public void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.Collision)
            {
                RunSound("Enemy dead");
            }
            else if (he.EventType == HeroEvent.Type.EnemyShooting)
            {
                RunSound("Shooting enemy");
            }
            else if (he.EventType == HeroEvent.Type.Shooting)
            {
                RunSound("Shooting");
            }
            else if (he.EventType == HeroEvent.Type.EnemyDead)
            {
                RunSound("Enemy dead");
            }
            else if (he.EventType == HeroEvent.Type.GameOver)
            {
                RunSound("Game Over");
            }
            else if (he.EventType == HeroEvent.Type.Click)
            {
                RunSound("Menu click");
            }
            else if (he.EventType == HeroEvent.Type.MenuButton)
            {
                RunSound("Menu buttons");
            }
            else if (he.EventType == HeroEvent.Type.PlayerDead)
            {
                RunSound("Player dead");
            }
            else if (he.EventType == HeroEvent.Type.Level1)
            {
                StopMusic();
                RunMusic("Level1 music");
            }
            else if (he.EventType == HeroEvent.Type.Level2)
            {
                StopMusic();
                RunMusic("Level2 music");
            }
            else if (he.EventType == HeroEvent.Type.Level3)
            {
                StopMusic();
                RunMusic("Level3 music");
            }
        }
        public void AddAudio(AudioComponent au)
        {
            _audioC.Add(au);
        }
        public void RunSound(string sound)
        {
            Search(sound).RunSound();
        }
        public void RunMusic(string music)
        {
            Search(music).RunMusic();
        }
        public void StopMusic()
        {
            SDL_mixer.Mix_HaltMusic();
        }
        public void ChangeVolumeMusic(int volume)
        {
            if (volume > _volumMax) { volume = _volumMin; }
            else if (volume < _volumMin) { volume = _volumMax; }

            volumeMusic = volume;
            foreach (var audio in _audioC) {
                audio.ChangeVolumeMusic(volume);
            }
        }
        public int GetVolumeMusic()
        {
            return volumeMusic;
        }
        public void ChangeVolumeSound(int volume)
        {
            if (volume > _volumMax) { volume = _volumMin; }
            else if (volume < _volumMin) { volume = _volumMax; }

            volumeSound = volume;
            foreach (var audio in _audioC){
                audio.ChangeVolumeSound(volume);
            }
        }
        public int GetVolumeSound()
        {
            return volumeSound;
        }
        public void CleanUp()
        {
            foreach (var audio in _audioC){
                audio.clean();
            }
        }
        public AudioComponent Search(string sound)
        {
            switch (sound)
            {
                case "Menu buttons": return Program.Game._loader.MenuButtons;
                case "Menu click": return Program.Game._loader.MenuClick;
                case "Menu music": return Program.Game._loader.MenuMusic;
                case "Level1 music": return Program.Game._loader.Level1Music;
                case "Level2 music": return Program.Game._loader.Level2Music;
                case "Level3 music": return Program.Game._loader.Level3Music;
                case "Game Over": return Program.Game._loader.GameOver;
                case "Shooting": return Program.Game._loader.Shooting;
                case "Shooting enemy": return Program.Game._loader.ShootingEnemy;
                case "Enemy dead": return Program.Game._loader.ExplodEnemy;
                case "Player dead": return Program.Game._loader.ExplodPlayer;

            }
            return null;
        }

    }
}
