namespace Raytracer.Primitives;

internal record Computations(
    double T,
    Shape Object,
    Vector<double> Point,
    Vector<double> EyeV,
    Vector<double> NormalV,
    bool Inside
);

internal record Intersection(double T, Shape Object)
{
    internal static Intersection[] Collect(params Intersection[] xs)
    {
        return xs;
    }

    internal static Intersection? Hit(Intersection[] xs)
    {
        return xs.Where(x => x.T > 0).OrderBy(x => x.T).FirstOrDefault();
    }

    internal Computations PrepareComputations(Ray ray)
    {
        var point = ray.Position(T);
        var eyeV = -ray.Direction;
        var normalV = Object.NormalAt(point);
        var inside = false;

        if (normalV.DotProduct(eyeV) < 0)
        {
            inside = true;
            normalV = -normalV;
        }

        return new Computations(T, Object, point, eyeV, normalV, inside);
    }
}
