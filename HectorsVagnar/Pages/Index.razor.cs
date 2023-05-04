using Microsoft.AspNetCore.Components;
using System.Web;

namespace HectorsVagnar.Pages;

public partial class Index
{
    private List<string> Vagnar = new();
    private string Lok = "";
    private string CurrenInput { get; set; } = string.Empty;
    private string Epostadress { get; set; } = string.Empty;
    private string EpostMall => String.Format("mailto:{0}?body={1}&subject=Lastat: {2}", Epostadress, GetMailFromMall(true), TagNr);
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
            Vagnar.Add(CurrenInput);
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
            return;
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
            if ((CurrenInput.Length == 7 && (CurrenInput.Contains('T') || CurrenInput.Contains('.'))))
                AddLok();
        }
    }

    private string GetMailFromMall(bool lastat)
    {
        var mall = Installningar?.MailMall ?? string.Empty;
        var mail = mall.Replace("{lastatEllerEj}", lastat ? "Lastade vagnar" : "Tomma vagnar").Replace("{Lok}", Lok).Replace("{Vagnar}", GetVagnarString());
        return mail;
    }
    private string GetVagnarString()
    {
        var grupper = Vagnar.Select((vagn, index) => new { vagn, index }).GroupBy(x => x.index / 5, i => i.vagn).ToList();
        return HttpUtility.UrlEncode(string.Join($"{Environment.NewLine}{Environment.NewLine}", grupper.Select(t => string.Join(Environment.NewLine, t))));
    }
}
