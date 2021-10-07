using System.Drawing;
using System.Drawing.Imaging;
using Raytracer.Primitives;
using static Raytracer.Primitives.Vectors;

namespace Raytracer
{
    internal static class Program
    {
        internal static void Main()
        {
            var rayOrigin = Point(0, 0, -5);
            var wallZ = 10.0;
            var wallSize = 7.0;
            var canvasSize = 400;
            var pixelSize = wallSize / canvasSize;
            var half = wallSize / 2;
            var sphere = new Sphere();

            using Bitmap? b = new(canvasSize, canvasSize);
            using (var g = Graphics.FromImage(b))
            {
                g.Clear(Color.White);
            }

            foreach (var y in Enumerable.Range(0, canvasSize - 1))
            {
                var worldY = half - (pixelSize * y);
                foreach (var x in Enumerable.Range(0, canvasSize - 1))
                {
                    var worldX = -half + (pixelSize * x);
                    var position = Point(worldX, worldY, wallZ);
                    var rayDirection = position - rayOrigin.Unit();
                    var ray = new Ray(rayOrigin, rayDirection);

                    var xs = sphere.Intersect(ray);
                    var hit = Intersection.Hit(xs);
                    if (hit is not null)
                    {
                        b.SetPixel(x, y, Color.Black);
                    }
                }
            }

            b.Save(@"images/ch05-sphere-shadow.png", ImageFormat.Png);
        }
    }
}
