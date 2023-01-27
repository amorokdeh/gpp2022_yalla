using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class PlayerUpdateComponent : UpdateComponent
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
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.PlayerDead));
        }

        public override void UpdatePosition(HeroEvent heroEv)
        {
            GameObject.PosY = heroEv.NewY;
            GameObject.PosX = heroEv.NewX;
        }

    }
}
