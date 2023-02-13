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
        public Animation Stand;
        public Animation Idle;

        public void SetConfig(CharacterConfig con)
        {
            Run = new Animation(con.Run.Frames, con.Run.Duration);
            Duck = new Animation(con.Duck.Frames, con.Duck.Duration);
            Jump = new Animation(con.Jump.Frames, con.Jump.Duration);
            Stand = new Animation(con.Stand.Frames, con.Stand.Duration);
            Idle = new Animation(con.Idle.Frames, con.Idle.Duration);
        }
    }
}
