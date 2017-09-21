using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KGY.Watch.Conway.Renderers
{
    public class CanvasRenderer : IRenderer
    {
        private readonly Canvas _canvas;

        public CanvasRenderer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Render(ConwayGame game)
        {
            _canvas.Children.Clear();

            int trueCount = 0;
            for (uint x = 0; x < game.Stage.Width; ++x)
            {
                for (uint y = 0; y < game.Stage.Height; ++y)
                {
                    if (game.Stage.GetCell(x, y))
                    {
                        var cellRect = new Rectangle();
                        cellRect.Fill = new SolidColorBrush(Colors.Green);
                        cellRect.Width = 3;
                        cellRect.Height = 3;
                        Canvas.SetLeft(cellRect, x * 3);
                        Canvas.SetTop(cellRect, y * 3);

                        _canvas.Children.Add(cellRect);
                        trueCount++;
                    }
                }
            }

            _canvas.InvalidateVisual();
        }
    }
}
