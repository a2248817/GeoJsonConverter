using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using System.Text.Json;

namespace GeoJsonHelper.Utilities;

public class GeoJsonHelper
{
    public static FeatureCollection CreateGrid(int x, int y)
    {
        var result = new FeatureCollection();

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                var feature = new Feature();
                //latitude 緯度 Y, longitude 經度 X 
                var position = new Position(i, j);
                var geometry = new Point(position);
                feature.Geometry = geometry;
                feature.Id = $"{i * x + j}";
                var properties = new Dictionary<string, object>();
                properties.Add("x", j);
                properties.Add("y", i);
                feature.Properties = properties;
                result.Features.Add(feature);
            }
        }

        return result;
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
