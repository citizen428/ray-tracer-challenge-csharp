using Raytracer.Primitives;
using Raytracer.Scene;
using Xunit;
using static Raytracer.Primitives.Vectors;
using static Raytracer.Tests.Constants;

namespace Raytracer.Tests
{
    public class MaterialTests
    {
        [Fact]
        public void Material_DefaultMaterial()
        {
            var m = new Material();

            Assert.Equal(RGB(1, 1, 1), m.Color);
            Assert.Equal(0.1, m.Ambient);
            Assert.Equal(0.9, m.Diffuse);
            Assert.Equal(0.9, m.Specular);
            Assert.Equal(200, m.Shininess);
        }

        [Fact]
        public void Material_LightingEyeBetweenLightAndSurface()
        {
            var m = new Material();
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = new Light(Point(0, 0, -10), RGB(1));

            var result = m.Lighting(light, Origin(), eyeV, normalV);

            Assert.Equal(RGB(1.9), result);
        }

        [Fact]
        public void Material_LightingEyeBetweenLightAndSurfaceEyeOffset45Degrees()
        {
            var m = new Material();
            var eyeV = Vector(0, Sqrt2 / 2, -Sqrt2 / 2);
            var normalV = Vector(0, 0, -1);
            var light = new Light(Point(0, 0, -10), RGB(1));

            var result = m.Lighting(light, Origin(), eyeV, normalV);

            Assert.Equal(RGB(1), result);
        }

        [Fact]
        public void Material_LightOppositeSurfaceLightOffset45Degrees()
        {
            var m = new Material();
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = new Light(Point(0, 10, -10), RGB(1));

            var result = m.Lighting(light, Origin(), eyeV, normalV);

            Assert.Equal(RGB(0.7364), result, new VectorEqualityComparer());
        }

        [Fact]
        public void Material_LightingWithEyeInPathOfReflectionVector()
        {
            var m = new Material();
            var eyeV = Vector(0, -Sqrt2 / 2, -Sqrt2 / 2);
            var normalV = Vector(0, 0, -1);
            var light = new Light(Point(0, 10, -10), RGB(1));

            var result = m.Lighting(light, Origin(), eyeV, normalV);

            Assert.Equal(RGB(1.6364), result, new VectorEqualityComparer());
        }

        [Fact]
        public void Material_LightingWithLightBehindTheSurface()
        {
            var m = new Material();
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = new Light(Point(0, 0, 10), RGB(1));

            var result = m.Lighting(light, Origin(), eyeV, normalV);

            Assert.Equal(RGB(0.1), result, new VectorEqualityComparer());
        }

    }
}
