
namespace TileBasedGame
{
    class HeroEvent : Event
    {
        public enum Type { StopMoving, Hurt, Collision, NeutralCollision, TryShooting, StopShooting, Shooting, ReloadShooting, EnemyShooting, EnemyDead, PlayerDead, GameOver, Click, MenuButton, Level1, Level2, Level3, LevelWin, FlyLeft, FlyRight, FlyUp, FlyStraight, ChangeImage, ChangeDirection, TakeCoin, TakePower, PowerUp, TryStanding, Jump }
        public Type EventType;
        public int Arg0;
        public GameObject GameObject;
        public float NewX;
        public float NewY;
        public HeroEvent(Type t, int arg0 = 0)
        {
            EventType = t;
            Arg0 = arg0;
        }
        public HeroEvent(Type t, GameObject gameObject)
        {
            EventType = t;
            GameObject = gameObject;       
        }
        public HeroEvent(Type t, GameObject gameObject, float x, float y)
        {
            EventType = t;
            GameObject = gameObject;
            NewX = x;
            NewY = y;
        }
    }
}
