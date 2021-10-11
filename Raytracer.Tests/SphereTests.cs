using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;
using static Raytracer.Tests.Constants;

namespace Raytracer.Tests
{
    public class SphereTests
    {
        private static readonly VectorEqualityComparer VectorComparer = new();

        [Fact]
        public void Sphere_RayIntersectsAtTwoPoints()
        {
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Length);
            Assert.Equal(4.0, xs[0].T);
            Assert.Equal(6.0, xs[1].T);
            Assert.Equal(s, xs[0].Object);
        }

        [Fact]
        public void Sphere_RayIntersectsAtTangent()
        {
            var ray = new Ray(Point(0, 1, -5), Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Length);
            Assert.Equal(5.0, xs[0].T);
            Assert.Equal(5.0, xs[1].T);
        }

        [Fact]
        public void Sphere_RayMisses()
        {
            var ray = new Ray(Point(0, 2, -5), Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Empty(xs);
        }

        [Fact]
        public void Sphere_RayOriginatesInsideSphere()
        {
            var ray = new Ray(Origin(), Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Length);
            Assert.Equal(-1, xs[0].T);
            Assert.Equal(1, xs[1].T);
        }

        [Fact]
        public void Sphere_IsBehindRay()
        {
            var ray = new Ray(Point(0, 0, 5), Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Length);
            Assert.Equal(-6, xs[0].T);
            Assert.Equal(-4, xs[1].T);
        }

        [Fact]
        public void Sphere_DefaultTransformation()
        {
            var s = new Sphere();

            Assert.Equal(Matrices.IdentityMatrix(), s.Transform);
        }

        [Fact]
        public void Sphere_ChangingTransformation()
        {
            var s = new Sphere();
            var t = Transformation.Translation(2, 3, 4);

            s.Transform = t;

            Assert.Equal(t, s.Transform);
        }

        [Fact]
        public void Sphere_IntersectingScaledWithRay()
        {
            var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var s = new Sphere
            {
                Transform = Transformation.Scaling(2, 2, 2)
            };

            var xs = s.Intersect(r);

            Assert.Equal(2, xs.Length);
            Assert.Equal(3, xs[0].T);
            Assert.Equal(7, xs[1].T);
        }

        [Fact]
        public void Sphere_IntersectingTranslatedWithRay()
        {

            var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var s = new Sphere
            {
                Transform = Transformation.Translation(5, 0, 0)
            };

            var xs = s.Intersect(r);

            Assert.Empty(xs);
        }

        [Fact]
        public void Sphere_NormalOnAPointAtTheXAxis()
        {
            var s = new Sphere();

            var n = s.NormalAt(Point(1, 0, 0));

            Assert.Equal(Vector(1, 0, 0), n);
        }

        [Fact]
        public void Sphere_NormalOnAPointAtTheYAxis()
        {
            var s = new Sphere();

            var n = s.NormalAt(Point(0, 1, 0));

            Assert.Equal(Vector(0, 1, 0), n);
        }

        [Fact]
        public void Sphere_NormalOnAPointAtTheZAxis()
        {
            var s = new Sphere();

            var n = s.NormalAt(Point(0, 0, 1));

            Assert.Equal(Vector(0, 0, 1), n);
        }

        [Fact]
        public void Sphere_NormalOnAPointAtANonaxialPoint()
        {
            var s = new Sphere();

            var x = System.Math.Sqrt(3) / 3;
            var n = s.NormalAt(Point(x, x, x));

            Assert.Equal(Vector(x, x, x), n);
        }

        [Fact]
        public void Sphere_NormalIsANormalizedVector()
        {
            var s = new Sphere();

            var x = System.Math.Sqrt(3) / 3;
            var n = s.NormalAt(Point(x, x, x));

            Assert.Equal(n.Unit(), n);
        }

        [Fact]
        public void Sphere_ComputingNormalOnTranslatedSphere()
        {
            var s = new Sphere
            {
                Transform = Transformation.Translation(0, 1, 0)
            };

            var n = s.NormalAt(Point(0, 1.70711, -0.70711));

            Assert.Equal(Vector(0, 0.70711, -0.70711), n, VectorComparer);
        }

        [Fact]
        public void Sphere_ComputingNormalOnTransformedSphere()
        {
            var m = Transformation.Scaling(1, 0.5, 1) *
                Transformation.RotationZ(System.Math.PI / 5);
            var s = new Sphere
            {
                Transform = m
            };

            var n = s.NormalAt(Point(0, Sqrt2 / 2, -Sqrt2 / 2));

            Assert.Equal(Vector(0, 0.97014, -0.24254), n, VectorComparer);
        }
    }
}
