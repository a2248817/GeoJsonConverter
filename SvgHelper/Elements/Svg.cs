
namespace SvgSharp;
public class Svg : BaseElement
{
    public Svg() : base("svg")
    {
        Attributes.Add(new("version", "1.1"));
        Attributes.Add(new("xmlns", "http://www.w3.org/2000/svg"));
    }

    public void ToFile()
    {
        File.WriteAllText($"{Id}.svg", ToString());
    }
}
