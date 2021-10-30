namespace Raytracer.Primitives;

using static Raytracer.Constants;

internal class Matrices
{
    internal static Matrix<double> Matrix(double[,] rows)
    {
        return Matrix<double>.Build.DenseOfArray(rows);
    }

    internal static Matrix<double> IdentityMatrix(int norm = 4)
    {
        return Matrix<double>.Build.DiagonalIdentity(norm);
    }

    internal class MatrixEqualityComparer : IEqualityComparer<Matrix<double>>
    {

        public bool Equals(Matrix<double>? m1, Matrix<double>? m2)
        {
            return m1 != null && m2 != null &&
                m1.Find2((v1, v2) => Math.Abs(v1 - v2) > Epsilon, m2) == null;
        }

        public int GetHashCode(Matrix<double> m)
        {
            return m.GetHashCode();
        }
    }

}
