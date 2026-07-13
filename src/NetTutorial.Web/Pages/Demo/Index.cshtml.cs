using NetTutorial.Core.Demos;
using NetTutorial.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Demo;

/// <summary>
/// Core プロジェクトのクラスを Web から呼び出すデモページです。
/// </summary>
public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [BindProperty]
    public string Name { get; set; } = "学習者";

    public string? Greeting { get; private set; }
    public string? Description { get; private set; }
    public bool Submitted { get; private set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        Submitted = true;
        var displayName = string.IsNullOrWhiteSpace(Name) ? "学習者" : Name.Trim();

        // Core プロジェクトのクラスを実体化して利用
        var greeter = new Greeter(displayName);
        Greeting = greeter.SayHello();
        Description = greeter.Describe(_lessonService.GetAll().Count);
    }
}
