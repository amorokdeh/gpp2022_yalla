
namespace TileBasedGame
{
    class Component : Observer
    {
        public GameObject GameObject;

        public Component() {}

        public virtual void OnEvent(Event e) {}

        public virtual void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
