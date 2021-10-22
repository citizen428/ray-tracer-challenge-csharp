namespace Raytracer.Tests;

using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;

public class RayTests
{
    [Fact]
    public static void Ray_CreatingAndQuerying()
    {
        var origin = Point(1, 2, 3);
        var direction = Vector(4, 5, 6);
        var ray = new Ray(origin, direction);

        Assert.Equal(ray.Origin, origin);
        Assert.Equal(ray.Direction, direction);
    }

    [Fact]
    public static void Ray_ComputingAPointFromADistance()
    {
        var r = new Ray(Point(2, 3, 4), Vector(1, 0, 0));

        Assert.Equal(Point(2, 3, 4), r.Position(0));
        Assert.Equal(Point(3, 3, 4), r.Position(1));
        Assert.Equal(Point(1, 3, 4), r.Position(-1));
        Assert.Equal(Point(4.5, 3, 4), r.Position(2.5));
    }

    [Fact]
    public static void Ray_Translation()
    {
        var r = new Ray(Point(1, 2, 3), Vector(0, 1, 0));
        var m = Transformation.Translation(3, 4, 5);

        var r2 = r.Transform(m);

        Assert.Equal(Point(4, 6, 8), r2.Origin);
        Assert.Equal(Vector(0, 1, 0), r2.Direction);
    }

    [Fact]
    public static void Ray_Scaling()
    {
        var r = new Ray(Point(1, 2, 3), Vector(0, 1, 0));
        var m = Transformation.Scaling(2, 3, 4);

        var r2 = r.Transform(m);

        Assert.Equal(Point(2, 6, 12), r2.Origin);
        Assert.Equal(Vector(0, 3, 0), r2.Direction);
    }
}
