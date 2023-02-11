using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class CharacterData
    {
        public Animation Run;
        public Animation Duck;
        public Animation Jump;

        public void SetConfig(CharacterConfig con)
        {
            Run = con.Run;
            Duck = con.Duck;
            Jump = con.Jump;
        }
    }
}
