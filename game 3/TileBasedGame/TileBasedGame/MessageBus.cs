
namespace TileBasedGame
{
    static class MessageBus
    {
        static Observable dispatcher = new Observable();

        static public void Register(Observer o)
        {
            dispatcher.AddObserver(o);
        }
        static public void PostEvent(Event evt)
        {
            dispatcher.Notify(evt);
        }

        static public void Clean()
        {
            dispatcher.Clean();
        }
    }
}
