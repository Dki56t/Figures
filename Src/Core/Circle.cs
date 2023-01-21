using System;

namespace Core;

public sealed class Circle : IFigure
{
    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius should be a positive number", nameof(radius));

        var area = GetArea(radius);
        if (double.IsInfinity(area) || double.IsNaN(area))
            throw new ArgumentException("Circle can not be represented", nameof(radius));

        Radius = radius;
    }

    public double Radius { get; }

    public double GetArea()
    {
        return GetArea(Radius);
    }

    private static double GetArea(double radius)
    {
        return Math.PI * radius * radius;
    }
}