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
                "Visual Studio Code（次の手順でインストール）"
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
            "`dotnet new webapp` は公式テンプレートです。後でブラウザに表示されるのは、このテンプレート標準の初期画面です。"),
        new(
            "6",
            "VS Code で開く",
            "いま作ったプロジェクトフォルダを VS Code で開きます。",
            null,
            """
            # HelloDotNet フォルダにいる状態で
            code .

            # または VS Code の「ファイル」→「フォルダーを開く…」で HelloDotNet を選択
            """,
            "C# Dev Kit の通知が出たら、ソリューション / プロジェクトの読み込みに従ってください。"),
        new(
            "7",
            "ビルドして起動する",
            "コンパイルできることを確認し、ローカルサーバーを起動します。",
            null,
            """
            # VS Code のターミナルで（プロジェクト直下）
            dotnet build
            dotnet run
            """,
            "成功すると `Now listening on: http://localhost:5xxx`（または https）のような URL が表示されます。止めるときは Ctrl+C です。"),
        new(
            "8",
            "ブラウザで初期画面を確認する",
            "ターミナルに出た URL をブラウザで開きます。テンプレート標準のホームが表示されれば環境構築は成功です。",
            [
                "上部にナビ（Home / Privacy など）のあるシンプルなページ",
                "ホーム中央付近に「Welcome」見出しと説明文（ASP.NET Core テンプレートの初期画面）",
                "フッターに © と Privacy へのリンク"
            ],
            null,
            "これが .NET（ASP.NET Core Web アプリ）の初期画面です。見えたら SDK・CLI・実行環境は一通り揃っています。"),
        new(
            "9",
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
            "レイアウト（ナビやフッター）はそのまま残ることがあります。それも消したい場合は Shared/_Layout.cshtml を簡略化してください。")
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
            "作業ディレクトリが `HelloDotNet`（.csproj がある場所）か確認し、`dotnet build` のエラーメッセージを読んで対処します。")
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
