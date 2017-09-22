using System;

namespace KGY.Watch.Conway.Disasters
{
    public class Napalm : IDisaster
    {
        public void Strike(IGame game)
        {
            game.Stage.Clear();

            game.Initialize();

            Console.WriteLine("Napalm");
        }
    }
}
