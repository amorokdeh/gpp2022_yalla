
namespace TileBasedGame
{
    interface State
    {
        PhysicsComponent.AnimationState HandleInput(MovingEvent me);
        void Update(float timeStep);
        void Enter(GameObject gameObject);
        string GetDirection();
        bool GetFlipped();
        void SetValues(string direction, bool flipped);
    }
}
