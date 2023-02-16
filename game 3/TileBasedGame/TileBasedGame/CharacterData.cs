
namespace TileBasedGame
{
    class CharacterData
    {
        public Animation Idle;
        public Animation Run;
        public Animation Duck;
        public Animation Jump;
        public Animation Stand;

        public void SetConfig(CharacterConfig con)
        {
            Idle = new Animation(con.Idle.Frames, con.Idle.Duration);
            Run = new Animation(con.Run.Frames, con.Run.Duration);
            Duck = new Animation(con.Duck.Frames, con.Duck.Duration);
            Jump = new Animation(con.Jump.Frames, con.Jump.Duration);
            Stand = new Animation(con.Stand.Frames, con.Stand.Duration);
        }
    }
}
