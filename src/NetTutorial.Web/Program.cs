using NetTutorial.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// --- サービス登録（DI コンテナ）---
// AddRazorPages: Razor Pages を使えるようにする
builder.Services.AddRazorPages();

// ILessonService の実装として LessonService を登録
// Singleton = アプリ起動中ずっと同じインスタンスを使う
builder.Services.AddSingleton<ILessonService, LessonService>();

var app = builder.Build();

// --- HTTP リクエストパイプライン ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTP → HTTPS
app.UseStaticFiles();       // wwwroot の静的ファイル配信
app.UseRouting();           // ルーティング
app.UseAuthorization();     // 認可（必要時）

app.MapRazorPages();        // Pages フォルダのページを URL に割り当て

app.Run();
