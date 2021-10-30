namespace Raytracer.Scene;

using System.Drawing;
using Raytracer.Primitives;
using static Raytracer.Primitives.Matrices;
using static Raytracer.Primitives.Vectors;

internal class Camera
{
    internal int HSize { get; }
    internal int VSize { get; }
    internal double FieldOFView { get; }
    internal Matrix<double> Transform { get; }
    internal double PixelSize { get; }

    private readonly double HalfWidth;
    private readonly double HalfHeight;

    internal Camera(int hsize, int vsize, double fov, Matrix<double>? transform = null)
    {
        HSize = hsize;
        VSize = vsize;
        FieldOFView = fov;
        Transform = transform ?? IdentityMatrix();

        var halfView = Math.Tan(fov / 2);
        var aspect = (double)hsize / vsize;

        if (aspect >= 1)
        {
            HalfWidth = halfView;
            HalfHeight = halfView / aspect;
        }
        else
        {
            HalfWidth = halfView * aspect;
            HalfHeight = halfView;
        }

        PixelSize = HalfWidth * 2 / (float)hsize;
    }

    internal Ray RayForPixel(int x, int y)
    {
        var xOffset = ((float)x + 0.5) * PixelSize;
        var yOffset = ((float)y + 0.5) * PixelSize;

        // The untransformed coordinates of the pixel in world space
        // Camera looks towards -z, so +x is to the left
        var worldX = HalfWidth - xOffset;
        var worldY = HalfHeight - yOffset;

        var inverse_transform = Transform.Inverse();
        var pixel = inverse_transform * Point(worldX, worldY, -1);
        var origin = inverse_transform * Point(0, 0, 0);
        var direction = (pixel - origin).Unit();

        return new Ray(origin, direction);
    }

    internal Bitmap Render(World world)
    {
        var b = new Bitmap(HSize, VSize);
        using (var g = Graphics.FromImage(b))
        {
            g.Clear(Color.White);
        }

        foreach (var y in Enumerable.Range(0, VSize - 1))
        {
            foreach (var x in Enumerable.Range(0, HSize - 1))
            {
                var ray = RayForPixel(x, y);
                var rgb = world.ColorAt(ray);

                b.SetPixel(x, y, rgb.ToColor());
            }
        }

        return b;
    }
}
