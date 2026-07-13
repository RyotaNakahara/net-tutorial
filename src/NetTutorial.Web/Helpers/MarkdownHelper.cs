using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NetTutorial.Web.Helpers;

/// <summary>
/// レッスン本文の簡易 Markdown を HTML に変換します。
/// 学習用のため外部ライブラリに頼らず、必要最低限だけ実装しています。
/// </summary>
public static partial class MarkdownHelper
{
    public static string ToHtml(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return string.Empty;
        }

        var normalized = markdown.Replace("\r\n", "\n").Trim();
        var blocks = normalized.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var html = new StringBuilder();

        foreach (var rawBlock in blocks)
        {
            var block = rawBlock.Trim();

            if (block.StartsWith("```"))
            {
                html.Append(RenderFencedCode(block));
                continue;
            }

            if (block.StartsWith("|"))
            {
                html.Append(RenderTable(block));
                continue;
            }

            if (HeadingRegex().IsMatch(block))
            {
                var match = HeadingRegex().Match(block);
                var level = match.Groups[1].Value.Length;
                var text = Inline(match.Groups[2].Value.Trim());
                html.Append($"<h{level}>{text}</h{level}>");
                continue;
            }

            if (block.StartsWith("- "))
            {
                html.Append("<ul>");
                foreach (var line in block.Split('\n'))
                {
                    var item = line.TrimStart('-', ' ').Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        html.Append($"<li>{Inline(item)}</li>");
                    }
                }
                html.Append("</ul>");
                continue;
            }

            if (OrderedListRegex().IsMatch(block))
            {
                html.Append("<ol>");
                foreach (var line in block.Split('\n'))
                {
                    var item = OrderedItemRegex().Replace(line, string.Empty).Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        html.Append($"<li>{Inline(item)}</li>");
                    }
                }
                html.Append("</ol>");
                continue;
            }

            var paragraph = string.Join("<br />", block.Split('\n').Select(Inline));
            html.Append($"<p>{paragraph}</p>");
        }

        return html.ToString();
    }

    private static string RenderFencedCode(string block)
    {
        var lines = block.Split('\n');
        var language = lines[0].TrimStart('`').Trim();
        var codeLines = lines.Skip(1).TakeWhile(l => !l.StartsWith("```"));
        var code = WebUtility.HtmlEncode(string.Join("\n", codeLines));
        var langClass = string.IsNullOrWhiteSpace(language) ? string.Empty : $" language-{WebUtility.HtmlEncode(language)}";
        return $"<pre class=\"code-block\"><code class=\"{langClass.Trim()}\">{code}</code></pre>";
    }

    private static string RenderTable(string block)
    {
        var rows = block.Split('\n')
            .Select(r => r.Trim())
            .Where(r => r.StartsWith('|'))
            .Where(r => !SeparatorRowRegex().IsMatch(r))
            .ToList();

        if (rows.Count == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder("<div class=\"table-wrap\"><table>");
        for (var i = 0; i < rows.Count; i++)
        {
            var cells = rows[i].Trim('|').Split('|').Select(c => c.Trim()).ToArray();
            sb.Append("<tr>");
            foreach (var cell in cells)
            {
                var tag = i == 0 ? "th" : "td";
                sb.Append($"<{tag}>{Inline(cell)}</{tag}>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</table></div>");
        return sb.ToString();
    }

    private static string Inline(string text)
    {
        var encoded = WebUtility.HtmlEncode(text);
        encoded = BoldRegex().Replace(encoded, "<strong>$1</strong>");
        encoded = CodeRegex().Replace(encoded, "<code>$1</code>");
        encoded = LinkRegex().Replace(encoded, "<a href=\"$2\" target=\"_blank\" rel=\"noopener\">$1</a>");
        return encoded;
    }

    [GeneratedRegex(@"^(#{1,3})\s+(.+)$")]
    private static partial Regex HeadingRegex();

    [GeneratedRegex(@"^\d+\.\s", RegexOptions.Multiline)]
    private static partial Regex OrderedListRegex();

    [GeneratedRegex(@"^\d+\.\s+")]
    private static partial Regex OrderedItemRegex();

    [GeneratedRegex(@"^\|[\s\-:|]+\|$")]
    private static partial Regex SeparatorRowRegex();

    [GeneratedRegex(@"\*\*(.+?)\*\*")]
    private static partial Regex BoldRegex();

    [GeneratedRegex(@"`([^`]+)`")]
    private static partial Regex CodeRegex();

    [GeneratedRegex(@"\[([^\]]+)\]\(([^)]+)\)")]
    private static partial Regex LinkRegex();
}
