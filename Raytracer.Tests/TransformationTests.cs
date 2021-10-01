using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;

namespace Raytracer.Tests
{
    using static Constants;

    public class TransformationTests
    {
        private static readonly VectorEqualityComparer VectorComparer = new();

        [Fact]
        public void Transformation_TranslatePoint()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var p = Point(-3, 4, 5);
            var expected = Point(2, 1, 7);

            Assert.Equal(expected, transform * p);
        }

        [Fact]
        public void Transformation_TranslatePointWithInvertedTranslationMatrix()
        {
            var transform = Transformation.Translation(5, -3, 2).Inverse();
            var p = Point(-3, 4, 5);
            var expected = Point(-8, 7, 3);

            Assert.Equal(expected, transform * p);
        }

        [Fact]
        public void Transformation_TranslationDoesNotAffectVectors()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var v = Vector(-3, 4, 5);

            Assert.Equal(v, transform * v);
        }

        [Fact]
        public void Transformation_ScalingAppliedToPoint()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var p = Point(-4, 6, 8);

            Assert.Equal(Point(-8, 18, 32), transform * p);
        }

        [Fact]
        public void Transformation_ScalingAppliedToVector()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var v = Vector(-4, 6, 8);

            Assert.Equal(Vector(-8, 18, 32), transform * v);
        }

        [Fact]
        public void Transformation_ReflectionScalesByNegativeValue()
        {
            var transform = Transformation.Scaling(-1, 1, 1);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(-2, 3, 4), transform * p);
        }

        [Fact]
        public void Transformation_RotatingPointAroundXAxis()
        {
            var p = Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(HalfQuarter);
            var fullQuarter = Transformation.RotationX(FullQuarter);

            Assert.Equal(Point(0, Sqrt2 / 2, Sqrt2 / 2), halfQuarter * p);
            Assert.Equal(Point(0, 0, 1), fullQuarter * p, VectorComparer);
        }

        [Fact]
        public void Transformation_InverseRotatingPointAroundXAxis()
        {
            var p = Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(HalfQuarter).Inverse();
            var expected = Point(0, Sqrt2 / 2, -Sqrt2 / 2);

            Assert.Equal(expected, halfQuarter * p, VectorComparer);
        }

        [Fact]
        public void Transformation_RotatingPointAroundYAxis()
        {
            var p = Point(0, 0, 1);
            var halfQuarter = Transformation.RotationY(HalfQuarter);
            var fullQuarter = Transformation.RotationY(FullQuarter);

            Assert.Equal(Point(Sqrt2 / 2, 0, Sqrt2 / 2), halfQuarter * p);
            Assert.Equal(Point(1, 0, 0), fullQuarter * p, VectorComparer);
        }

        [Fact]
        public void Transformation_RotatingPointAroundZAxis()
        {
            var p = Point(0, 1, 0);
            var halfQuarter = Transformation.RotationZ(HalfQuarter);
            var fullQuarter = Transformation.RotationZ(FullQuarter);

            Assert.Equal(Point(-Sqrt2 / 2, Sqrt2 / 2, 0), halfQuarter * p);
            Assert.Equal(Point(-1, 0, 0), fullQuarter * p, VectorComparer);
        }

        [Fact]
        public void Transformation_ShearingMovesXinProportionToY()
        {
            var transform = Transformation.Shearing(1, 0, 0, 0, 0, 0);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(5, 3, 4), transform * p);
        }

        [Fact]
        public void Transformation_ShearingMovesXinProportionToZ()
        {
            var transform = Transformation.Shearing(0, 1, 0, 0, 0, 0);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(6, 3, 4), transform * p);
        }

        [Fact]
        public void Transformation_ShearingMovesYinProportionToX()
        {
            var transform = Transformation.Shearing(0, 0, 1, 0, 0, 0);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(2, 5, 4), transform * p);
        }

        [Fact]
        public void Transformation_ShearingMovesYinProportionToZ()
        {
            var transform = Transformation.Shearing(0, 0, 0, 1, 0, 0);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(2, 7, 4), transform * p);
        }

        [Fact]
        public void Transformation_ShearingMovesZinProportionToX()
        {
            var transform = Transformation.Shearing(0, 0, 0, 0, 1, 0);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(2, 3, 6), transform * p);
        }

        [Fact]
        public void Transformation_ShearingMovesZinProportionToY()
        {
            var transform = Transformation.Shearing(0, 0, 0, 0, 0, 1);
            var p = Point(2, 3, 4);

            Assert.Equal(Point(2, 3, 7), transform * p);
        }

        [Fact]
        public void Transformation_IndividuaTranformationsAppliedInSequence()
        {
            var p = Point(1, 0, 1);
            var a = Transformation.RotationX(FullQuarter);
            var b = Transformation.Scaling(5, 5, 5);
            var c = Transformation.Translation(10, 5, 7);

            // apply rotation first
            var p2 = a * p;
            Assert.Equal(Point(1, -1, 0), p2, VectorComparer);

            // then apply scaling
            var p3 = b * p2;
            Assert.Equal(Point(5, -5, 0), p3, VectorComparer);

            // then apply translation
            var p4 = c * p3;
            Assert.Equal(Point(15, 0, 7), p4, VectorComparer);
        }

        [Fact]
        public void Transformation_ChainedTransformationsAreAppliedinReverse()
        {
            var p = Point(1, 0, 1);
            var a = Transformation.RotationX(FullQuarter);
            var b = Transformation.Scaling(5, 5, 5);
            var c = Transformation.Translation(10, 5, 7);
            var transform = c * b * a;

            Assert.Equal(Point(15, 0, 7), transform * p, VectorComparer);
        }
    }
}
