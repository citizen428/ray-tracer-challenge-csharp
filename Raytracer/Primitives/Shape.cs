namespace Raytracer.Primitives;

using Raytracer.Scene;

internal abstract class Shape
{
    internal Material Material { get; init; } = new Material();
    internal Matrix<double> Transform { get; set; } = Matrices.IdentityMatrix();
    internal abstract Intersection[] Intersect(Ray r);
    internal abstract Vector<double> NormalAt(Vector<double> worldPoint);
}
