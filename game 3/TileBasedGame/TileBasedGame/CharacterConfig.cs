﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace TileBasedGame
{
    class CharacterConfig
    {
        public Animation Run;
        public Animation Duck;
        public Animation Jump;
        public Animation Stand;
        public Animation Idle;
        
        public static CharacterConfig Load(string fileName)
        {
            CharacterConfig config = JsonConvert.DeserializeObject<CharacterConfig>(fileName);

            return config;
        }
    }
}
