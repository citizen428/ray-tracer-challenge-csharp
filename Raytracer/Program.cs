namespace Raytracer;

using System.Drawing;
using System.Drawing.Imaging;
using Raytracer.Primitives;
using Raytracer.Scene;
using static Raytracer.Primitives.Vectors;

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
        var material = new Material() { Color = RGB(1, 0.2, 1) };
        var sphere = new Sphere(material);
        // Light behind, above and to the left of the eye
        var lightPosition = Point(-10, 10, -10);
        var light = new Light(lightPosition, RGB(1));

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
                var rayDirection = (position - rayOrigin).Unit();
                var ray = new Ray(rayOrigin, rayDirection);

                var xs = sphere.Intersect(ray);
                var hit = Intersection.Hit(xs);
                if (hit is not null)
                {
                    var point = ray.Position(hit.T);
                    var hitSphere = (Sphere)hit.Object;
                    var eyeV = -ray.Direction;
                    var normalV = hitSphere.NormalAt(point);
                    var rgb = hitSphere.Material.Lighting(light, point, eyeV, normalV);

                    b.SetPixel(x, y, rgb.ToColor());
                }
            }
        }

        b.Save(@"images/ch06-sphere-lighting.png", ImageFormat.Png);
    }
}
