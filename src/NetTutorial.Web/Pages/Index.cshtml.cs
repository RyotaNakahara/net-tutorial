using NetTutorial.Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages;

/// <summary>
/// ホームページの PageModel。
/// コンストラクタで ILessonService を受け取るのが DI の典型パターンです。
/// </summary>
public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public int LessonCount { get; private set; }

    public void OnGet()
    {
        LessonCount = _lessonService.GetAll().Count;
    }
}
