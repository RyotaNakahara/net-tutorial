namespace NetTutorial.Core.Models;

/// <summary>
/// レッスン内で表示するコード例です。
/// record は、データを保持する不変に近い型を簡潔に書くための構文です。
/// </summary>
public record CodeSample(string Title, string Language, string Code, string? Explanation = null);
