using Microsoft.AspNetCore.Components;
using System.Web;

namespace HectorsVagnar.Pages;

public partial class Index
{
    private readonly List<string> Vagnar = new();
    private string Lok = "";
    private string CurrenInput { get; set; } = string.Empty;
    private string EpostMallOlastat => string.Format("mailto:{0}?body={1}&subject=Lastat: {2}", Installningar?.Epost, GetMailFromMall(false), TagNr);
    private string EpostMallLastat => string.Format("mailto:{0}?body={1}&subject=Lastat: {2}", Installningar?.Epost, GetMailFromMall(true), TagNr);
    private string TagNr { get; set; } = string.Empty;
    private Installningar Installningar { get; set; } = null!;
    [Inject]
    private ISettingsService _settingsService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Installningar = await _settingsService.Load();

    }
    public void AddVagn()
    {
        if (CurrenInput != string.Empty && !Vagnar.Contains(CurrenInput))
        {
            Vagnar.Insert(0, CurrenInput);
            CurrenInput = string.Empty;
        }
    }
    private void AddLok()
    {
        Lok = CurrenInput;
        CurrenInput = string.Empty;
    }

    public async Task RemoveVagn(string vagn)
    {
        if (!await JSRuntime.InvokeAsync<bool>("confirm", new[] { "Är du säker?"}))
            await Task.CompletedTask;
        if(Vagnar.Contains(vagn))
            Vagnar.Remove(vagn);
    }
    private void ButtonClicked(string value)
    {
        if (value == "C" && CurrenInput.Length == 0)
        { }
        else if (value == "C" && CurrenInput.Length > 0)
            CurrenInput = CurrenInput.Substring(0, CurrenInput.Length - 1);
        else
        {
            CurrenInput += value;
            if (CurrenInput.Length == 4 && !(CurrenInput.Contains('T') || CurrenInput.Contains('.')))
                AddVagn();
            if (CurrenInput.Length == 7 && (CurrenInput.Contains('T') || CurrenInput.Contains('.')))
                AddLok();
        }
    }

    private string GetMailFromMall(bool lastat)
    {
        var mall = Installningar?.MailMall ?? string.Empty;
        var mail = mall.Replace("{lastatEllerEj}", lastat ? "Lastade vagnar" : "Tomma vagnar").Replace("{lok}", Lok).Replace("{vagnar}", GetVagnarString());
        return HttpUtility.UrlEncode(mail.Replace("\n", Environment.NewLine)).Replace("+", HttpUtility.HtmlEncode(" "));
    }
    private string GetVagnarString()
    {
        var grupper = Vagnar.Select((vagn, index) => new { vagn, index }).GroupBy(x => x.index / 5, i => i.vagn).ToList();
        return string.Join($"\n\n", grupper.Select(t => string.Join("\n", t)));
    }
}
