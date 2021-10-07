using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;

namespace Raytracer.Tests
{
    public class SphereTests
    {
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
            var ray = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
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
    }
}
