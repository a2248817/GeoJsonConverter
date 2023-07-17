using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using System.Text.Json;

namespace GeoJsonHelper.Utilities;

public class GeoJsonHelper
{
    public static Feature CreatePoint(int x, int y)
    {
        var feature = new Feature();

        //latitude 緯度 Y, longitude 經度 X 
        var position = new Position(y, x);

        var point = new Point(position);
        feature.Geometry = point;
        feature.Id = $"{Guid.NewGuid()}";

        var properties = new Dictionary<string, object>();
        properties.Add("guid", feature.Id);
        properties.Add("x", x);
        properties.Add("y", y);
        feature.Properties = properties;

        return feature;
    }
    public static FeatureCollection CreatePoints(int x, int y)
    {
        var result = new FeatureCollection();

        //再往上建立Y方向點
        for (int i = 0; i < y; i++)
        {
            //先往右建立X方向點
            for (int j = 0; j < x; j++)
            {
                var feature = GeoJsonHelper.CreatePoint(j, i);
                result.Features.Add(feature);
            }
        }

        return result;
    }

    public static Feature CreateLineString(IEnumerable<Feature> points)
    {
        var feature = new Feature();

        var positions = new List<IPosition>();
        var guids = new List<string>();
        foreach (var point in points)
        {
            var position = ((Point)point.Geometry).Coordinates;
            positions.Add(position);
            guids.Add(point.Id);
        }
        //latitude 緯度 Y, longitude 經度 X 

        var lineString = new LineString(positions);

        feature.Geometry = lineString;
        feature.Id = $"{Guid.NewGuid()}";

        var properties = new Dictionary<string, object>();
        properties.Add("guid", feature.Id);
        properties.Add("guids", String.Join(" ",guids));
        feature.Properties = properties;

        return feature;
    }

    public static Feature CreateRectangle(int x, int y)
    {
        var feature = new Feature();

        //latitude 緯度 Y, longitude 經度 X 
        var positions = new List<IPosition>();

        positions.Add(new Position(0, 0));
        positions.Add(new Position(0, x));
        positions.Add(new Position(y, x));
        positions.Add(new Position(y, 0));
        positions.Add(new Position(0, 0));

        var lineString = new LineString(positions);

        var rectangle = new Polygon(new List<LineString> { lineString });

        feature.Geometry = rectangle;
        feature.Id = $"{Guid.NewGuid()}";

        var properties = new Dictionary<string, object>();
        properties.Add("guid", feature.Id);
        properties.Add("x", x);
        properties.Add("y", y);
        feature.Properties = properties;

        return feature;
    }


    public static async Task ToGeoJsonFileAsync(FeatureCollection collection, string filePath)
    {
        var geoJson = ToGeoJson(collection);
        await File.WriteAllTextAsync(filePath, geoJson);
    }

    public static string ToGeoJson(FeatureCollection collection)
    {
        var geoJson = JsonSerializer.Serialize(collection, new JsonSerializerOptions { WriteIndented = true });
        return geoJson;
    }

}
