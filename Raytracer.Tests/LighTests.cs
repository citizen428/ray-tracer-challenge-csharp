using Raytracer.Scene;
using Xunit;
using static Raytracer.Primitives.Vectors;

namespace Raytracer.Tests
{
    public class LightTests
    {
        [Fact]
        public void LightPointLightHasPositionAndIntensity()
        {
            var position = Origin();
            var color = RGB(1, 1, 1);

            var light = new Light(position, color);

            Assert.Equal(position, light.Position);
            Assert.Equal(color, light.Color);
        }
    }
}
