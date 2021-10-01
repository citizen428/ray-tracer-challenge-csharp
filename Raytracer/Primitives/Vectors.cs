namespace Raytracer.Primitives
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

    internal class VectorEqualityComparer : IEqualityComparer<Vector<double>>
    {
        private static readonly double Epsilon = 0.00001;

        public bool Equals(Vector<double>? v1, Vector<double>? v2)
        {
            return v1 != null &&
                v2 != null &&
                Math.Abs(v1.X() - v2.X()) < Epsilon &&
                Math.Abs(v1.Y() - v2.Y()) < Epsilon &&
                Math.Abs(v1.Z() - v2.Z()) < Epsilon;
        }

        public int GetHashCode(Vector<double> v)
        {
            return v.GetHashCode();
        }
    }
}
