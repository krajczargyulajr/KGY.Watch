using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace KGY.Watch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bitmap = new Bitmap(GetResourceStream(new System.Uri("pack://application:,,,/clock.png")).Stream);
            var iconHandle = bitmap.GetHicon();

            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Properties", PropertiesMenuItem_Click));
            contextMenu.MenuItems.Add(new MenuItem("-"));
            contextMenu.MenuItems.Add(new MenuItem("Exit", ExitMenuItem_Click));

            NotifyIcon ni = new NotifyIcon
            {
                ContextMenu = contextMenu,
                Icon = Icon.FromHandle(iconHandle),
                Visible = true
            };
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
