namespace Raytracer.Types
{
    internal class Vectors
    {
        internal static Vector4 Point(float x, float y, float z)
        {
            return new Vector4(x, y, z, 1f);
        }

        internal static Vector4 Vector(float x, float y, float z)
        {
            return new Vector4(x, y, z, 0f);
        }

        internal static Vector3 RGB(float r, float g, float b)
        {
            return new Vector3(r, g, b);
        }
    }
}
