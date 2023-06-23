namespace Optimum.Api;

public class RequestPathParams : Dictionary<string, string?>
{
    public new string? this[string key]
    {
        get => this.GetValueOrDefault(key.ToLower().Replace("-", ""));
        set => this.Add(key.ToLower().Replace("-", ""), value);
    }

    public double? GetDouble(string key)
        => double.TryParse(this[key], out var boolValue) ? boolValue : null;
    public int? GetInt(string key)
        => int.TryParse(this[key], out var boolValue) ? boolValue : null;
    
    public decimal? GetDecimal(string key)
        => decimal.TryParse(this[key], out var boolValue) ? boolValue : null;

    public long? GetLong(string key)
        => long.TryParse(this[key], out var boolValue) ? boolValue : null;

    public bool? GetBoolean(string key)
        => bool.TryParse(this[key], out var boolValue) ? boolValue : null;

    public Guid GetGuid(string key)
        => Guid.TryParse(this[key], out var boolValue) ? boolValue : Guid.Empty;
}