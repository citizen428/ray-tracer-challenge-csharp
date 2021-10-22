namespace Raytracer.Scene;

using Raytracer.Primitives;
using static Raytracer.Primitives.Vectors;

internal record Material
{
    internal Vector<double> Color { get; init; } = RGB(1, 1, 1);
    internal double Ambient { get; init; } = 0.1;
    internal double Diffuse { get; init; } = 0.9;
    internal double Specular { get; init; } = 0.9;
    internal double Shininess { get; init; } = 200.0;

    internal Vector<double> Lighting(
        Light light,
        Vector<double> point,
        Vector<double> eyeV,
        Vector<double> normalV)
    {
        Vector<double> ambient, diffuse, specular;

        // combine the surface color with the light's color/intensity
        var effectiveColor = RGB(Color, light.Color);

        // find the direction of the light source
        var lightV = (light.Position - point).Unit();

        // compute the ambient contribution
        ambient = effectiveColor * Ambient;

        // The cosine of the angle between the light vector and the normal
        // vector. A negative number means the light is on the other
        // side of the surface.
        var lightDotNormal = lightV.DotProduct(normalV);
        if (lightDotNormal < 0)
        {
            diffuse = RGB(0);
            specular = RGB(0);
        }
        else
        {
            // compute the diffuse contribution
            diffuse = effectiveColor * Diffuse * lightDotNormal;

            var reflectV = (-lightV).Reflect(normalV);
            // The cosine of the angle between the reflection vector and the
            // eye vector. A negative number means the light reflects away
            // from the edge.
            var reflectDotEye = reflectV.DotProduct(eyeV);

            if (reflectDotEye <= 0)
            {
                specular = RGB(0);
            }
            else
            {
                // compute the specular contribution
                var factor = Math.Pow(reflectDotEye, Shininess);
                specular = light.Color * Specular * factor;
            }
        }

        return ambient + diffuse + specular;
    }
}
