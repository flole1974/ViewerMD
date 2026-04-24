using System.IO;
using System.Windows;

namespace ViewerMD;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var window = new MainWindow();
        window.Show();

        if (e.Args.Length > 0 && File.Exists(e.Args[0]))
            await window.LoadFileAsync(e.Args[0]);
    }
}
