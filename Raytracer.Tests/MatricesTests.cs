using Xunit;
using static Raytracer.Types.Matrices;
using static Raytracer.Types.Vectors;

namespace Raytracer.Tests
{
    public class MatrixTests
    {
        [Fact]
        public void Matrix_4x4CanBeInitializedFromRowArray()
        {
            var m = Matrix(new double[,] {
                { 1, 2, 3, 4 },
                { 5.5, 6.5, 7.5, 8.5 },
                { 9, 10, 11, 12 },
                { 13.5, 14.5, 15.5, 16.5 }
            });

            Assert.Equal(1, m[0, 0]);
            Assert.Equal(4, m[0, 3]);
            Assert.Equal(5.5, m[1, 0]);
            Assert.Equal(7.5, m[1, 2]);
            Assert.Equal(11, m[2, 2]);
            Assert.Equal(13.5, m[3, 0]);
            Assert.Equal(15.5, m[3, 2]);
        }


        [Fact]
        public void Matrix_2x2CanBeInitializedFromRowArray()
        {
            var m = Matrix(new double[,] {
                { -3, 5 },
                { 1, -2 }
            });

            Assert.Equal(-3, m[0, 0]);
            Assert.Equal(5, m[0, 1]);
            Assert.Equal(1, m[1, 0]);
            Assert.Equal(-2, m[1, 1]);
        }

        [Fact]
        public void Matrix_3x3CanBeInitializedFromRowArray()
        {
            var m = Matrix(new double[,] {
                { -3, 5, 0 },
                { 1, -2, -7 },
                { 0, 1, 1 }
            });

            Assert.Equal(-3, m[0, 0]);
            Assert.Equal(-2, m[1, 1]);
            Assert.Equal(1, m[2, 2]);
        }

        [Fact]
        public void Matrix_CanBeMultipliedWithOtherMatrix()
        {
            var m1 = Matrix(new double[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2}
            });

            var m2 = Matrix(new double[,] {
                { -2, 1, 2, 3 },
                { 3, 2, 1, -1 },
                { 4, 3, 6, 5 },
                { 1, 2, 7, 8 }
            });

            var expected = Matrix(new double[,] {
                { 20, 22, 50, 48 },
                { 44, 54, 114, 108 },
                { 40, 58, 110, 102 },
                { 16, 26, 46, 42 }
            });

            Assert.Equal(expected, m1 * m2);
        }

        [Fact]
        public void Matrix_CanBeMultipliedByVector()
        {
            var m = Matrix(new double[,] {
                { 1, 2, 3, 4 },
                { 2, 4, 4, 2 },
                { 8, 6, 4, 1 },
                { 0, 0, 0, 1 }
            });

            var v = Vector(1, 2, 3);
            var expected = Vector(14, 22, 32);

            Assert.Equal(expected, m * v);
        }

        [Fact]
        public void Matrix_MultipliedByIdentityMatrixDoesNotChange()
        {
            var m = Matrix(new double[,] {
                { 0, 1, 2, 4 },
                { 1, 2, 4, 8 },
                { 2, 4, 8, 16 },
                { 4, 8, 16, 32 }
            });

            Assert.Equal(m, m * IdentityMatrix());
        }

        [Fact]
        public void Matrix_IdentityMultipliedByVector()
        {
            var v = Vector(1, 2, 3);

            Assert.Equal(v, IdentityMatrix() * v);
        }

        [Fact]
        public void Matrix_CanBeTransposed()
        {
            var m = Matrix(new double[,] {
                { 0, 9, 3, 0 },
                { 9, 8, 0, 8 },
                { 1, 8, 5, 3 },
                { 0, 0, 5, 8 }
            });

            var expected = Matrix(new double[,] {
                { 0, 9, 1, 0 },
                { 9, 8, 8, 0 },
                { 3, 0, 5, 5 },
                { 0, 8, 3, 8 }
            });

            Assert.Equal(expected, m.Transpose());
        }

        [Fact]
        public void Matrix_TransposingIdentityMatrix()
        {
            var i = IdentityMatrix();
            Assert.Equal(i, i.Transpose());
        }
    }
}
