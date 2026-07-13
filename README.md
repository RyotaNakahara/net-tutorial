# NetTutorial

.NET / C# / ASP.NET Core の**基本構造**を、動く Web アプリとして学ぶための学習用プロジェクトです。

アプリ内の「レッスン」画面で、.NET の概要や C# の基本構文を閲覧できます。ソースコード自体も教材になるよう、コメントと役割分担を意識して構成しています。

## 構成

```
NetTutorial.sln
└── src/
    ├── NetTutorial.Core/     # クラスライブラリ（モデル・サービス）
    └── NetTutorial.Web/      # ASP.NET Core Razor Pages（UI）
```

| プロジェクト | 役割 |
| --- | --- |
| `NetTutorial.Core` | 学習コンテンツとドメインロジック |
| `NetTutorial.Web` | 画面表示、ルーティング、DI 設定 |

## 必要なもの

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

インストール確認:

```bash
dotnet --version
```

## 起動方法

```bash
cd src/NetTutorial.Web
dotnet run
```

表示された URL（例: `https://localhost:7xxx` または `http://localhost:5xxx`）をブラウザで開いてください。

コード変更を監視しながら起動する場合:

```bash
dotnet watch run
```

## 画面

| ページ | 内容 |
| --- | --- |
| `/` | ホーム（アプリの紹介） |
| `/Lessons` | レッスン一覧（カテゴリ絞り込み可） |
| `/Lessons/{id}` | レッスン詳細（構文説明・コード例） |
| `/Structure` | ソリューション構成ガイド |
| `/Demo` | Core の `Greeter` を呼び出すデモ |

## 学習の進め方

1. アプリを起動し、画面からレッスンを読む
2. 「構成ガイド」でフォルダ役割を確認する
3. `Program.cs` → `Pages` → `LessonService` の順にソースを読む
4. 「デモ」で Web → Core の呼び出しを体験する
5. 新しいレッスンやプロパティを追加してみる

## よく使うコマンド

```bash
dotnet build          # ビルド
dotnet run            # 実行
dotnet watch run      # 監視実行
dotnet new list       # テンプレート一覧
```
