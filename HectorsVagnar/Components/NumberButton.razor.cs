using Microsoft.AspNetCore.Components;

namespace HectorsVagnar.Components;

public partial class NumberButton
{
    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> Callback { get; set; }

    public async Task Clicked() => await Callback.InvokeAsync(Value);
}

