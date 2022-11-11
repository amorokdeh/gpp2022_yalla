﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Game
    {
        public int display;
        public MainMenu mainMenu;
        public Level1 level1;
        public GameOver gameOver;
        public Game() {
            display = 1;
        }
        public void runMainMenu()
        {
            mainMenu = new MainMenu();
            mainMenu.run();
        }
        public void runLevel1()
        {
            level1 = new Level1();
            level1.run(); 
        }
        public void runGameOver()
        {
            gameOver = new GameOver();
            gameOver.run();
        }
        public void quit()
        {
            SDL.SDL_DestroyWindow(Program.window.show);
            SDL.SDL_Quit();
        }
        //Game loop
        public void run() {
            while (true) { 
                switch (display) { //if display = 0 end the game
                    case 0: quit(); return;
                    case 1: runMainMenu(); break;
                    case 2: runLevel1(); break;
                    case 3: runGameOver(); break;
                }
            }
            
        }
    }
}