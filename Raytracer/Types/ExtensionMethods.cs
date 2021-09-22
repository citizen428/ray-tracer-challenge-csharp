namespace Raytracer.Types
{
    internal static class Vector4Extensions
    {
        public static Vector4 Cross(this Vector4 v1, Vector4 v2)
        {
            var x = (v1.Y * v2.Z) - (v2.Y * v1.Z);
            var y = ((v1.X * v2.Z) - (v2.X * v1.Z)) * -1;
            var z = (v1.X * v2.Y) - (v2.X * v1.Y);

            return new Vector4(x, y, z, 0.0f);
        }
    }
}
