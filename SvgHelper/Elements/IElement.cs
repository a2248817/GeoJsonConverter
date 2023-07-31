
namespace SvgSharp;

public interface IElement
{
    public IElement AddAttribute(string name, string value);
    public IElement RemoveAttribute(string name);
    public IElement SetAttribute(string name, string value);
    public IElement AddElement(BaseElement addedElement);
    public IElement RemoveElement(Guid id);

}
