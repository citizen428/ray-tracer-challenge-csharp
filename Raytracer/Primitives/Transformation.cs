namespace Raytracer.Primitives;

using static Raytracer.Primitives.Matrices;

internal class Transformation
{
    internal static Matrix<double> Translation(double x, double y, double z)
    {
        return Matrix(new double[,] {
                { 1, 0, 0, x },
                { 0, 1, 0, y },
                { 0, 0, 1, z },
                { 0, 0, 0, 1 }
            });
    }

    internal static Matrix<double> Scaling(double x, double y, double z)
    {
        return Matrix(new double[,] {
                { x, 0, 0, 0 },
                { 0, y, 0, 0 },
                { 0, 0, z, 0 },
                { 0, 0, 0, 1 }
            });
    }

    internal static Matrix<double> RotationX(double radian)
    {
        var sin = Math.Sin(radian);
        var cos = Math.Cos(radian);

        return Matrix(new double[,] {
                { 1, 0, 0, 0 },
                { 0, cos, -sin, 0 },
                { 0, sin, cos, 0 },
                { 0, 0, 0, 1 }
            });
    }

    internal static Matrix<double> RotationY(double radian)
    {
        var sin = Math.Sin(radian);
        var cos = Math.Cos(radian);

        return Matrix(new double[,] {
                { cos, 0, sin, 0 },
                { 0, 1, 0, 0 },
                { -sin, 0, cos, 0 },
                { 0, 0, 0, 1 }
            });
    }

    internal static Matrix<double> RotationZ(double radian)
    {
        var sin = Math.Sin(radian);
        var cos = Math.Cos(radian);

        return Matrix(new double[,] {
                { cos, -sin, 0, 0 },
                { sin, cos, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
    }

    internal static Matrix<double> Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
    {
        return Matrix(new double[,] {
                { 1, xy, xz, 0 },
                { yx, 1, yz, 0 },
                { zx, zy, 1, 0 },
                { 0, 0, 0, 1 }
            });

    }

    internal static Matrix<double> View(Vector<double> from, Vector<double> to, Vector<double> up)
    {
        var forward = (to - from).Unit();
        var left = forward.Cross(up.Unit());
        var trueUp = left.Cross(forward);

        var orientation = Matrix(new double[,] {
            { left.X(), left.Y(), left.Z(), 0 },
            { trueUp.X(), trueUp.Y(), trueUp.Z(), 0 },
            { -forward.X(), -forward.Y(), -forward.Z(), 0 },
            { 0, 0, 0, 1}
        });

        return orientation * Translation(-from.X(), -from.Y(), -from.Z());

    }
}
