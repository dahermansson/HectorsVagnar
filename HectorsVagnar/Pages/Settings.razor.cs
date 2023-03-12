using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HectorsVagnar.Pages;
public partial class Settings
{
    public Installningar MinaInstallningar { get; set; } = new Installningar();

    [Inject]
    private ISettingsService _settingsService { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        MinaInstallningar = await _settingsService.Load();
    }

    private async void HandleSubmit()
    {
        if (MinaInstallningar != null)
        {
            await _settingsService.Save(MinaInstallningar);
        }
    }
}
