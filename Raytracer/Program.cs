using System.Drawing;
using System.Drawing.Imaging;
using static Raytracer.Types.Vectors;

namespace Raytracer
{
    internal record Projectile(Vector4 Position, Vector4 Velocity);

    internal record Environment(Vector4 Gravity, Vector4 Wind);

    internal static class Program
    {
        private const int ImageWidth = 900;
        private const int ImageHeight = 550;

        internal static Projectile Tick(Environment environment, Projectile projectile)
        {
            var position = projectile.Position + projectile.Velocity;
            var velocity = projectile.Velocity + environment.Gravity + environment.Wind;
            return new Projectile(position, velocity);
        }

        internal static void Main()
        {
            // Projectile starts one unit above origin
            // Velocity is normalized to 1 unit/tick
            var start = Point(0f, 1f, 0f);
            var velocity = Vector4.Normalize(Vector(1f, 1.8f, 0f)) * 11.25f;
            Projectile? projectile = new(start, velocity);

            var gravity = Vector(0f, -0.1f, 0f);
            var wind = Vector(-0.01f, 0f, 0f);
            Environment? environment = new(gravity, wind);

            using Bitmap? b = new(ImageWidth, ImageHeight);
            using (var g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
            }

            do
            {
                var x = (int)projectile.Position.X;
                var y = ImageHeight - (int)projectile.Position.Y;
                b.SetPixel(x, y, Color.Red);
                projectile = Tick(environment, projectile);
            } while (projectile.Position.Y > 0f);

            b.Save(@"images\ch02-projectile.png", ImageFormat.Png);
        }
    }
}
