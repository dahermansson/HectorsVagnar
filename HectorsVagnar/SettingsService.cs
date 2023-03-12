using Blazored.LocalStorage;

namespace HectorsVagnar;

public interface ISettingsService
{
    Task Save(Installningar installningar);
    Task<Installningar> Load();
}
public class SettingsService : ISettingsService
{
    private readonly ILocalStorageService _localStorageService;
    private const string SettingsKey = "Installningar";
    public SettingsService(ILocalStorageService localStorageService)
    {
        _localStorageService= localStorageService;
    }
    public async Task<Installningar> Load()
    {
        return await _localStorageService.GetItemAsync<Installningar>(SettingsKey) ?? new Installningar();
    }

    public async Task Save(Installningar installningar)
    {
        await _localStorageService.SetItemAsync<Installningar>(SettingsKey, installningar);
    }
}
