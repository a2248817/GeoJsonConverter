namespace SvgSharp;
public class BaseAttribute
{
    public BaseAttribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
    public override string ToString()
    {
        return $"{Name}=\"{Value}\"";
    }

    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

