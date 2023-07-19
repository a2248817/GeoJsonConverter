using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using GeoJsonHelper.Extensions;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Point = GeoJSON.Text.Geometry.Point;

namespace GeoJsonHelper.Utilities;

public class GeoJsonHelper
{
    public static Position CreatePosition(int x, int y)
    {
        //latitude 緯度 Y, longitude 經度 X 
        var position = new Position(y, x);
        return position;
    }

    public static Point CreatePoint(int x, int y)
    {
        var point = CreatePosition(x, y).ToPoint();
        return point;
    }
    public static List<Point> CreatePoints(int x, int y)
    {
        var result = new List<Point>();

        //再往上建立Y方向點
        for (int i = 0; i < y; i++)
        {
            //先往右建立X方向點
            for (int j = 0; j < x; j++)
            {
                var feature = CreatePoint(j, i);
                result.Add(feature);
            }
        }

        return result;
    }


    public static LineString CreateLineStringFromPoints(IEnumerable<Point> points)
    {
        var positions = points.ToPositions();
        var lineString = new LineString(positions);
        return lineString;
    }

    public static Polygon CreatePolygonFromPoints(IEnumerable<Point> points)
    {
        if (points.First().Equals(points.Last()) == false)
        {
            points = points.Append(points.First());
        }
        var lineString = CreateLineStringFromPoints(points);

        var Polygon = new Polygon(new List<LineString> { lineString });
        return Polygon;
    }
    public static Feature CreateRectangle(int x, int y)
    {
        //latitude 緯度 Y, longitude 經度 X 
        var positions = new List<IPosition>
        {
            CreatePosition(0, 0),
            CreatePosition(x, 0),
            CreatePosition(x, y),
            CreatePosition(0, y),
            CreatePosition(0, 0)
        };

        var lineString = new LineString(positions);

        var rectangle = new Polygon(new List<LineString> { lineString }).ToFeature();

        rectangle.Properties.Add("points", positions.Count);

        return rectangle;
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
