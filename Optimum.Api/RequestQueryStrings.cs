namespace Optimum.Api;

public class RequestQueryStrings : Dictionary<string, string?>
{
    public new string? this[string key]
    {
        get => this.GetValueOrDefault(key.ToLower().Replace("-",""));
        set => this.Add(key.ToLower().Replace("-",""),value);
    }
    
    
}