namespace NetTutorial.Core.Models;

/// <summary>
/// 1つの学習レッスンを表すモデルクラスです。
/// プロパティ（get; init;）は、オブジェクト初期化時に値を設定し、その後は変更しない想定です。
/// </summary>
public class Lesson
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required LessonCategory Category { get; init; }
    public required string ContentMarkdown { get; init; }
    public int Order { get; init; }
    public IReadOnlyList<CodeSample> CodeSamples { get; init; } = [];
    public IReadOnlyList<string> KeyPoints { get; init; } = [];
}
