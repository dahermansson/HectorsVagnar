using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HectorsVagnar.Pages;
public partial class Settings
{
    private const string SettingsKey = "MinaInstallningar";

    public Installningar MinaInstallningar { get; set; } = new();
    

    [Inject]
    private ILocalStorageService _localStarageService { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        MinaInstallningar = await _localStarageService.GetItemAsync<Installningar>(SettingsKey);
    }

    private void HandleSubmit(EditContext installningar)
    {
        if (installningar == null)
        {
                
        }
    }
}
