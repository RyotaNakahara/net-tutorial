using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetTutorial.Web.Pages.Architecture;

public class IndexModel : PageModel
{
    public IReadOnlyList<SimpleIdea> CoreIdeas { get; } =
    [
        new("困りごと", "画面が100枚あると、Pages フォルダが巨大な引き出し1つになる。注文画面を直したいのに、どこにあるか探し回る。"),
        new("考え方", "技術の種類（画面 / サービス / DB）ではなく、「何の機能か」でフォルダを分ける。注文は注文、顧客は顧客。"),
        new("たとえ", "会社の部署分けと同じ。総務と営業を同じ机に混ぜず、部署ごとに部屋を分ける。")
    ];

    public IReadOnlyList<FolderItem> FolderItems { get; } =
    [
        new("Modules/注文/", "「注文」という業務の部屋。画面もルールもDB操作も、ここに寄せる。"),
        new("その中の Web/", "注文の画面だけ（一覧・詳細・キャンセルなど）。"),
        new("その中の Domain/", "注文のルール。「この状態ではキャンセルできない」などの決まりごと。"),
        new("その中の Application/", "「注文を作る」「一覧を取る」といった作業の手順。"),
        new("その中の Infrastructure/", "実際のDBや外部サービスへのつなぎ込み。"),
        new("BuildingBlocks/", "どの部署でも使う文房具置き場（ログイン共通部品など）。置きすぎ注意。"),
        new("Host/", "玄関と受付。アプリ起動・ルーティング・全部署のつなぎ合わせ。"),
        new("tests/", "各部署の動作確認。注文のテストは注文の近くに置く。")
    ];

    public IReadOnlyList<CompareRow> CompareRows { get; } =
    [
        new("探しやすさ", "「画面フォルダ」を全部漁る", "「注文フォルダ」だけ見ればよい"),
        new("直しやすさ", "関係ないファイルまで触りがち", "影響が注文まわりに収まりやすい"),
        new("分担しやすさ", "同じファイルで衝突しやすい", "部署（機能）ごとに担当できる"),
        new("テスト", "後回しになりがち", "機能ごとに小さく確認できる")
    ];

    public IReadOnlyList<GrowthStep> GrowthSteps { get; } =
    [
        new(1, "まず画面を機能フォルダへ", "Pages の直下に全部置かず、Orders / Customers のように分ける。これが最初の一歩。"),
        new(2, "テストを足す", "tests フォルダを作り、機能ごとに確認できるようにする。"),
        new(3, "機能ごとにもう一段階まとめる", "画面・ルール・DB を Modules/注文 のような部屋にまとめる。"),
        new(4, "他の機能を直接いじらない", "注文から顧客の中身を勝手に触らず、決まった窓口（公開API）経由にする。")
    ];

    public IReadOnlyList<Pitfall> Pitfalls { get; } =
    [
        new("最初からサービスをバラバラに", "マイクロサービスは「別会社にする」ようなもの。部署の境目が曖昧なまま分割すると、かえって複雑になる。"),
        new("なんでも Utils に入れる", "「とりあえず共通」が増えると、結局どこにあるかわからなくなる。"),
        new("全部やる巨大サービス1本", "全画面から呼ばれる神クラスは、フォルダ分けの意味をなくす。"),
        new("部署同士が勝手に中を触る", "境界を破ると、また巨大な1フォルダに戻ったのと同じになる。")
    ];

    public IReadOnlyList<TermItem> Terms { get; } =
    [
        new("機能モジュール分割", "業務のまとまり（注文・顧客など）ごとにコードを分けること。"),
        new("Modular Monolith（モジュラーモノリス）", "アプリは1つ（起動も1つ）のまま、中だけ部署分けすること。"),
        new("Vertical Slice（垂直スライス）", "画面→処理→DB を横にバラバラにせず、1機能の縦の流れでまとめること。"),
        new("レイヤー分割", "Domain / Application / Web のように技術の層で分けること。機能分割の内側で使うと効く。")
    ];

    public record SimpleIdea(string Title, string Body);
    public record FolderItem(string Path, string Description);
    public record CompareRow(string Aspect, string LayerOnly, string Modular);
    public record GrowthStep(int Number, string Title, string Description);
    public record Pitfall(string Title, string Description);
    public record TermItem(string Name, string Meaning);
}
