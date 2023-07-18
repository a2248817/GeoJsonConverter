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


    //public static Feature ToFeature(this Point point)
    //{
    //    var feature = new Feature();

    //    feature.Geometry = point;
    //    feature.Id = $"{Guid.NewGuid()}";

    //    var properties = new Dictionary<string, object>
    //    {
    //        { "guid", feature.Id },
    //        { "x", point.Coordinates.Longitude },
    //        { "y", point.Coordinates.Latitude },
    //    };

    //    feature.Properties = properties;

    //    return feature;
    //}
    //public static Feature ToFeature(this LineString lineString)
    //{
    //    var feature = new Feature();

    //    feature.Geometry = lineString;
    //    feature.Id = $"{Guid.NewGuid()}";

    //    var properties = new Dictionary<string, object>
    //    {
    //        { "guid", feature.Id }
    //    };
    //    feature.Properties = properties;

    //    return feature;
    //}
    //public static Feature ToFeature(this Polygon polygon)
    //{
    //    var feature = new Feature();

    //    feature.Geometry = polygon;
    //    feature.Id = $"{Guid.NewGuid()}";

    //    var properties = new Dictionary<string, object>
    //    {
    //        { "guid", feature.Id },
    //    };
    //    feature.Properties = properties;

    //    return feature;
    //}

    public static Feature ToFeature<T>(this T geometryObject) where T : IGeometryObject
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

    public static IEnumerable<IPosition> ToPositions(this IEnumerable<Point> Points)
    {
        var positions = new List<IPosition>();
        foreach (var point in Points)
        {
            positions.Add(point.Coordinates);
        }
        return positions;
    }

}
