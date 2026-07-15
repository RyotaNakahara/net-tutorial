using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Setup;

/// <summary>
/// ローカル環境構築の手順書です。
/// この NetTutorial リポジトリは使わず、dotnet new で新規プロジェクトを作る前提です。
/// </summary>
public class IndexModel : PageModel
{
    public IReadOnlyList<SetupStep> Steps { get; } =
    [
        new(
            "1",
            "前提条件を確認する",
            "まだ開発環境が入っていない PC を想定します。次を使えるようにします（未導入なら以降の手順で入れます）。",
            [
                "OS: macOS / Windows / Linux のいずれか",
                "インターネット接続",
                "ターミナル（macOS: Terminal / Windows: PowerShell や Windows Terminal）",
                "ブラウザ（Chrome / Edge / Safari など）",
                "Visual Studio Code（次の手順でインストール）",
                "PostgreSQL（DB 連携する場合。手順 11 以降）"
            ],
            null,
            "ここでのゴールは「空の PC から、自分で作った .NET Web アプリをブラウザで開く」ことです。この NetTutorial フォルダは使いません。"),
        new(
            "2",
            "VS Code とおすすめプラグインをインストールする",
            "コード編集・デバッグ用のエディタを用意します。",
            [
                "C# Dev Kit（Microsoft / ID: ms-dotnettools.csdevkit）… ソリューション表示、デバッグ、テスト支援",
                "C#（Microsoft / ID: ms-dotnettools.csharp）… IntelliSense・構文チェック（通常は C# Dev Kit と一緒に入ります）",
                ".NET Install Tool（Microsoft / ID: ms-dotnettools.vscode-dotnet-runtime）… SDK / ランタイム補助",
                "IntelliCode for C# Dev Kit（Microsoft / ID: ms-dotnettools.vscodeintellicode-csharp）… AI 補完（任意）"
            ],
            """
            # --- VS Code 本体 ---
            # https://code.visualstudio.com/ からインストール

            # macOS（Homebrew がある場合）
            brew install --cask visual-studio-code

            # --- おすすめ拡張機能 ---
            code --install-extension ms-dotnettools.csdevkit
            code --install-extension ms-dotnettools.csharp
            code --install-extension ms-dotnettools.vscode-dotnet-runtime
            # 任意
            code --install-extension ms-dotnettools.vscodeintellicode-csharp
            """,
            "`code` が使えないときは、VS Code のコマンドパレット（macOS: Cmd+Shift+P / Windows: Ctrl+Shift+P）→「Shell Command: Install 'code' command in PATH」を実行します。"),
        new(
            "3",
            ".NET 8 SDK をインストールする",
            "新規プロジェクト作成・ビルド・実行に必要な SDK を入れます（Runtime のみでは不可）。",
            null,
            """
            # --- macOS / Linux（公式スクリプト）---
            curl -fsSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
            bash dotnet-install.sh --channel 8.0

            echo 'export DOTNET_ROOT="$HOME/.dotnet"' >> ~/.zshrc
            echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.zshrc
            source ~/.zshrc

            # --- macOS（Homebrew）---
            brew install --cask dotnet-sdk

            # --- Windows ---
            # https://dotnet.microsoft.com/download/dotnet/8.0
            # 「SDK 8.0.x」をインストール
            """,
            "インストール後はターミナルを開き直すと確実です。"),
        new(
            "4",
            "dotnet コマンドを確認する",
            "新しいターミナルで SDK が使えるか確認します。",
            null,
            """
            dotnet --version
            # 例: 8.0.422 （8.0.x なら OK）

            dotnet --list-sdks
            """,
            "`command not found: dotnet` なら手順 3 の PATH / インストールを見直してください。"),
        new(
            "5",
            "新規 Web アプリを作る",
            "作業用の空フォルダで、テンプレートから ASP.NET Core Web アプリを新規作成します。",
            null,
            """
            # ホームなど、好きな場所に作業用フォルダを作る
            mkdir ~/Projects
            cd ~/Projects

            # Razor Pages の Web アプリを新規作成（プロジェクト名は任意）
            dotnet new webapp -n HelloDotNet -o HelloDotNet
            cd HelloDotNet
            """,
            "`dotnet new webapp` は最小構成です。NetTutorial のように Web + Core に分けた構成を CLI で作る場合は、手順 6 へ進んでください。"),
        new(
            "6",
            "（任意・発展）CLI で NetTutorial 型の構成を作る",
            "`dotnet new webapp` 1 本ではなく、ソリューション + Web プロジェクト + クラスライブラリの 3 層構成を CLI だけで作ります。NetTutorial と同じ「骨組み」まで自動化できます。",
            [
                "MyApp.sln … ソリューション（全体の入れ物）",
                "src/MyApp.Web … Razor Pages の Web アプリ（画面・Program.cs）",
                "src/MyApp.Core … クラスライブラリ（Models / Services など再利用ロジック）",
                "Web → Core のプロジェクト参照で、画面から Core のクラスを呼び出せる"
            ],
            """
            # 作業用ディレクトリへ（手順 5 で HelloDotNet を作った場合は別名にする）
            mkdir -p ~/Projects
            cd ~/Projects

            # 1) ソリューション作成
            dotnet new sln -n MyApp

            # 2) Web / Core プロジェクト作成
            dotnet new webapp -n MyApp.Web -o src/MyApp.Web
            dotnet new classlib -n MyApp.Core -o src/MyApp.Core

            # 3) ソリューションへ登録
            dotnet sln add src/MyApp.Web/MyApp.Web.csproj
            dotnet sln add src/MyApp.Core/MyApp.Core.csproj

            # 4) Web から Core を参照（NetTutorial と同じ関係）
            dotnet add src/MyApp.Web/MyApp.Web.csproj reference src/MyApp.Core/MyApp.Core.csproj

            # 5) Core 側のフォルダ構成（CLI では mkdir）
            mkdir -p src/MyApp.Core/Models
            mkdir -p src/MyApp.Core/Services

            # 6) テンプレート既定の Class1.cs を削除
            rm src/MyApp.Core/Class1.cs

            # 7) ルート（MyApp.sln がある場所）でビルド確認
            dotnet build
            """,
            """
            完成イメージ:
            MyApp/
            ├── MyApp.sln
            └── src/
                ├── MyApp.Core/
                │   ├── Models/
                │   └── Services/
                └── MyApp.Web/
                    ├── Pages/
                    ├── Program.cs
                    └── wwwroot/

            Pages の追加・DI 登録・デザイン・DB 連携は CLI では作れません。NetTutorial のレッスン画面のような中身は、この骨組みの上に手動で足していきます。
            """),
        new(
            "7",
            "VS Code で開く",
            "作ったプロジェクトを VS Code で開きます。",
            null,
            """
            # 最小構成（手順 5）の場合
            cd ~/Projects/HelloDotNet
            code .

            # NetTutorial 型（手順 6）の場合 — ソリューションのルートを開く
            cd ~/Projects/MyApp
            code .
            """,
            "手順 6 の場合は `.sln` があるフォルダを開いてください。C# Dev Kit がソリューションを検出したら読み込みます。"),
        new(
            "8",
            "ビルドして起動する",
            "コンパイルできることを確認し、Web プロジェクトを起動します。",
            null,
            """
            # --- 最小構成（HelloDotNet）---
            cd ~/Projects/HelloDotNet
            dotnet build
            dotnet run

            # --- NetTutorial 型（MyApp）---
            cd ~/Projects/MyApp
            dotnet build                              # ルート（.sln）から全体ビルド
            cd src/MyApp.Web
            dotnet run --launch-profile http

            # 変更を監視して再起動
            dotnet watch run --launch-profile http
            """,
            "NetTutorial 型では `dotnet run` は Web プロジェクト（src/MyApp.Web）で実行します。ルートの `dotnet build` はソリューション全体をビルドします。"),
        new(
            "9",
            "ブラウザで初期画面を確認する",
            "ターミナルに出た URL をブラウザで開きます。テンプレート標準のホームが表示されれば環境構築は成功です。",
            [
                "上部にナビ（Home / Privacy など）のあるシンプルなページ",
                "ホーム中央付近に「Welcome」見出しと説明文（ASP.NET Core テンプレートの初期画面）",
                "フッターに © と Privacy へのリンク"
            ],
            null,
            "手順 5 でも 6 でも、Web テンプレートの初期画面は同じです。NetTutorial 型は中身（Pages / Services）を足していく段階からが本番開発です。"),
        new(
            "10",
            "（任意）Hello World だけにする",
            "初期画面の代わりに、最小の Hello World だけ出したい場合の例です。",
            [
                "Pages/Index.cshtml を開き、中身を次の短い HTML に置き換える",
                "保存してから、起動中ならブラウザを再読み込み（または `dotnet run` し直し）",
                "「Hello, World!」とだけ出れば OK"
            ],
            """
            @page
            @model IndexModel
            @{
                ViewData["Title"] = "Home";
            }

            <h1>Hello, World!</h1>
            <p>.NET の環境構築に成功しました。</p>
            """,
            "レイアウト（ナビやフッター）はそのまま残ることがあります。それも消したい場合は Shared/_Layout.cshtml を簡略化してください。"),
        new(
            "11",
            "PostgreSQL をインストールする",
            ".NET アプリから使うデータベースとして PostgreSQL を入れます（DB 連携が不要ならスキップ可）。",
            [
                "macOS（Homebrew）: `brew install postgresql@16` → `brew services start postgresql@16`",
                "Windows: https://www.postgresql.org/download/windows/ のインストーラ（インストール時にパスワードを控える）",
                "Linux（例: Ubuntu）: `sudo apt install postgresql postgresql-contrib` のあとサービス起動"
            ],
            """
            # インストール後、バージョン確認（パスは環境により異なる）
            psql --version

            # macOS（Homebrew）でクライアントに PATH を通す例
            echo 'export PATH="$(brew --prefix postgresql@16)/bin:$PATH"' >> ~/.zshrc
            source ~/.zshrc
            """,
            "GUI が欲しい場合は pgAdmin や TablePlus、VS Code 拡張「PostgreSQL」（Microsoft / ID: ms-ossdata.vscode-postgresql）も便利です。"),
        new(
            "12",
            "データベースを作成する",
            "アプリ専用の DB を用意します。名前やユーザーは任意ですが、以降の接続文字列と揃えてください。",
            null,
            """
            # 対話シェルを開く（macOS では現在の OS ユーザーで入れることが多い）
            psql postgres

            # psql 内で実行
            CREATE DATABASE hellodotnet;
            \\l                  -- DB 一覧で hellodotnet があるか確認
            \\q                  -- 終了

            # 接続確認
            psql -d hellodotnet -c "SELECT version();"
            """,
            "Windows ではインストール時に決めた `postgres` ユーザー／パスワードで `psql -U postgres` とします。認証エラー時はパスワードや `pg_hba.conf` を確認してください。"),
        new(
            "13",
            ".NET に PostgreSQL 用パッケージを入れる",
            "Web プロジェクトで Entity Framework Core 経由で PostgreSQL につなぎます。これは「新規に自分で作ったプロジェクト」向けです。",
            [
                "既存のソースを動かすだけなら、この手順は通常不要です",
                "既存プロジェクトではパッケージは .csproj に既に書かれており、`dotnet restore` / `dotnet build` で取得されます",
                "既存ソースの手順は「既存ソースの環境構築」ページを参照してください"
            ],
            """
            # --- 最小構成 ---
            cd ~/Projects/HelloDotNet

            # --- NetTutorial 型 ---
            cd ~/Projects/MyApp/src/MyApp.Web

            # EF Core の PostgreSQL プロバイダ（.csproj がある Web プロジェクトで実行）
            dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

            # マイグレーション用ツール（初回のみグローバル）
            dotnet tool install --global dotnet-ef
            """,
            """
            既存リポジトリを手元で動かす場合は、パッケージ追加ではなく「PostgreSQL のインストール・DB 作成・接続文字列」が必要なことがあります（既存ソース手順の手順 10）。

            dotnet ef が見つからない場合は、シェルを開き直すか次を実行してください:
            export PATH="$PATH:$HOME/.dotnet/tools"
            """),
        new(
            "14",
            "接続文字列と DbContext を設定する",
            "`appsettings.json` に接続情報を書き、Program.cs で DI 登録します。",
            [
                "appsettings.json に ConnectionStrings を追加する（Web プロジェクト側）",
                "Models/AppDbContext.cs を作成する（最小構成は Web/Models、NetTutorial 型は Core/Models でも可）",
                "Program.cs で AddDbContext する"
            ],
            """
            // --- appsettings.json（抜粋）---
            {
              "ConnectionStrings": {
                "DefaultConnection": "Host=localhost;Port=5432;Database=hellodotnet;Username=YOUR_USER;Password=YOUR_PASSWORD"
              }
            }

            // --- Models/AppDbContext.cs ---
            using Microsoft.EntityFrameworkCore;

            public class AppDbContext : DbContext
            {
                public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options)
                {
                }

                // 例: public DbSet<TodoItem> TodoItems => Set<TodoItem>();
            }

            // --- Program.cs（builder.Services 付近に追加）---
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
            """,
            "Username は macOS では OS のログイン名、Windows では多くの場合 `postgres` です。パスワードが無い構成なら `Password=` を省略できることがあります。接続文字列にパスワードを書く場合、本番では環境変数やシークレットに退避してください。"),
        new(
            "15",
            "接続できるか確認する",
            "起動時に DB へ一度つなぐ処理を入れて、エラーが出ないか確認します。",
            [
                "Program.cs の `var app = builder.Build();` の直後などに、スコープを取って Database.CanConnect() を呼ぶ（下の例）",
                "`dotnet run` で起動し、ターミナルに「PostgreSQL OK」などと出れば接続成功",
                "失敗したら接続文字列・PostgreSQL サービス起動・ファイアウォールを確認"
            ],
            """
            // Program.cs（app 作成直後の例）
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (db.Database.CanConnect())
                {
                    Console.WriteLine("PostgreSQL OK: 接続できました");
                }
                else
                {
                    Console.WriteLine("PostgreSQL NG: 接続できません");
                }
            }

            // テーブルをマイグレーションで作る場合の例（モデル追加後）
            // dotnet add package Microsoft.EntityFrameworkCore.Design
            // dotnet ef migrations add InitialCreate
            // dotnet ef database update
            """,
            "ここまでできれば「.NET ↔ PostgreSQL」の基本連携は完了です。CRUD やマイグレーションは、このあとの学習ステップとして拡張できます。")
    ];

