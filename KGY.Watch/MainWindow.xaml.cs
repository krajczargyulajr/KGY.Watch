using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KGY.Watch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer = new Timer();
        private Timer conwayTimer = new Timer();
        private Conway conway;

        public MainWindow()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

            conwayTimer.Interval = 300;
            conwayTimer.Elapsed += ConwayTimer_Elapsed;

            InitializeComponent();

            Timer_Elapsed(null, null);

            timer.Start();
        }

        private void ConwayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                conway.Turn();
                ConwayCanvas.Children.Clear();

                int trueCount = 0;
                for (var x = 0; x < conway.Width; ++x)
                {
                    for (var y = 0; y < conway.Height; ++y)
                    {
                        if (conway.Cells[x, y])
                        {
                            var cellRect = new Rectangle();
                            cellRect.Fill = new SolidColorBrush(Colors.Green);
                            cellRect.Width = 3;
                            cellRect.Height = 3;
                            Canvas.SetLeft(cellRect, x * 3);
                            Canvas.SetTop(cellRect, y * 3);

                            ConwayCanvas.Children.Add(cellRect);
                            trueCount++;
                        }
                    }
                }
                ConwayCanvas.InvalidateVisual();
            });
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
            conwayTimer.Stop();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int width = (int)Math.Floor(ConwayCanvas.ActualWidth / 3);
            int height = (int)Math.Floor(ConwayCanvas.ActualHeight / 3);
            conway = new Conway(width, height);
            conway.RandomSeed();

            conwayTimer.Start();
        }
    }
}
