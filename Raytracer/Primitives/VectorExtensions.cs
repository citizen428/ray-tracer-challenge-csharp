using System.Drawing;

namespace Raytracer.Primitives
{
    internal static class VectorExtensions
    {

        public static double X(this Vector<double> v)
        {
            return v[0];
        }

        public static double Y(this Vector<double> v)
        {
            return v[1];
        }
        public static double Z(this Vector<double> v)
        {
            return v[2];
        }
        public static double W(this Vector<double> v)
        {
            return v[3];
        }

        public static double Length(this Vector<double> v)
        {
            return Math.Sqrt((v.X() * v.X()) + (v.Y() * v.Y()) + (v.Z() * v.Z()));
        }

        public static Vector<double> Unit(this Vector<double> v)
        {
            var length = v.Length();

            return Vector<double>.Build.DenseOfArray(new[] {
                v.X() / length,
                v.Y() / length,
                v.Z() / length,
                0
            });
        }

        public static Vector<double> Cross(this Vector<double> v1, Vector<double> v2)
        {
            var x = (v1.Y() * v2.Z()) - (v2.Y() * v1.Z());
            var y = ((v1.X() * v2.Z()) - (v2.X() * v1.Z())) * -1;
            var z = (v1.X() * v2.Y()) - (v2.X() * v1.Y());

            return Vector<double>.Build.DenseOfArray(new[] { x, y, z, 0 });
        }

        public static Color ToColor(this Vector<double> v)
        {
            static int convert(double d)
            {
                return (int)(255 * d);
            }

            return Color.FromArgb(convert(v.X()), convert(v.Y()), convert(v.Z()));
        }
    }
}
