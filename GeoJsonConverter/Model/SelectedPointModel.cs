using GeoJSON.Text.Feature;

namespace GeoJsonConverter.Model;

public class SelectedPointModel
{
    private Feature _point;
    public Feature Point { get { return _point; } }

    public string Name { get; set; }
    public int Id { get; set; }
    public string Category { get; set; }

    public SelectedPointModel(Feature selectedPoint)
    {
        _point = selectedPoint;
        Name = selectedPoint.Id.ToString()[..8];
        Category = _point.Geometry.Type.ToString();
    }
}
