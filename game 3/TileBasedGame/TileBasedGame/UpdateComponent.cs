
namespace TileBasedGame
{
    class UpdateComponent : Component
    {
        UpdateManager UpdateManager;

        public HeroEvent hero;
        public UpdateComponent(UpdateManager um):base()
        {
            MessageBus.Register(this);
            this.UpdateManager = um;
        }

        public override void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.GameObject == this.GameObject)
            {
                if (he.EventType == HeroEvent.Type.Collision)
                {
                    hero = he;
                }
                else if (he.EventType == HeroEvent.Type.NeutralCollision)
                {
                    UpdatePosition(he);
                }
            }

            if (he.EventType == HeroEvent.Type.TakeCoin)
            {
                he.GameObject.Active = false;
                he.GameObject.Died = true;
                Program.Game.Player.Score++;
            }
            if (he.EventType == HeroEvent.Type.TakePower)
            {
                he.GameObject.Active = false;
                he.GameObject.Died = true;
                Program.Game.Player.Score += 5;
                if(Program.Game.Player.Lives < 10)
                    Program.Game.Player.Lives++;
                Program.Game.InfoBox.powerUpInfo();
            }
            if (he.EventType == HeroEvent.Type.LevelWin)
            {
                Program.Game.InfoBox.LevelWinInfo();
            }
        }

        public void DoUpdate()
        {
            if (hero != null)
                Update();
            hero = null;
        }

        public virtual void Update() {}

        public virtual void UpdatePosition(HeroEvent heroEv)
        {
            GameObject.PosY = heroEv.NewY;
            GameObject.PosX = heroEv.NewX;
        }
    }
}
