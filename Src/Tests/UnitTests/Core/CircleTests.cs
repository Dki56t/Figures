using System;
using Core;
using Shouldly;
using Xunit;

namespace Tests.UnitTests.Core;

public sealed class CircleTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(double.MaxValue)]
    public void ShouldThrowIfArgumentsAreIncorrect(double radius)
    {
        Should.Throw<ArgumentException>(() => new Circle(radius));
    }

    [Theory]
    [InlineData(1, Math.PI)]
    [InlineData(2, 4 * Math.PI)]
    public void ShouldGetExpectedArea(double radius, double area)
    {
        new Circle(radius).GetArea().ShouldBe(area);
    }
}