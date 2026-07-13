namespace NetTutorial.Core.Demos;

/// <summary>
/// クラスライブラリから呼び出すデモ用のクラスです。
/// Web プロジェクト → Core プロジェクト参照の流れを理解するための簡単な例です。
/// </summary>
public class Greeter
{
    private readonly string _name;

    // コンストラクタ: インスタンス生成時に一度だけ呼ばれ、必須データを受け取ります。
    public Greeter(string name)
    {
        // null や空白を防ぐためのガード句です。
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        _name = name.Trim();
    }

    // メソッド: オブジェクトの振る舞いを定義します。
    public string SayHello() => $"こんにちは、{_name} さん！ .NET の学習を始めましょう。";

    public string Describe(int lessonCount) =>
        $"{_name} さん向けのレッスンは現在 {lessonCount} 件あります。";
}
