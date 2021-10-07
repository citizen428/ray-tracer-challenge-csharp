namespace Raytracer.Primitives
{
    internal class Shape
    {
        public Matrix<double> Transform { get; set; } = Matrices.IdentityMatrix();
    }
}
