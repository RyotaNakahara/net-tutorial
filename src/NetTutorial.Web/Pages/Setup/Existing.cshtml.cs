using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Setup;

/// <summary>
/// 既存のソースコードを手元に用意し、ビルド・起動する手順書です。
/// </summary>
public class ExistingModel : PageModel
{
    public IReadOnlyList<SetupStep> Steps { get; } =
    [
        new(
            "1",
            "この手順でやること",
            "すでに誰かが作ったプロジェクト（この NetTutorial や社内アプリなど）を、自分の PC で動かす流れです。",
            [
                "ソースを手元にコピーする（Git clone など）",
                ".NET SDK を入れて `dotnet` が使えるようにする",
                "VS Code でソリューションを開く",
                "ビルドして Web プロジェクトを起動する",
                "ブラウザで画面を確認する"
            ],
            null,
            "新規に `dotnet new` で作る手順は「環境構築」ページを見てください。こちらは既存フォルダを使います。"),
        new(
            "2",
            "前提条件を確認する",
            "ソースを受け取る前に、次が揃っているか確認します。",
            [
                "OS: macOS / Windows / Linux",
                "インターネット接続（Git clone や SDK 取得に使う）",
                "ターミナル（macOS: Terminal / Windows: PowerShell など）",
                "ブラウザ",
                "Git（リポジトリから取得する場合）",
                "Visual Studio Code（次の手順でインストール可）"
            ],
            null,
            "ZIP で受け取っただけでも動かせます。Git がなくても手順 3 の「ZIP を展開」で進められます。"),
        new(
            "3",
            "既存ソースを手元に用意する",
            "プロジェクトの置き場所を決め、ソースをダウンロードまたは clone します。",
            [
                "Git リポジトリの URL をもらっている → clone",
                "ZIP や共有フォルダでもらった → 任意の場所に展開",
                "この NetTutorial なら、すでに手元にあるフォルダをそのまま使える"
            ],
            """
            # 例: Git で取得する場合
            mkdir -p ~/Projects
            cd ~/Projects
            git clone <リポジトリのURL> net-tutorial
            cd net-tutorial

            # 例: すでに手元にある場合（パスは環境に合わせて変更）
            cd ~/Documents/Work/net-tutorial
            ls
            # NetTutorial.sln と src/ があることを確認
            """,
            "社内プロジェクトでは VPN や SSH 鍵が必要なことがあります。clone できないときは担当者にアクセス権を確認してください。"),
        new(
            "4",
            ".NET 8 SDK を入れる・確認する",
            "既存プロジェクトが要求する SDK バージョンに合わせます。NetTutorial は .NET 8 です。",
            null,
            """
            # バージョン確認（8.0.x なら OK）
            dotnet --version

            # 入っていない場合（macOS / Linux の例）
            curl -fsSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
            bash dotnet-install.sh --channel 8.0
            echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.zshrc
            source ~/.zshrc

            # Windows は https://dotnet.microsoft.com/download/dotnet/8.0 から SDK をインストール
            """,
            "`command not found: dotnet` のときはターミナルを開き直すか、PATH に SDK が含まれるか確認してください。"),
        new(
            "5",
            "VS Code と拡張機能を用意する",
            "既存ソリューションを開いて編集・デバッグするためのエディタを入れます。",
            [
                "C# Dev Kit（ms-dotnettools.csdevkit）",
                "C#（ms-dotnettools.csharp）",
                ".NET Install Tool（ms-dotnettools.vscode-dotnet-runtime）"
            ],
            """
            # macOS（Homebrew がある場合）
            brew install --cask visual-studio-code

            code --install-extension ms-dotnettools.csdevkit
            code --install-extension ms-dotnettools.csharp
            code --install-extension ms-dotnettools.vscode-dotnet-runtime
            """,
            "`code` コマンドがないときは VS Code のコマンドパレット →「Shell Command: Install 'code' command in PATH」を実行します。"),
        new(
            "6",
            "プロジェクトを VS Code で開く",
            "エディタでソースを開きます。`.sln` があればそれを使いますが、なくても `.csproj` だけで開発できます。",
            [
                "`.sln` がある → リポジトリのルートを開く（複数プロジェクトを一覧しやすい）",
                "`.sln` がない → `src/NetTutorial.Web` など Web の `.csproj` があるフォルダを開く",
                "C# Dev Kit の読み込みが終わるまで少し待つ"
            ],
            """
            # .sln がある場合（NetTutorial の例）
            cd ~/Documents/Work/net-tutorial
            code .

            # .sln がない場合 — Web プロジェクトのフォルダを開く
            cd ~/Projects/社内アプリ/src/MyApp.Web
            code .
            """,
            "`.sln` は複数プロジェクトをまとめて扱うための入れ物です。ビルドや起動に必須ではありません。Web の `.csproj` に Core への参照があれば、そこからビルドできます。"),
        new(
            "7",
            "依存関係を復元してビルドする",
            "初回は NuGet パッケージの取得とコンパイルが必要です。`.sln` がなくても `.csproj` を指定すればビルドできます。",
            [
                "`.sln` がある → ルートで `dotnet build`（全体を一括ビルド）",
                "`.sln` がない → Web の `.csproj` を指定してビルド（参照先の Core も自動でビルドされる）"
            ],
            """
            # パターン A: .sln がある（ルートで一括）
            cd ~/Documents/Work/net-tutorial
            dotnet restore
            dotnet build

            # パターン B: .sln がない（.csproj を直接指定）
            cd ~/Projects/社内アプリ/src/MyApp.Web
            dotnet restore
            dotnet build

            # パスを明示する場合（どちらのパターンでも可）
            dotnet build src/NetTutorial.Web/NetTutorial.Web.csproj
            """,
            "Web の `.csproj` に `<ProjectReference>` で Core が書かれていれば、`dotnet build` 時に Core も先にビルドされます。`.sln` はなくても問題ありません。"),
        new(
            "8",
            "Web プロジェクトを起動する",
            "画面を出すのは Web プロジェクト側です。ルートではなく Web のフォルダで `dotnet run` します。",
            null,
            """
            # NetTutorial の例
            cd src/NetTutorial.Web
            dotnet run

            # launchSettings のプロファイルを指定する場合
            dotnet run --launch-profile http

            # コード変更を監視しながら起動
            dotnet watch run --launch-profile http
            """,
            "ターミナルに `Now listening on: http://localhost:5xxx` のような URL が表示されます。そのアドレスをブラウザで開きます。"),
        new(
            "9",
            "ブラウザで動作を確認する",
            "起動ログに出た URL を開き、トップページやナビが表示されれば成功です。",
            [
                "NetTutorial ならホーム（/）が表示される",
                "レッスン（/Lessons）や構成ガイド（/Structure）も開ける",
                "画面が真っ白・エラーなら手順 10 のトラブルシュートを参照"
            ],
            null,
            "ポートが使用中のときは、起動中の別ターミナルの `dotnet run` を Ctrl+C で止めるか、表示された別ポートの URL を使います。"),
        new(
            "10",
            "（任意）PostgreSQL が必要なプロジェクトの場合",
            "接続文字列や DB 名はプロジェクトごとに異なります。README や appsettings.json を確認してください。",
            [
                "PostgreSQL をインストール・起動する（ローカル DB を立てる）",
                "README や担当者の指示どおりに DB を作成する",
                "appsettings.Development.json に接続文字列を設定する",
                "マイグレーションがあれば `dotnet ef database update` を実行する",
                "新規作成ページの手順 13（`dotnet add package`）は不要 — パッケージは .csproj に既に書かれており、`dotnet restore` / `dotnet build` で取得済み"
            ],
            """
            # EF ツールが未導入の場合（マイグレーションを回すとき）
            dotnet tool install --global dotnet-ef
            export PATH="$PATH:$HOME/.dotnet/tools"

            # Web プロジェクトで（プロジェクトに EF が入っている場合）
            cd src/NetTutorial.Web
            dotnet ef database update
            """,
            "NetTutorial 本体は DB なしで動きます。社内アプリで DB が必須のときだけ、PC 側の PostgreSQL 準備と接続設定をします。NuGet パッケージの追加は既存ソースでは通常やりません。")
    ];

