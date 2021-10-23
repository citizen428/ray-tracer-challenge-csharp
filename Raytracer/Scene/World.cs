namespace Raytracer.Scene;

using Raytracer.Primitives;
using static Raytracer.Primitives.Vectors;

internal class World
{
    internal Shape[] Objects { get; private set; } = Array.Empty<Shape>();
    internal Light? Light { get; set; }

    internal World() { }

    internal World(Shape[] objects, Light light)
    {
        Objects = objects;
        Light = light;
    }

    internal static World Default()
    {
        var mat = new Material
        {
            Color = RGB(0.8, 1, 0.6),
            Diffuse = 0.7,
            Specular = 0.2
        };
        var s1 = new Sphere { Material = mat };
        var s2 = new Sphere
        {
            Transform = Transformation.Scaling(0.5, 0.5, 0.5)
        };

        var objects = new Shape[] { s1, s2 };
        var light = new Light(Vector(-10, 10, -10), RGB(1, 1, 1));
        return new(objects, light);
    }

    internal Intersection[] Intersect(Ray r)
    {
        return Objects
            .SelectMany(shape => shape.Intersect(r))
            .OrderBy(i => i.T).ToArray();

    }

    internal Vector<double> ShadeHit(Computations comps)
    {
        return comps.Object.Material.Lighting(Light!, comps.Point, comps.EyeV, comps.NormalV);
    }

    internal Vector<double> ColorAt(Ray r)
    {
        var xs = Intersect(r);
        var hit = Intersection.Hit(xs);
        if (hit == null)
        {
            return RGB(0, 0, 0);
        }

        var comps = hit.PrepareComputations(r);
        return ShadeHit(comps);
    }
}
