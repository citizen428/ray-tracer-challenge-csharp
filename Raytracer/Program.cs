using static Raytracer.Types.Vectors;

namespace Raytracer
{
    internal record Projectile(Vector4 Position, Vector4 Velocity);
    internal record Environment(Vector4 Gravity, Vector4 Wind);

    internal static class Program
    {
        internal static Projectile Tick(Environment environment, Projectile projectile)
        {
            var position = projectile.Position + projectile.Velocity;
            var velocity = projectile.Velocity + environment.Gravity + environment.Wind;
            return new Projectile(position, velocity);
        }

        internal static void Main(string[] args)
        {
            // Projectile starts one unit above origin
            // Velocity is normalized to 1 unit/tick
            var position = Point(0f, 1f, 0f);
            var velocity = Vector4.Normalize(Vector(1f, 1f, 0f));
            var projectile = new Projectile(position, velocity);

            var gravity = Vector(0f, -0.1f, 0f);
            var wind = Vector(-0.01f, 0f, 0f);
            var environment = new Environment(gravity, wind);

            var tick = 0;

            do
            {
                Console.WriteLine($"Tick: {tick,2}, Position: {projectile.Position}");
                projectile = Tick(environment, projectile);
                tick++;
            } while (projectile.Position.Y > 0f);
        }
    }
}
