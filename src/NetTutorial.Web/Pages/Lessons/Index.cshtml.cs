using NetTutorial.Core.Models;
using NetTutorial.Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Lessons;

public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public IReadOnlyList<Lesson> Lessons { get; private set; } = [];
    public string? SelectedCategory { get; private set; }

    public void OnGet(string? category)
    {
        SelectedCategory = category;

        if (!string.IsNullOrWhiteSpace(category)
            && Enum.TryParse<LessonCategory>(category, ignoreCase: true, out var parsed))
        {
            Lessons = _lessonService.GetByCategory(parsed);
            return;
        }

        Lessons = _lessonService.GetAll();
    }

    public static string CategoryLabel(LessonCategory category) => category switch
    {
        LessonCategory.Overview => "概要",
        LessonCategory.Structure => "構成",
        LessonCategory.Syntax => "構文",
        LessonCategory.Framework => "フレームワーク",
        _ => category.ToString()
    };
}
