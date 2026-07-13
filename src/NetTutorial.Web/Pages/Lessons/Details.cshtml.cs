using NetTutorial.Core.Models;
using NetTutorial.Core.Services;
using NetTutorial.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Lessons;

public class DetailsModel : PageModel
{
    private readonly ILessonService _lessonService;

    public DetailsModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public Lesson? Lesson { get; private set; }
    public string ContentHtml { get; private set; } = string.Empty;
    public Lesson? Previous { get; private set; }
    public Lesson? Next { get; private set; }

    public IActionResult OnGet(string id)
    {
        Lesson = _lessonService.GetById(id);
        if (Lesson is null)
        {
            return NotFound();
        }

        ContentHtml = MarkdownHelper.ToHtml(Lesson.ContentMarkdown);

        var all = _lessonService.GetAll();
        var index = all.ToList().FindIndex(l => l.Id == Lesson.Id);
        Previous = index > 0 ? all[index - 1] : null;
        Next = index >= 0 && index < all.Count - 1 ? all[index + 1] : null;

        return Page();
    }
}
