
using System.Text;

namespace SvgSharp;
public class BaseElement : IElement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<BaseElement> Elements { get; set; } = new();
    public List<BaseAttribute> Attributes { get; set; } = new();

    public string this[string name]
    {
        get => Attributes.Find(a => a.Name == name).Value;
        set => SetAttribute(name, value);
    }

    public BaseElement this[Guid Id]
    {
        get => Elements.Find(e => e.Id == Id);
    }


    public string TagName { get; init; } = string.Empty;

    public BaseElement(string tagName)
    {
        TagName = tagName;
    }

    public IElement AddElement(BaseElement addedElement)
    {
        Elements.Add(addedElement);
        return this;
    }

    public IElement RemoveElement(Guid id)
    {
        var removedElement = Elements.FirstOrDefault(x => x.Id == id);
        if (removedElement is not null)
        {
            Elements.Remove(removedElement);
        }
        return this;
    }

    public IElement AddAttribute(string name, string value)
    {
        Attributes.Add(new(name, value));
        return this;
    }
    public IElement RemoveAttribute(string name)
    {
        var attribute = Attributes.FirstOrDefault(x => x.Name == name);
        if (attribute is not null)
        {
            Attributes.Remove(attribute);
        }
        return this;
    }

    public IElement SetAttribute(string name, string value)
    {
        var attribute = Attributes.FirstOrDefault(a => a.Name == name);
        if (attribute is not null) attribute.Value = value;
        else AddAttribute(name, value);
        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        var openingTag = $"<{TagName}";
        sb.Append(openingTag);

        foreach (var attribute in Attributes)
        {
            sb.Append($" {attribute}");
        }

        sb.AppendLine(">");

        foreach (var element in Elements)
        {
            sb.Append(element.ToString());
        }

        var closingTag = $"</{TagName}>";
        sb.AppendLine(closingTag);

        return sb.ToString();
    }
}
