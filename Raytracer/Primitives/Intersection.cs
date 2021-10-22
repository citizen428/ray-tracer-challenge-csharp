namespace Raytracer.Primitives;

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
}
