namespace Raytracer.Tests;

using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;

public class IntersectionTests
{
    [Fact]
    public void Intersection_EncapsulatesTAndObject()
    {
        var s = new Sphere();
        var i = new Intersection(3.5, s);

        Assert.Equal(3.5, i.T);
        Assert.Equal(s, i.Object);
    }

    [Fact]
    public void Intersection_Aggregating()
    {
        var s = new Sphere();
        var i1 = new Intersection(1, s);
        var i2 = new Intersection(2, s);

        var xs = Intersection.Collect(i1, i2);

        Assert.Equal(2, xs.Length);
        Assert.Equal(1, xs[0].T);
        Assert.Equal(2, xs[1].T);
    }

    [Fact]
    public void Hit_WhenAllIntersectionsHavePositiveT()
    {
        var s = new Sphere();
        var i1 = new Intersection(1, s);
        var i2 = new Intersection(2, s);
        var xs = Intersection.Collect(i1, i2);

        var i = Intersection.Hit(xs);

        Assert.Equal(i1, i);
    }

    [Fact]
    public void Hit_WhenSomeIntersectionsHaveNegativeT()
    {
        var s = new Sphere();
        var i1 = new Intersection(-1, s);
        var i2 = new Intersection(1, s);
        var xs = Intersection.Collect(i1, i2);

        var i = Intersection.Hit(xs);

        Assert.Equal(i2, i);
    }

    [Fact]
    public void Hit_WhenAllIntersectionsHaveNegativeT()
    {
        var s = new Sphere();
        var i1 = new Intersection(-2, s);
        var i2 = new Intersection(-1, s);
        var xs = Intersection.Collect(i1, i2);

        var i = Intersection.Hit(xs);

        Assert.Null(i);
    }

    [Fact]
    public void Hit_IsAlwaysTheLowestNonnegativeIntersection()
    {
        var s = new Sphere();
        var i1 = new Intersection(5, s);
        var i2 = new Intersection(7, s);
        var i3 = new Intersection(-3, s);
        var i4 = new Intersection(2, s);
        var xs = Intersection.Collect(i1, i2, i3, i4);

        var i = Intersection.Hit(xs);

        Assert.Equal(i4, i);
    }


    [Fact]
    public void Intersection_PrecomputingTheStateOfAnIntersection()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
        var s = new Sphere();
        var i = new Intersection(4, s);

        var comps = i.PrepareComputations(r);

        Assert.Equal(i.T, comps.T);
        Assert.Equal(i.Object, comps.Object);
        Assert.Equal(Point(0, 0, -1), comps.Point);
        Assert.Equal(Vector(0, 0, -1), comps.EyeV);
        Assert.Equal(Vector(0, 0, -1), comps.NormalV);
    }

    [Fact]
    public void Hit_WhenIntersectionOccursOnTheOutside()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
        var s = new Sphere();
        var i = new Intersection(4, s);

        var comps = i.PrepareComputations(r);

        Assert.False(comps.Inside);
    }

    [Fact]
    public void Hit_WhenIntersectionOccursOnTheInside()
    {
        var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
        var s = new Sphere();
        var i = new Intersection(1, s);

        var comps = i.PrepareComputations(r);

        Assert.Equal(Point(0, 0, 1), comps.Point);
        Assert.Equal(Vector(0, 0, -1), comps.EyeV);
        Assert.True(comps.Inside);
        // normal would have been (0, 0, 1) but is inverted
        Assert.Equal(Vector(0, 0, -1), comps.NormalV);
    }
}
