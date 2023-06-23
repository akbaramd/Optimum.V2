namespace Optimum.Api.Attributes;


public class PostRequestAttribute : Attribute
{
    public PostRequestAttribute(string pattern)
    {
        this.pattern = pattern;
    }

    public string pattern { get; set; }
}