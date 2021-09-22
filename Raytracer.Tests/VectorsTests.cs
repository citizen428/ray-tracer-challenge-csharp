namespace Raytracer.Tests;

using Raytracer.Types;
using Xunit;
using static Raytracer.Types.Vectors;

public class VectorTests
{
    [Fact]
    public void Point_WComponentEquals1()
    {
        var p = Point(4.3f, -4.2f, 3.1f);

        Assert.Equal(1f, p.W);
    }

    [Fact]
    public void Vector_WComponentEquals0()
    {
        var p = Vector(4.3f, -4.2f, 3.1f);

        Assert.Equal(0f, p.W);
    }

    [Fact]
    public void Vector_CanComputeCrossProduct()
    {
        var v1 = Vector(1f, 2f, 3f);
        var v2 = Vector(2f, 3f, 4f);

        Assert.Equal(Vector(-1f, 2f, -1f), v1.Cross(v2));
        Assert.Equal(Vector(1f, -2f, 1f), v2.Cross(v1));
    }
}
