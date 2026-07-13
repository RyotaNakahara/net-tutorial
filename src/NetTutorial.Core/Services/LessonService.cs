using NetTutorial.Core.Models;

namespace NetTutorial.Core.Services;

/// <summary>
/// 学習レッスンをメモリ上に保持する実装です。
/// 本アプリでは DB を使わず、コード内にコンテンツを定義して構造をシンプルに保っています。
/// </summary>
public class LessonService : ILessonService
{
    private readonly IReadOnlyList<Lesson> _lessons;

    public LessonService()
    {
        _lessons = CreateLessons();
    }

    public IReadOnlyList<Lesson> GetAll() =>
        _lessons.OrderBy(l => l.Order).ToList();

    public Lesson? GetById(string id) =>
        _lessons.FirstOrDefault(l =>
            string.Equals(l.Id, id, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Lesson> GetByCategory(LessonCategory category) =>
        _lessons.Where(l => l.Category == category).OrderBy(l => l.Order).ToList();

    private static IReadOnlyList<Lesson> CreateLessons() =>
    [
        new Lesson
        {
            Id = "dotnet-overview",
            Title = ".NET とは何か",
            Summary = "ランタイム・SDK・言語の関係と、.NET の全体像を学びます。",
            Category = LessonCategory.Overview,
            Order = 1,
            KeyPoints =
            [
                ".NET はクロスプラットフォームの開発プラットフォームです",
                "C# で書いたコードは中間言語 (IL) にコンパイルされ、ランタイムで実行されます",
                "SDK（ソフトウェア開発キット）にコンパイラやテンプレートが含まれます",
                "ASP.NET Core / コンソール / クラスライブラリなど、複数のアプリ種別があります"
            ],
            ContentMarkdown =
                """
                .NET（ドットネット）は、Microsoft が提供するオープンソースの開発プラットフォームです。
                Web・デスクトップ・モバイル・クラウド・ゲームなど、さまざまなアプリを同じ言語・共通の基盤で作れます。

                ## 構成要素

                | 要素 | 役割 |
                | --- | --- |
                | **言語 (C# / F# / VB)** | ソースコードを書く |
                | **コンパイラ** | ソースを中間言語 (IL) に変換する |
                | **ランタイム (CLR)** | IL を実行し、メモリ管理などを担当する |
                | **ベースクラスライブラリ (BCL)** | 文字列・コレクション・ファイル I/O などの標準機能 |
                | **SDK** | `dotnet` CLI、テンプレート、ビルドツール一式 |

                ## 実行の流れ（ざっくり）

                1. `Program.cs` などに C# を書く
                2. `dotnet build` で IL（.dll）にコンパイルされる
                3. `dotnet run` でランタイムがその DLL を実行する

                ## このアプリで使っているもの

                - **.NET 8 SDK** … 開発・実行に使用
                - **C#** … 主要言語
                - **ASP.NET Core Razor Pages** … Web UI
                - **クラスライブラリ** … 共有ロジック（`NetTutorial.Core`）
                """,
            CodeSamples =
            [
                new CodeSample(
                    "最小のコンソールアプリ",
                    "csharp",
                    """
                    // トップレベルステートメント（.NET 6+）
                    // 明示的な Main メソッドがなくてもここから実行が始まります
                    Console.WriteLine("Hello, .NET!");
                    """,
                    "エントリポイントの簡略化により、小さなプログラムが短く書けます。")
            ]
        },
        new Lesson
        {
            Id = "project-structure",
            Title = "ソリューションとプロジェクト構成",
            Summary = "このリポジトリ自体を教材に、.sln / .csproj / フォルダ役割を理解します。",
            Category = LessonCategory.Structure,
            Order = 2,
            KeyPoints =
            [
                "ソリューション (.sln) は複数プロジェクトをまとめる器です",
                "プロジェクト (.csproj) がビルド単位です",
                "Web は UI、Core は再利用可能なロジック、という分離が一般的です",
                "プロジェクト参照で依存関係を明示します"
            ],
            ContentMarkdown =
                """
                このリポジトリは、学習しやすいように **2 つのプロジェクト** に分かれています。

                ## ディレクトリ構成

                ```
                NetTutorial.sln                 … ソリューション（全体のまとめ）
                src/
                  NetTutorial.Core/             … クラスライブラリ（ビジネスロジック）
                    Models/                     … データ構造（Lesson など）
                    Services/                   … 処理（レッスン取得）
                    Demos/                      … 簡単なデモクラス
                  NetTutorial.Web/              … ASP.NET Core Web アプリ（画面）
                    Pages/                      … Razor Pages（画面ごと）
                    wwwroot/                    … CSS / JS / 画像などの静的ファイル
                    Program.cs                  … アプリ起動と DI・ミドルウェア設定
                    appsettings.json            … 設定ファイル
                ```

                ## なぜ分けるのか

                - **関心の分離**: UI の表示と、学習データの管理を分けて理解しやすくする
                - **再利用**: Core は別のアプリ（コンソールや API）からも参照できる
                - **テストしやすさ**: UI なしでロジックだけをテストしやすい

                ## 主要なファイル

                | ファイル | 意味 |
                | --- | --- |
                | `*.sln` | Visual Studio / `dotnet` が扱うソリューション定義 |
                | `*.csproj` | 対象フレームワーク、パッケージ参照、プロジェクト参照 |
                | `Program.cs` | アプリの起動処理（サービスの登録・HTTP パイプライン） |
                | `Pages/*.cshtml` | HTML に近いマークアップ（Razor 構文） |
                | `Pages/*.cshtml.cs` | ページの裏側（PageModel = C# ロジック） |
                """,
            CodeSamples =
            [
                new CodeSample(
                    "プロジェクト参照のイメージ",
                    "xml",
                    """
                    <!-- NetTutorial.Web.csproj 内（概念） -->
                    <ItemGroup>
                      <ProjectReference Include="..\NetTutorial.Core\NetTutorial.Core.csproj" />
                    </ItemGroup>
                    """,
                    "Web から Core を参照することで、LessonService や Greeter を使えます。")
            ]
        },
        new Lesson
        {
            Id = "csharp-basics",
            Title = "C# の基本構文（変数・型・演算）",
            Summary = "変数宣言、組み込み型、文字列補間など、最初に覚える構文です。",
            Category = LessonCategory.Syntax,
            Order = 3,
            KeyPoints =
            [
                "型があることでコンパイル時に多くの誤りを検出できます",
                "var は型推論であり、型がなくなるわけではありません",
                "文字列補間 ($\"...\") で読みやすい文字列が作れます",
                "null 許容参照型で「null になりうるか」を意識できます"
            ],
            ContentMarkdown =
                """
                C# は静的型付け言語です。変数には型があり、代入できる値が制限されます。

                ## よく使う型

                | 型 | 例 | 用途 |
                | --- | --- | --- |
                | `int` | `42` | 整数 |
                | `double` / `decimal` | `3.14` / `9.99m` | 小数（お金は decimal） |
                | `bool` | `true` / `false` | 真偽値 |
                | `string` | `"hello"` | 文字列 |
                | `DateTime` | `DateTime.Now` | 日時 |
                | `List<T>` | `new List<int>()` | 可変長のリスト |

                ## 宣言のパターン

                - `int count = 0;` … 明示的に型を書く
                - `var name = "Ryota";` … 右辺から型を推論（この場合は string）
                - `string? maybe = null;` … null を許容する参照型
                """,
            CodeSamples =
            [
                new CodeSample(
                    "変数と文字列補間",
                    "csharp",
                    """
                    int age = 20;
                    string name = "太郎";
                    var message = $"{name} さんは {age} 歳です。";

                    // 定数（再代入不可）
                    const double Pi = 3.14159;

                    // null 許容型
                    string? nickname = null;
                    Console.WriteLine(nickname ?? "未設定");
                    """,
                    "`??` は左側が null のときに右側を使う演算子です。")
            ]
        },
        new Lesson
        {
            Id = "control-flow",
            Title = "制御構文（if / switch / ループ）",
            Summary = "条件分岐と繰り返しの書き方を学びます。",
            Category = LessonCategory.Syntax,
            Order = 4,
            KeyPoints =
            [
                "if / else で条件分岐します",
                "switch 式は値を返す分岐に便利です",
                "for / foreach / while で繰り返し処理します",
                "break / continue / return で流れを制御します"
            ],
            ContentMarkdown =
                """
                プログラムの「流れ」を変える構文が制御構文です。

                ## 使い分けの目安

                - **if**: 条件が少数・複雑な論理式があるとき
                - **switch**: 同じ値を複数パターンで分岐するとき
                - **foreach**: コレクションを順番に処理するとき（最もよく使う）
                - **for**: インデックスが必要なとき
                """,
            CodeSamples =
            [
                new CodeSample(
                    "if と switch 式",
                    "csharp",
                    """
                    int score = 85;

                    if (score >= 80)
                    {
                        Console.WriteLine("合格");
                    }
                    else
                    {
                        Console.WriteLine("要復習");
                    }

                    // switch 式（値を返す）
                    string grade = score switch
                    {
                        >= 90 => "A",
                        >= 80 => "B",
                        >= 70 => "C",
                        _ => "D" // それ以外
                    };
                    """),
                new CodeSample(
                    "ループ",
                    "csharp",
                    """
                    var lessons = new[] { "概要", "構文", "DI" };

                    foreach (var title in lessons)
                    {
                        Console.WriteLine($"・{title}");
                    }

                    for (int i = 0; i < lessons.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {lessons[i]}");
                    }
                    """)
            ]
        },
        new Lesson
        {
            Id = "classes-methods",
            Title = "クラス・メソッド・プロパティ",
            Summary = "オブジェクト指向の基本。このアプリの Models / Services も同じ考え方です。",
            Category = LessonCategory.Syntax,
            Order = 5,
            KeyPoints =
            [
                "クラスはデータと振る舞いをまとめた設計図です",
                "プロパティでフィールドへの安全なアクセスを提供します",
                "メソッドは振る舞い（処理）を定義します",
                "インターフェースは「できること」の契約です"
            ],
            ContentMarkdown =
                """
                `NetTutorial.Core` の `Lesson` や `Greeter` もクラスです。
                Web 側の `LessonService` 実装を通じて、クラス図書館のコードを呼び出す流れを体験できます。

                ## 用語の整理

                - **クラス**: 型の定義（設計図）
                - **インスタンス**: `new` で作った実体
                - **フィールド**: 内部状態を保持する変数（多くは `private`）
                - **プロパティ**: `get` / `set` で公開するデータ
                - **メソッド**: 処理を行う関数
                - **コンストラクタ**: 生成時の初期化処理
                """,
            CodeSamples =
            [
                new CodeSample(
                    "クラスの例（このアプリの Greeter に近い形）",
                    "csharp",
                    """
                    public class Greeter
                    {
                        private readonly string _name;

                        public Greeter(string name)
                        {
                            _name = name;
                        }

                        public string SayHello()
                        {
                            return $"こんにちは、{_name} さん！";
                        }
                    }

                    var greeter = new Greeter("学習者");
                    Console.WriteLine(greeter.SayHello());
                    """,
                    "「デモ」ページでは、この Greeter を実際に呼び出しています。")
            ]
        },
        new Lesson
        {
            Id = "collections-linq",
            Title = "コレクションと LINQ",
            Summary = "配列・List・辞書と、データを問い合わせる LINQ を学びます。",
            Category = LessonCategory.Syntax,
            Order = 6,
            KeyPoints =
            [
                "List<T> は可変のコレクションの定番です",
                "LINQ で Where / Select / OrderBy などが使えます",
                "メソッド構文（.Where(...)）が実務でよく使われます",
                "このアプリの LessonService も LINQ でフィルタしています"
            ],
            ContentMarkdown =
                """
                複数のデータを扱うときはコレクションを使います。
                さらに **LINQ（Language Integrated Query）** を使うと、条件抽出・変換・並べ替えが簡潔に書けます。

                ## このアプリでの実例

                `LessonService.GetByCategory` は次のような LINQ です。

                - `Where` … 条件に合う要素だけ残す
                - `OrderBy` … 並べ替え
                - `ToList` … 結果をリスト化
                """,
            CodeSamples =
            [
                new CodeSample(
                    "List と LINQ",
                    "csharp",
                    """
                    var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

                    var evenSquares = numbers
                        .Where(n => n % 2 == 0)   // 偶数だけ
                        .Select(n => n * n)       // 二乗
                        .OrderByDescending(n => n)
                        .ToList();

                    // evenSquares => [36, 16, 4]
                    """,
                    "`n => ...` はラムダ式（無名の短い関数）です。")
            ]
        },
        new Lesson
        {
            Id = "aspnet-pipeline",
            Title = "ASP.NET Core の起動とリクエスト処理",
            Summary = "Program.cs・ミドルウェア・ルーティングの基礎を理解します。",
            Category = LessonCategory.Framework,
            Order = 7,
            KeyPoints =
            [
                "Program.cs でサービス登録と HTTP パイプラインを組み立てます",
                "ミドルウェアはリクエストを順番に処理する部品です",
                "Razor Pages は URL と Pages フォルダが対応します",
                "静的ファイルは wwwroot から配信されます"
            ],
            ContentMarkdown =
                """
                Web アプリでは、ブラウザからの HTTP リクエストを一連の処理（パイプライン）で扱います。

                ## Program.cs の二段構え

                1. **サービスの登録**（`builder.Services...`）
                   - DI コンテナに部品を登録する
                2. **パイプラインの構成**（`app.Use...` / `app.Map...`）
                   - リクエストの流れ（HTTPS 化、静的ファイル、認可、ページ割り当て）を定義する

                ## Razor Pages の対応例

                | URL | ファイル |
                | --- | --- |
                | `/` | `Pages/Index.cshtml` |
                | `/Lessons` | `Pages/Lessons/Index.cshtml` |
                | `/Lessons/Details?id=...` | `Pages/Lessons/Details.cshtml` |
                """,
            CodeSamples =
            [
                new CodeSample(
                    "Program.cs の要点",
                    "csharp",
                    """
                    var builder = WebApplication.CreateBuilder(args);

                    // 1) サービス登録（DI）
                    builder.Services.AddRazorPages();
                    builder.Services.AddSingleton<ILessonService, LessonService>();

                    var app = builder.Build();

                    // 2) ミドルウェア（上から順に処理）
                    app.UseHttpsRedirection();
                    app.UseStaticFiles();
                    app.UseRouting();
                    app.MapRazorPages();

                    app.Run();
                    """)
            ]
        },
        new Lesson
        {
            Id = "dependency-injection",
            Title = "依存性の注入 (DI)",
            Summary = "new をあちこちに書かず、必要な部品を外側から渡す考え方です。",
            Category = LessonCategory.Framework,
            Order = 8,
            KeyPoints =
            [
                "依存関係をコンストラクタで受け取るのが定石です",
                "インターフェースに依存すると柔軟になります",
                "AddSingleton / AddScoped / AddTransient で寿命が変わります",
                "このアプリでは ILessonService を Singleton 登録しています"
            ],
            ContentMarkdown =
                """
                **依存性の注入（Dependency Injection）** は、クラスが自分で依存を `new` せず、外から渡してもらう設計です。

                ## 寿命（ライフタイム）

                | 登録方法 | 寿命 |
                | --- | --- |
                | `AddSingleton` | アプリ全体で 1 つ |
                | `AddScoped` | リクエストごとに 1 つ |
                | `AddTransient` | 取得するたびに新規 |

                レッスンデータはアプリ起動中に変わらないため、本アプリでは `AddSingleton` を使っています。
                """,
            CodeSamples =
            [
                new CodeSample(
                    "登録と受け取り",
                    "csharp",
                    """
                    // Program.cs
                    builder.Services.AddSingleton<ILessonService, LessonService>();

                    // PageModel（コンストラクタ注入）
                    public class IndexModel : PageModel
                    {
                        private readonly ILessonService _lessons;

                        public IndexModel(ILessonService lessons)
                        {
                            _lessons = lessons;
                        }
                    }
                    """,
                    "PageModel は「必要なもの」を受け取るだけで、生成方法を知りません。")
            ]
        },
        new Lesson
        {
            Id = "async-await",
            Title = "非同期処理 (async / await)",
            Summary = "I/O 待ちをブロックせず、応答性の高いコードを書くための構文です。",
            Category = LessonCategory.Syntax,
            Order = 9,
            KeyPoints =
            [
                "async メソッドは Task または Task<T> を返します",
                "await で非同期操作の完了を待ちます",
                "ファイル・HTTP・DB などの I/O と相性が良いです",
                "UI やサーバのスレッドを無駄に塞がないのが目的です"
            ],
            ContentMarkdown =
                """
                Web アプリや API 呼び出しでは、待ち時間の大半がネットワークやディスクです。
                `async` / `await` を使うと、待ちの間にスレッドを解放しやすくなります。

                ## 基本ルール

                - メソッド名は慣習的に `Async` で終わる（例: `GetLessonAsync`）
                - `await` できるのは awaitable（主に `Task`）
                - 例外は通常の `try/catch` で捕捉できる
                """,
            CodeSamples =
            [
                new CodeSample(
                    "非同期メソッドの例",
                    "csharp",
                    """
                    public async Task<string> FetchTitleAsync(HttpClient client)
                    {
                        // ネットワーク待ちのあいだ、スレッドを占有しにくい
                        string json = await client.GetStringAsync("https://example.com");
                        return json.Length > 0 ? "取得成功" : "空レスポンス";
                    }
                    """)
            ]
        },
        new Lesson
        {
            Id = "next-steps",
            Title = "次に学ぶとよいこと",
            Summary = "このアプリを起点に、伸ばしていく学習の道筋を示します。",
            Category = LessonCategory.Overview,
            Order = 10,
            KeyPoints =
            [
                "公式ドキュメントと dig into する習慣をつける",
                "小さな機能を自分で足して破壊的に試す",
                "Entity Framework / API / 認証など応用へ進む",
                "テスト（xUnit）でロジックを守る"
            ],
            ContentMarkdown =
                """
                ひととおりレッスンを読んだら、次は「改変」が最短の学習です。

                ## おすすめの演習

                1. `LessonService` に新しいレッスンを 1 つ追加する
                2. レッスン詳細に「難易度」プロパティを追加して表示する
                3. カテゴリ別フィルタの UI を作る（クエリ文字列で絞り込み）
                4. Core に xUnit テストプロジェクトを追加し、`GetById` をテストする
                5. Minimal API のエンドポイントを追加し、JSON でレッスン一覧を返す

                ## 参考リンク

                - [.NET ドキュメント](https://learn.microsoft.com/dotnet/)
                - [C# ガイド](https://learn.microsoft.com/dotnet/csharp/)
                - [ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
                """,
            CodeSamples =
            [
                new CodeSample(
                    "よく使う CLI",
                    "bash",
                    """
                    dotnet new           # テンプレート一覧 / 作成
                    dotnet restore       # 依存関係の復元
                    dotnet build         # ビルド
                    dotnet run           # 実行
                    dotnet watch run     # 変更を監視して再起動
                    dotnet test          # テスト実行
                    """)
            ]
        }
    ];
}
