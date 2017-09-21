using System;

namespace KGY.Watch.Conway.Disasters
{
    public class Meteor : IDisaster
    {
        public void Strike(ConwayGame game)
        {
            var r = new Random();
            uint startX = r.Next(game.Stage.Width);
            uint startY = r.Next(game.Stage.Height);
            uint endX = r.Next(game.Stage.Width);
            uint endY = r.Next(game.Stage.Height);
            double length = Math.Sqrt(Math.Pow((startX - endX), 2) + Math.Pow((startY - endY), 2));
            int size = (int)Math.Ceiling(length / 10);

            // DDA
            double dx = endX - startX;
            double dy = endY - startY;

            int steps;
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                steps = (int)Math.Abs(dx);
            }
            else
            {
                steps = (int)Math.Abs(dy);
            }

            double Xincrement = dx / steps;
            double Yincrement = dy / steps;

            double x = startX;
            double y = startY;
            for (int i = 0; i < steps; ++i)
            {
                x = x + Xincrement;
                y = y + Yincrement;
                game.Stage.SetCell((uint)Math.Ceiling(x), (uint)Math.Ceiling(y), false);
            }

            Console.WriteLine($"Meteor [{startX},{startY}] [{endX},{endY}]");
        }
    }
}
