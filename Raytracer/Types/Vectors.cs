namespace Raytracer.Types
{
    internal class Vectors
    {
        internal static Vector<double> Point(double x, double y, double z)
        {
            return Vector<double>.Build.DenseOfArray(new[] { x, y, z, 1 });
        }

        internal static Vector<double> Vector(double x, double y, double z)
        {
            return Vector<double>.Build.DenseOfArray(new[] { x, y, z, 0 });
        }

        internal static Vector<double> RGB(double r, double g, double b)
        {
            return Vector<double>.Build.DenseOfArray(new[] { r, g, b });
        }
    }
}
