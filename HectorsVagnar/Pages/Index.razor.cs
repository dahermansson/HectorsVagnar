using Microsoft.AspNetCore.Components;
using System.Web;

namespace HectorsVagnar.Pages;

public partial class Index
{
    private List<string> Vagnar = new();
    private string CurrenInput { get; set; } = string.Empty;
    private string Epost { get; set; } = string.Empty;
    private string TagNr { get; set; } = string.Empty;
    [Inject]
    private ISettingsService _settingsService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Epost = (await _settingsService.Load()).Epost;
    }
    public void AddVagn()
    {
        if (CurrenInput != string.Empty && !Vagnar.Contains(CurrenInput))
        {
            Vagnar.Add(CurrenInput);
            CurrenInput = string.Empty;
        }
    }

    public void RemoveVagn(string vagn)
    {
        if(Vagnar.Contains(vagn))
            Vagnar.Remove(vagn);
    }
    private void ButtonClicked(string value)
    {
        CurrenInput += value;
    }

    private string GetVagnarString()
    {
        var grupper = Vagnar.Select((vagn, index) => new { vagn, index }).GroupBy(x => x.index / 5, i => i.vagn).ToList();
        return HttpUtility.UrlEncode(string.Join($"{Environment.NewLine}{Environment.NewLine}", grupper.Select(t => string.Join(Environment.NewLine, t))));
    }
}
