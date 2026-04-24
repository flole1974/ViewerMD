using Markdig;

namespace ViewerMD;

public static class MarkdownRenderer
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    public static string ToHtml(string markdownContent)
    {
        string body = Markdown.ToHtml(markdownContent, Pipeline);
        return $$"""
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset="utf-8"/>
            <style>
              body { font-family: Segoe UI, sans-serif; max-width: 860px; margin: 40px auto; padding: 0 20px; color: #222; line-height: 1.6; }
              h1,h2,h3 { border-bottom: 1px solid #ddd; padding-bottom: .2em; }
              code { background: #f4f4f4; padding: 2px 5px; border-radius: 3px; font-family: Consolas, monospace; }
              pre code { display: block; padding: 12px; overflow-x: auto; }
              blockquote { border-left: 4px solid #ddd; margin: 0; padding-left: 16px; color: #555; }
              table { border-collapse: collapse; width: 100%; }
              th, td { border: 1px solid #ddd; padding: 6px 12px; }
              th { background: #f0f0f0; }
              img { max-width: 100%; }
              a { color: #0969da; }
            </style>
            </head>
            <body>
            {{body}}
            </body>
            </html>
            """;
    }
}
