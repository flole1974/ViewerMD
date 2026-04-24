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
        WebView.CoreWebView2.WebMessageReceived -= OnWebMessage;
        WebView.CoreWebView2.WebMessageReceived += OnWebMessage;
        WebView.NavigateToString(html);
        Title = $"ViewerMD — {Path.GetFileName(path)}";
        PrintMenuItem.IsEnabled = true;
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

    private void OnPrint(object sender, RoutedEventArgs e)
    {
        WebView.CoreWebView2.ShowPrintUI(Microsoft.Web.WebView2.Core.CoreWebView2PrintDialogKind.System);
    }

    private void OnWebMessage(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
    {
        if (e.TryGetWebMessageAsString() == "printComplete")
            MessageBox.Show("Printing complete.", "ViewerMD", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void OnExit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
}
