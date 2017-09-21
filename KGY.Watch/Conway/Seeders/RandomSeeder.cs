using System;

namespace KGY.Watch.Conway.Seeders
{
    public class RandomSeeder : ISeeder
    {
        public void Seed(ConwayGame game)
        {
            Random r = new Random(DateTime.Now.Millisecond);

            for (uint x = 0; x < game.Stage.Width; ++x)
            {
                for (uint y = 0; y < game.Stage.Height; ++y)
                {
                    var rnext = r.Next(10);
                    if (rnext > 4) game.Stage.SetCell(x, y, true);
                    else game.Stage.SetCell(x, y, false);
                }
            }
        }
    }
}
