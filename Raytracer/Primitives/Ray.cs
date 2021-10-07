namespace Raytracer.Primitives
{
    internal record Ray(Vector<double> Origin, Vector<double> Direction)
    {
        internal Vector<double> Position(double distance)
        {
            return Origin + (Direction * distance);
        }

        internal Ray Transform(Matrix<double> m)
        {
            return new Ray(m * Origin, m * Direction);
        }
    }
}
