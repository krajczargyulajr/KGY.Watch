using System;

namespace KGY.Watch.Conway.Disasters
{
    public class ABomb : IDisaster
    {
        public void Strike(IGame game)
        {
            var r = new Random();

            uint xLower = r.Next(game.Stage.Width);
            uint xUpper = r.Next(xLower, game.Stage.Width);
            uint yLower = r.Next(game.Stage.Height);
            uint yUpper = r.Next(yLower, game.Stage.Height);

            game.Stage.Clear(xLower, xUpper, yLower, yUpper);
            Console.WriteLine($"ABomb strike [{xLower},{yLower}] [{xUpper},{yUpper}]");
        }
    }
}