    public IReadOnlyList<TroubleItem> Troubleshooting { get; } =
    [
        new(
            "dotnet コマンドが見つからない",
            "手順 4 の SDK インストールと PATH 設定を確認し、ターミナルを開き直してください。"),
        new(
            "git clone できない",
            "リポジトリ URL・認証（SSH 鍵 / Personal Access Token）・VPN を確認します。ZIP でもらえるか担当者に聞いてください。"),
        new(
            "ビルドエラー（SDK バージョン）",
            "`dotnet --list-sdks` で要求バージョンが入っているか確認します。README や global.json の指定に合わせて SDK を追加インストールします。"),
        new(
            "プロジェクト参照エラー",
            "Web の `.csproj` に Core への `<ProjectReference>` があるか確認します。`.sln` がなくても、Web フォルダで `dotnet build` すれば参照先は自動でビルドされます。"),
        new(
            "ポートが使用中 (Address already in use)",
            "別の `dotnet run` を Ctrl+C で止めるか、`--urls http://127.0.0.1:5020` のように別ポートを指定します。"),
        new(
            "C# の補完が効かない",
            "VS Code で `.sln` のルート、または Web の `.csproj` があるフォルダを開き、C# Dev Kit の読み込み完了を待ってからウィンドウを再読み込みします。"),
        new(
            "画面は出るが DB エラーになる",
            "PostgreSQL の起動、接続文字列、DB 名・ユーザー・パスワードを appsettings と照合します。マイグレーション未実行なら `dotnet ef database update` を試します。")
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
