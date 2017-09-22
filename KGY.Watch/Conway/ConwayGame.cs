using KGY.Watch.Conway;
using KGY.Watch.Conway.Logic;
using KGY.Watch.Conway.Renderers;
using KGY.Watch.Conway.Seeders;
using KGY.Watch.Conway.Stages;
using System;
using System.Timers;
using System.Windows;

namespace KGY.Watch
{
    public class ConwayGame : IGame
    {
        public bool IsInitialized { get; private set; }

        private Timer t = new Timer();

        public IRenderer Renderer { get; set; }

        public ISeeder Seeder { get; set; }

        public IStage Stage { get; set; }

        public IGameLogic GameLogic { get; set; }

        public ConwayGame()
        {
            this.t.Interval = 300;
            this.t.Elapsed += Timer_Elapsed;
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            if (Stage == null) throw new Exception("Stage is not set");
            if (GameLogic == null) throw new Exception("Game logic is not set");

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
            GameLogic.Turn(this);
        }
    }
}
