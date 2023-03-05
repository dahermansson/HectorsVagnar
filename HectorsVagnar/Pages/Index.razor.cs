namespace HectorsVagnar.Pages;

public partial class Index
{
    private List<string> Vagnar = new();
    private string CurrenInput { get; set; } = string.Empty;
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
}
