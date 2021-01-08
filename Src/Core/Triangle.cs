using System;

namespace Core
{
    public sealed class Triangle : IFigure
    {
        public Triangle(double a, double b, double c)
        {
            const string wrongArgumentMessage = "Length of a side of the triangle should be a positive number";

            if (a <= 0)
                throw new ArgumentException(wrongArgumentMessage, nameof(a));

            if (b <= 0)
                throw new ArgumentException(wrongArgumentMessage, nameof(b));

            if (c <= 0)
                throw new ArgumentException(wrongArgumentMessage, nameof(c));

            var area = GetArea(a, b, c);
            if (double.IsInfinity(area) || double.IsNaN(area))
                throw new ArgumentException("Triangle can not be represented");

            A = a;
            B = b;
            C = c;
        }

        public double A { get; }
        public double B { get; }
        public double C { get; }

        public double GetArea()
        {
            return GetArea(A, B, C);
        }

        private static double GetArea(double a, double b, double c)
        {
            var p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }
}