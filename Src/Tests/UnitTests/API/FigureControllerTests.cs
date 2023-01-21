using System;
using API.Converters;
using API.Model.Figures;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Tests.UnitTests.API;

public sealed class FiguresConverterTests
{
    [Fact]
    public void ShouldConvertJsonToTriangleDto()
    {
        const string json = "{\"type\":\"triangle\",\"a\":3.0,\"b\":4.0,\"c\":5.0}";

        var triangle = Should.NotThrow(() => DeserializeJson(json)) as TriangleDto;

        triangle.ShouldNotBeNull();
        triangle.A.ShouldBe(3);
        triangle.B.ShouldBe(4);
        triangle.C.ShouldBe(5);
    }

    [Fact]
    public void ShouldConvertJsonToCircleDto()
    {
        const string json = "{\"type\":\"circle\",\"radius\":3.0}";

        var circle = Should.NotThrow(() => DeserializeJson(json)) as CircleDto;

        circle.ShouldNotBeNull();
        circle.Radius.ShouldBe(3);
    }

    [Fact]
    public void ShouldConvertTriangleDtoToJson()
    {
        var json = Should.NotThrow(() => SerializeJson(new TriangleDto
        {
            A = 3,
            B = 4,
            C = 5
        }));

        json.ShouldBe("{\"type\":\"triangle\",\"a\":3.0,\"b\":4.0,\"c\":5.0}");
    }

    [Fact]
    public void ShouldNotExtendRandomObjectWithType()
    {
        var json = Should.NotThrow(() => SerializeJson(new
        {
            A = 3,
            B = 4,
            C = 5
        }));

        json.ShouldBe("{\"A\":3,\"B\":4,\"C\":5}");
    }

    [Fact]
    public void ShouldConvertCircleDtoToJson()
    {
        var json = Should.NotThrow(() => SerializeJson(new CircleDto
        {
            Radius = 3
        }));

        json.ShouldBe("{\"type\":\"circle\",\"radius\":3.0}");
    }

    [Fact]
    public void ShouldThrowIfTypeIsNotSupported()
    {
        const string json = "{\"type\":\"square\",\"a\":3.0}";

        var exception = Should.Throw<InvalidOperationException>(() => DeserializeJson(json));

        exception.Message.ShouldBe("'square' type of figure is not defined or is not supported");
    }

    [Fact]
    public void ShouldThrowIfTypeIsNotSpecified()
    {
        const string json = "{\"a\":3.0}";

        var exception = Should.Throw<InvalidOperationException>(() => DeserializeJson(json));

        exception.Message.ShouldBe("Type property is not present in json");
    }

    private static IFigureDto DeserializeJson(string json)
    {
        return JsonConvert.DeserializeObject<IFigureDto>(json, new FigureConverter());
    }

    private static string SerializeJson(object value)
    {
        return JsonConvert.SerializeObject(value, Formatting.None, new FigureConverter());
    }
}