using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Structure;

public class IndexModel : PageModel
{
    public IReadOnlyList<StructureItem> Items { get; } =
    [
        new("NetTutorial.sln", "ソリューション。複数プロジェクトをまとめる入口です。"),
        new("src/NetTutorial.Core/", "クラスライブラリ。モデル・サービスなど再利用可能なロジック。"),
        new("src/NetTutorial.Core/Models/", "Lesson / CodeSample などのデータ構造定義。"),
        new("src/NetTutorial.Core/Services/", "ILessonService と LessonService（学習コンテンツ提供者）。"),
        new("src/NetTutorial.Core/Demos/", "Greeter など、呼び出しデモ用の小さなクラス。"),
        new("src/NetTutorial.Web/", "ASP.NET Core Razor Pages の Web アプリ（UI）。"),
        new("Program.cs", "起動設定。サービス登録 (DI) と HTTP パイプラインを組み立てます。"),
        new("Pages/", "画面。`.cshtml` がマークアップ、`.cshtml.cs` が PageModel（C#）。"),
        new("wwwroot/", "CSS・JS・画像などの静的ファイル置き場。"),
        new("appsettings.json", "環境設定。接続文字列やログレベルなどを置く場所です。")
    ];

    public record StructureItem(string Path, string Description);
}
