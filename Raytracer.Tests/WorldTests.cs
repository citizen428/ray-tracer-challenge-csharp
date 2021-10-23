namespace Raytracer.Tests;

using Raytracer.Primitives;
using Raytracer.Scene;
using Xunit;
using static Raytracer.Primitives.Vectors;

public class WorldTests
{
    readonly World world;

    public WorldTests()
    {
        world = World.Default();
    }

    [Fact]
    public void World_CreatingAWorld()
    {
        var world = new World();

        Assert.Empty(world.Objects);
        Assert.Null(world.Light);
    }

    [Fact]
    public void World_DefaultWorld()
    {
        var light = new Light(Vector(-10, 10, -10), RGB(1, 1, 1));

        Assert.Equal(2, world.Objects.Length);
        Assert.Equal(light, world.Light);
    }

    [Fact]
    public void World_IntersectWorldWithRay()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));

        var xs = world.Intersect(r);

        Assert.Equal(4, xs.Length);
        Assert.Equal(4, xs[0].T);
        Assert.Equal(4.5, xs[1].T);
        Assert.Equal(5.5, xs[2].T);
        Assert.Equal(6, xs[3].T);
    }

    [Fact]
    public void World_ShadingAnIntersection()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
        var s = world.Objects[0];
        var i = new Intersection(4, s);

        var comps = i.PrepareComputations(r);

        var c = world.ShadeHit(comps);
        Assert.Equal(RGB(0.38066, 0.47583, 0.2855), c, new VectorEqualityComparer());
    }

    [Fact]
    public void World_ShadingAnIntersectionFromTheInside()
    {
        world.Light = new Light(Point(0, 0.25, 0), RGB(1, 1, 1));
        var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
        var s = world.Objects[1];
        var i = new Intersection(0.5, s);

        var comps = i.PrepareComputations(r);

        var c = world.ShadeHit(comps);
        Assert.Equal(RGB(0.90498, 0.90498, 0.90498), c, new VectorEqualityComparer());
    }

    [Fact]
    public void World_TheColorWhenARayMisses()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 1, 0));

        var c = world.ColorAt(r);

        Assert.Equal(RGB(0, 0, 0), c);
    }

    [Fact]
    public void World_TheColorWhenARayHits()
    {
        var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));

        var c = world.ColorAt(r);

        Assert.Equal(RGB(0.38066, 0.47583, 0.2855), c, new VectorEqualityComparer());
    }

    [Fact]
    public void World_TheColorWithAnIntersectionBehindTheRay()
    {
        var material = new Material { Ambient = 1 };
        var outer = new Sphere(material);
        var inner = new Sphere(material);
        var light = new Light(Vector(-10, 10, -10), RGB(1, 1, 1));
        var world = new World(new Shape[] { outer, inner }, light);
        var r = new Ray(Point(0, 0, 0.75), Vector(0, 0, -1));

        var c = world.ColorAt(r);

        Assert.Equal(inner.Material.Color, c);
    }
}
