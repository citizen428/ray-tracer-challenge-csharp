namespace Raytracer.Primitives
{
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
    }
}
