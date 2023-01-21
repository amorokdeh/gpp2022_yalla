using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class PlayerUpdateComponent:UpdateComponent
    {
        public PlayerUpdateComponent(UpdateManager um) : base(um)
        {
        }

        public override void Update()
        {
            Level.rounds++;
            Console.WriteLine("PLAYER");
            //TODO Überarbeiten
            Program.Game.Player.Lives--;
            Program.Game._audio.RunSound("Player dead");
        }
    }
}
