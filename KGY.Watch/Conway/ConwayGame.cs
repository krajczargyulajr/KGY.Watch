using KGY.Watch.Conway.Disasters;
using KGY.Watch.Conway.Renderers;
using KGY.Watch.Conway.Seeders;
using KGY.Watch.Conway.Stages;
using System;
using System.Timers;
using System.Windows;

namespace KGY.Watch
{
    public class ConwayGame
    {
        public bool IsInitialized { get; private set; }

        private Timer t = new Timer();

        public IRenderer Renderer { get; set; }

        public ISeeder Seeder { get; set; }

        public IStage Stage { get; set; }

        public ConwayGame()
        {
            this.t.Interval = 300;
            this.t.Elapsed += Timer_Elapsed;
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            if (Stage == null) throw new Exception("Stage is not set");

            Stage.Initialize();

            if (Seeder != null)
            {
                Seeder.Seed(this);
            }
        }

        public void Start()
        {
            if (!IsInitialized) Initialize();

            t.Start();
        }

        public void Stop() => t.Stop();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Turn();

                if (Renderer != null)
                {
                    Renderer.Render(this);
                }
            });
        }

        public void Turn()
        {
            Random r = new Random();
            var isDisasterTime = r.Next(100) > 90;

            if (isDisasterTime)
            {
                IntroduceDisaster();
            }
            else
            {
                IStage nextStage = Stage.Clone();

                for (uint x = 0; x < Stage.Width; ++x)
                {
                    for (uint y = 0; y < Stage.Height; ++y)
                    {
                        var neighborCount = Stage.NeighborCount(x, y);
                        if (Stage.GetCell(x, y))
                        {
                            if (neighborCount < 2 || neighborCount > 3)
                            {
                                nextStage.SetCell(x, y, false);
                            }
                        }
                        else
                        {
                            if (neighborCount == 3)
                            {
                                nextStage.SetCell(x, y, true);
                            }
                        }
                    }
                }

                Stage = nextStage;
            }
        }

        public void IntroduceDisaster()
        {
            var rSeed = new Random(DateTime.Now.Second).Next(100);

            IDisaster disaster = null;

            if (rSeed < 30) Console.WriteLine("Lucky bastard");
            else if (rSeed < 60) disaster = new ABomb();
            else if (rSeed < 95) disaster = new Meteor();
            else disaster = new Napalm();

            if (disaster != null)
            {
                disaster.Strike(this);
            }
        }
    }
}
