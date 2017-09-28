using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KGY.Watch.Conway.Renderers
{
    public class BitmapRenderer : IRenderer
    {
        private Canvas _canvas;

        public BitmapRenderer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Render(ConwayGame game)
        {
            Bitmap bitmap = new Bitmap((int)game.Stage.Width * 3, (int)game.Stage.Height * 3);
            for (uint x = 0; x < game.Stage.Width; ++x)
            {
                for (uint y = 0; y < game.Stage.Height; ++y)
                {
                    if (game.Stage.GetCell(x, y))
                    {
                        var ixBase = x * 3;
                        var iyBase = y * 3;
                        for (var ix = x * 3; ix < ixBase + 3; ++ix)
                        {
                            for (var iy = y * 3; iy < iyBase + 3; ++iy)
                            {
                                bitmap.SetPixel((int)ix, (int)iy, System.Drawing.Color.Green);
                            }
                        }
                    }
                }
            }

            BitmapImage img = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                img.BeginInit();
                img.StreamSource = memory;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
            }

            _canvas.Background = new ImageBrush()
            {
                ImageSource = img
            };

            _canvas.InvalidateVisual();
        }
    }
}
