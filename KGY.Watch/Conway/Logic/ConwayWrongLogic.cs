using KGY.Watch.Conway.Disasters;
using KGY.Watch.Conway.Stages;
using System;

namespace KGY.Watch.Conway.Logic
{
    public class ConwayWrongLogic : IGameLogic
    {
        public void Turn(IGame game)
        {
            Random r = new Random();
            var isDisasterTime = r.Next(100) > 90;

            if (isDisasterTime)
            {
                IntroduceDisaster(game);
            }
            else
            {
                IStage nextStage = game.Stage;

                for (uint x = 0; x < game.Stage.Width; ++x)
                {
                    for (uint y = 0; y < game.Stage.Height; ++y)
                    {
                        var neighborCount = game.Stage.NeighborCount(x, y);
                        var cell = game.Stage.GetCell(x, y);
                        if (cell)
                        {
                            if (neighborCount < 2 || neighborCount > 3)
                            {
                                nextStage.SetCell(x, y, false);
                            }
                        }
                        else if (neighborCount == 3)
                        {
                            nextStage.SetCell(x, y, true);
                        }
                        else
                        {
                            nextStage.SetCell(x, y, cell);
                        }
                    }
                }

                if (nextStage.IsAlive)
                {
                    game.Stage = nextStage;
                }
            }
        }

        public void IntroduceDisaster(IGame game)
        {
            var rSeed = new Random(DateTime.Now.Second).Next(100);

            IDisaster disaster = null;

            if (rSeed < 30) Console.WriteLine("Lucky bastard");
            else if (rSeed < 60) disaster = new ABomb();
            else if (rSeed < 95) disaster = new Meteor();
            else disaster = new Napalm();

            if (disaster != null)
            {
                // TODO: remove cast
                disaster.Strike((ConwayGame)game);
            }
        }
    }
}
