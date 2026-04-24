using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace ViewerMD;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        WebView.NavigationCompleted += (_, _) => { };
    }

    public async Task LoadFileAsync(string path)
    {
        string markdown = await File.ReadAllTextAsync(path);
        string html = MarkdownRenderer.ToHtml(markdown);
        await WebView.EnsureCoreWebView2Async();
        WebView.NavigateToString(html);
        Title = $"ViewerMD — {Path.GetFileName(path)}";
    }

    private async void OnOpen(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Markdown files (*.md;*.markdown)|*.md;*.markdown|All files (*.*)|*.*",
            Title = "Open Markdown file"
        };

        if (dialog.ShowDialog() == true)
            await LoadFileAsync(dialog.FileName);
    }

    private void OnExit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
}
