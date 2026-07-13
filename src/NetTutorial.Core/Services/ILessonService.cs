using NetTutorial.Core.Models;

namespace NetTutorial.Core.Services;

/// <summary>
/// レッスン取得の契約（インターフェース）です。
/// 実装クラスに依存せず「何ができるか」だけを定義します。DI で差し替えやすいのが利点です。
/// </summary>
public interface ILessonService
{
    IReadOnlyList<Lesson> GetAll();
    Lesson? GetById(string id);
    IReadOnlyList<Lesson> GetByCategory(LessonCategory category);
}
