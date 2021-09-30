using System.Drawing;
using System.Drawing.Imaging;
using Raytracer.Types;
using static Raytracer.Types.Vectors;

namespace Raytracer
{
    internal static class Program
    {
        private const int ImageWidth = 400;
        private const int ImageHeight = 400;

        internal static void Main()
        {
            var centerX = ImageWidth / 2;
            var centerY = ImageHeight / 2;
            var twelve = Point(0, (float)centerY * 0.8, 0);
            var hourAngle = Math.PI / 6;

            using Bitmap? b = new(ImageWidth, ImageHeight);
            using (var g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
            }

            foreach (var hour in Enumerable.Range(1, 12))
            {
                var transformation = Transformation.RotationZ(hourAngle * hour);
                var point = transformation * twelve;
                var x = centerX + (int)point.X();
                var y = centerY - (int)point.Y();
                b.SetPixel(x, y, Color.White);
            }

            b.Save(@"images/ch04-clock.png", ImageFormat.Png);
        }
    }
}
