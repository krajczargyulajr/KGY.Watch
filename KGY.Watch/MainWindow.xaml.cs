using KGY.Watch.Conway.Renderers;
using KGY.Watch.Conway.Seeders;
using KGY.Watch.Conway.Stages;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace KGY.Watch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer = new Timer();
        private ConwayGame conway;

        public MainWindow()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

            InitializeComponent();

            Timer_Elapsed(null, null);

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                txtTime.Text = $"{DateTime.Now.ToShortTimeString()}";
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            conway.Stop();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uint width = (uint)Math.Floor(ConwayCanvas.ActualWidth / 3);
            uint height = (uint)Math.Floor(ConwayCanvas.ActualHeight / 3);
            conway = new ConwayGame()
            {
                Renderer = new CanvasRenderer(ConwayCanvas),
                Seeder = new RandomSeeder(),
                Stage = new GridStage()
                {
                    Height = height,
                    Width = width
                }
            };

            conway.Start();
        }
    }
}
