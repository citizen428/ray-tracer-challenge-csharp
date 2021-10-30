namespace Raytracer.Tests;

using Raytracer.Primitives;
using Raytracer.Scene;
using Xunit;
using static Raytracer.Primitives.Vectors;
using static Raytracer.Tests.Constants;

public class CameraTests
{
    [Fact]
    public void Camera_ConstructingACamera()
    {
        var fov = FullQuarter;
        var c = new Camera(160, 120, fov);

        Assert.Equal(160, c.HSize);
        Assert.Equal(120, c.VSize);
        Assert.Equal(fov, c.FieldOFView);
    }

    [Fact]
    public void Camera_PixelSizeForHorizontalCanvas()
    {
        var c = new Camera(200, 125, FullQuarter);

        Assert.Equal(0.01, c.PixelSize, FloatPrecision);
    }

    [Fact]
    public void Camera_PixelSizeForVerticalCanvas()
    {
        var c = new Camera(125, 200, FullQuarter);

        Assert.Equal(0.01, c.PixelSize, FloatPrecision);
    }

    [Fact]
    public void Camera_ConstructingRayThroughCenterOfTheCanvas()
    {
        var c = new Camera(201, 101, FullQuarter);

        var r = c.RayForPixel(100, 50);

        Assert.Equal(Point(0, 0, 0), r.Origin);
        Assert.Equal(Vector(0, 0, -1), r.Direction, new VectorEqualityComparer());
    }

    [Fact]
    public void Camera_ConstructingRayThroughACornerOfTheCanvas()
    {
        var c = new Camera(201, 101, FullQuarter);

        var r = c.RayForPixel(0, 0);

        Assert.Equal(Point(0, 0, 0), r.Origin);
        Assert.Equal(Vector(0.66519, 0.33259, -0.66851), r.Direction, new VectorEqualityComparer());
    }

    [Fact]
    public void Camera_ConstructingRayWhenCameraIsTransformed()
    {
        var t = Transformation.RotationY(HalfQuarter) * Transformation.Translation(0, -2, 5);
        var c = new Camera(201, 101, FullQuarter, t);

        var r = c.RayForPixel(100, 50);

        Assert.Equal(Point(0, 2, -5), r.Origin);
        Assert.Equal(Vector(Sqrt2 / 2, 0, -Sqrt2 / 2), r.Direction, new VectorEqualityComparer());
    }

    [Fact]
    public void Camera_RenderingAWorld()
    {
        var w = World.Default();
        var from = Point(0, 0, -5);
        var to = Point(0, 0, 0);
        var up = Vector(0, 1, 0);
        var transform = Transformation.View(from, to, up);
        var c = new Camera(11, 11, FullQuarter, transform);

        var image = c.Render(w);

        Assert.Equal(image.GetPixel(5, 5), RGB(0.38066, 0.47583, 0.2855).ToColor());
    }
}
