namespace Optimum.Api.Attributes;


public class GetRequestAttribute : Attribute
{
    public GetRequestAttribute(string pattern)
    {
        this.pattern = pattern;
    }

    public string pattern { get; set; }
}