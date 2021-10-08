using static Raytracer.Primitives.Vectors;

namespace Raytracer.Primitives
{
    internal class Sphere : Shape
    {
        private readonly double radius = 1;
        private readonly Vector<double> origin = Point(0, 0, 0);

        internal Intersection[] Intersect(Ray ray)
        {
            var ray2 = ray.Transform(Transform.Inverse());

            // sphere is centered at world origin
            var sphereToRay = ray2.Origin - origin;
            var a = ray2.Direction.DotProduct(ray2.Direction);
            var b = 2 * ray2.Direction.DotProduct(sphereToRay);
            var c = sphereToRay.DotProduct(sphereToRay) - radius;
            var discriminant = (b * b) - (4 * a * c);
            if (discriminant < 0)
            {
                return Array.Empty<Intersection>();
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return new Intersection[] {
                new(t1, this),
                new(t2, this)
            };
        }

        internal Vector<double> NormalAt(Vector<double> worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = objectPoint - origin;
            var wn = Transform.Inverse().Transpose() * objectNormal;
            // resetting w to 0
            var worldNormal = Vector(wn.X(), wn.Y(), wn.Z());
            return worldNormal.Unit();
        }
    }
}
