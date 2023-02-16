using System;

namespace TileBasedGame
{
    class PlayerUpdateComponent : UpdateComponent
    {
        public PlayerUpdateComponent(UpdateManager um) : base(um) {}

        public override void Update()
        {
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
