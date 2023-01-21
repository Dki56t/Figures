using System;
using Core;
using Shouldly;
using Xunit;

namespace Tests.UnitTests.Core;

public class TriangleTests
{
    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(1, double.MaxValue, double.MaxValue)]
    [InlineData(double.MaxValue, 1, double.MaxValue)]
    [InlineData(double.MaxValue, double.MaxValue, 1)]
    [InlineData(100, 1, 1)]
    public void ShouldThrowIfArgumentsAreIncorrect(double a, double b, double c)
    {
        Should.Throw<ArgumentException>(() => new Triangle(a, b, c));
    }

    [Theory]
    [InlineData(3, 4, 5, 6)]
    [InlineData(13, 14, 15, 84)]
    public void ShouldGetExpectedArea(double a, double b, double c, double area)
    {
        new Triangle(a, b, c).GetArea().ShouldBe(area);
    }
}