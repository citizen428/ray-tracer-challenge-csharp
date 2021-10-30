namespace Raytracer;

using System.Drawing.Imaging;
using Raytracer.Primitives;
using Raytracer.Scene;
using static Raytracer.Primitives.Vectors;

internal static class Program
{
    internal static void Main()
    {
        var floor = new Sphere()
        {
            Transform = Transformation.Scaling(10, 0.01, 10),
            Material = new Material()
            {
                Color = RGB(1, 0.9, 0.9),
                Specular = 0
            }
        };

        var leftWall = new Sphere()
        {
            Transform =
                Transformation.Translation(0, 0, 5) *
                Transformation.RotationY(-Math.PI / 4) *
                Transformation.RotationX(Math.PI / 2) *
                Transformation.Scaling(10, 0.01, 10),

            Material = floor.Material
        };

        var rightWall = new Sphere()
        {

            Transform =
                Transformation.Translation(0, 0, 5) *
                Transformation.RotationY(Math.PI / 4) *
                Transformation.RotationX(Math.PI / 2) *
                Transformation.Scaling(10, 0.01, 10),
            Material = floor.Material
        };

        var middle = new Sphere()
        {
            Transform = Transformation.Translation(-0.5, 1, 0.5),
            Material = new Material()
            {
                Color = RGB(0.1, 1, 0.5),
                Diffuse = 0.7,
                Specular = 0.3
            }
        };

        var right = new Sphere()
        {
            Transform = Transformation.Translation(1.5, 0.5, -0.5) *
                Transformation.Scaling(0.5, 0.5, 0.5),
            Material = new Material()
            {
                Color = RGB(0.5, 1, 0.1),
                Diffuse = 0.7,
                Specular = 0.3
            }
        };

        var left = new Sphere()
        {
            Transform = Transformation.Translation(-1.5, 0.44, -0.75) *
                Transformation.Scaling(0.33, 0.33, 0.33),
            Material = new Material()
            {
                Color = RGB(1, 0.8, 0.1),
                Diffuse = 0.7,
                Specular = 0.3
            }
        };

        var light = new Light(Point(-10, 10, -10), RGB(1, 1, 1));

        var cameraT = Transformation.View(Point(0, 1.5, -5), Point(0, 1, 0), Vector(0, 1, 0));
        var camera = new Camera(1000, 500, Math.PI / 3, cameraT);

        var objects = new Shape[] { floor, leftWall, rightWall, middle, right, left };
        var world = new World(objects, light);

        using var b = camera.Render(world);
        b.Save(@"images/ch07-scene.png", ImageFormat.Png);
    }
}
