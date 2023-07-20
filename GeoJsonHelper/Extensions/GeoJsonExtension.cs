using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using System.Runtime.CompilerServices;

namespace GeoJsonHelper.Extensions;
public static class GeoJsonExtension
{
    public static Point ToPoint(this IPosition position)
    {
        var point = new Point(position);
        return point;
    }

    public static Feature ToFeature(this IGeometryObject geometryObject)
    {
        var feature = new Feature();

        feature.Geometry = geometryObject;
        feature.Id = $"{Guid.NewGuid()}";

        var properties = new Dictionary<string, object>
        {
            { "guid", feature.Id },
        };
        feature.Properties = properties;

        return feature;
    }

    public static List<Feature> ToFeatures(this IEnumerable<IGeometryObject> geometryObjects)
    {
        var features = new List<Feature>();

        foreach (var geometryObject in geometryObjects)
        {
            var feature = geometryObject.ToFeature();
            features.Add(feature);
        }

        return features;
    }

    public static FeatureCollection ToFeatureCollection(this Feature feature)
    {
        var FeatureCollection = new FeatureCollection();

        FeatureCollection.Features.Add(feature);

        return FeatureCollection;
    }

    public static FeatureCollection ToFeatureCollection(this IEnumerable<Feature> features)
    {
        var FeatureCollection = new FeatureCollection();

        FeatureCollection.Features = features.ToList();

        return FeatureCollection;
    }

    public static List<IPosition> ToPositions(this IEnumerable<Point> Points)
    {
        var positions = new List<IPosition>();
        foreach (var point in Points)
        {
            positions.Add(point.Coordinates);
        }
        return positions;
    }
    /// <summary>
    /// Returns a new FeatureCollection with the newly added feature.
    /// <para>
    /// This method returns a new FeatureCollection so that the SfMaps will update itself. 
    /// </para>
    /// </summary>
    /// <param name="features"></param>
    /// <param name="feature"></param>
    /// <returns></returns>
    public static FeatureCollection? AddFeature(this FeatureCollection featureCollection, Feature feature)
    {
        var result = new FeatureCollection();

        result.Features = featureCollection.Features.Append(feature).ToList();

        return result;

    }

}