    public IReadOnlyList<TroubleItem> Troubleshooting { get; } =
    [
        new(
            "dotnet コマンドが見つからない",
            "ターミナルを開き直し、PATH に SDK が含まれるか確認します。手順 3 をやり直してください。"),
        new(
            "VS Code で `code` コマンドが使えない",
            "コマンドパレット →「Shell Command: Install 'code' command in PATH」を実行します。"),
        new(
            "C# の補完やエラー表示が出ない",
            "拡張機能「C# Dev Kit」の導入と、プロジェクトフォルダを開けているかを確認し、必要ならウィンドウを再読み込みします。"),
        new(
            "ポートが使用中 (Address already in use)",
            "別の `dotnet run` が残っていないか確認し、Ctrl+C で止めるか別ポートを指定します。"),
        new(
            "ビルドや起動に失敗する",
            "最小構成なら `.csproj` があるフォルダ、NetTutorial 型なら `dotnet build` は `.sln` があるルート、`dotnet run` は `src/MyApp.Web` で実行しているか確認します。"),
        new(
            "プロジェクト参照エラー（Core が見つからない）",
            "手順 6 の `dotnet add ... reference ...` を実行したか、VS Code で `.sln` を開いているか確認します。Core にクラスを追加したら `using MyApp.Core.Models;` など名前空間を合わせます。"),
        new(
            "psql コマンドが見つからない",
            "PostgreSQL の bin に PATH が通っているか確認します。macOS（Homebrew）なら `brew --prefix postgresql@16`/bin を PATH に追加してください。"),
        new(
            "PostgreSQL に接続できない",
            "サービスが起動しているか（`brew services list` や Windows のサービス）、Host/Port/Database/Username/Password が正しいか、`psql -d hellodotnet` 単体でつながるかを確認します。"),
        new(
            "UseNpgsql / AppDbContext が見つからない",
            "`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL` を実行したか、`using Microsoft.EntityFrameworkCore;` と名前空間を確認し、ビルドし直してください。")
    ];

    public record SetupStep(
        string Number,
        string Title,
        string Summary,
        IReadOnlyList<string>? Bullets,
        string? Code,
        string? Note);

    public record TroubleItem(string Title, string Detail);
}
