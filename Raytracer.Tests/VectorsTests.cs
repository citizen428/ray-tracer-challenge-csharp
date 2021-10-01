using Raytracer.Primitives;
using Xunit;
using static Raytracer.Primitives.Vectors;

namespace Raytracer.Tests
{

    public class VectorTests
    {
        [Fact]
        public void Point_WComponentEquals1()
        {
            var p = Point(4.3, -4.2, 3.1);

            Assert.Equal(1, p.W());
        }

        [Fact]
        public void Vector_WComponentEquals0()
        {
            var p = Vector(4.3, -4.2, 3.1);

            Assert.Equal(0, p.W());
        }

        [Fact]
        public void Vector_CanComputeCrossProduct()
        {
            var v1 = Vector(1, 2, 3);
            var v2 = Vector(2, 3, 4);

            Assert.Equal(Vector(-1, 2, -1), v1.Cross(v2));
            Assert.Equal(Vector(1, -2, 1), v2.Cross(v1));
        }

        [Fact]
        public void RGB_CanConvertFromFloatValuesToColor()
        {
            var rgb = RGB(0.59, 0.65, 0.23);
            var color = rgb.ToColor();

            Assert.Equal(150, color.R);
            Assert.Equal(165, color.G);
            Assert.Equal(58, color.B);
        }
    }
}
