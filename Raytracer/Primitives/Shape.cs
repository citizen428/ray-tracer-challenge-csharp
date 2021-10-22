namespace Raytracer.Primitives;

internal abstract class Shape
{
    internal Matrix<double> Transform { get; set; } = Matrices.IdentityMatrix();
    internal abstract Intersection[] Intersect(Ray r);
}
