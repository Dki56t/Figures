using System;
using API.Converters;
using API.Model.Figures;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Tests.UnitTests.API
{
    public sealed class FigureConverterTests
    {
        [Fact]
        public void ShouldConvertJsonToTriangleDto()
        {
            const string json = "{\"type\": \"triangle\",\"A\":3,\"B\":4,\"C\":5}";

            var triangle = Should.NotThrow(() => DeserializeJson(json)) as TriangleDto;

            triangle.ShouldNotBeNull();
            triangle.A.ShouldBe(3);
            triangle.B.ShouldBe(4);
            triangle.C.ShouldBe(5);
        }

        [Fact]
        public void ShouldConvertJsonToCircleDto()
        {
            const string json = "{\"type\": \"circle\",\"radius\":3}";

            var circle = Should.NotThrow(() => DeserializeJson(json)) as CircleDto;

            circle.ShouldNotBeNull();
            circle.Radius.ShouldBe(3);
        }

        [Fact]
        public void ShouldThrowIfTypeIsNotSupported()
        {
            const string json = "{\"type\": \"square\",\"A\":3}";

            var exception = Should.Throw<InvalidOperationException>(() => DeserializeJson(json));

            exception.Message.ShouldBe("Type of figure is not defined or is not supported");
        }

        [Fact]
        public void ShouldThrowIfTypeIsNotSpecified()
        {
            const string json = "{\"A\":3}";

            var exception = Should.Throw<InvalidOperationException>(() => DeserializeJson(json));

            exception.Message.ShouldBe("Type of figure is not defined or is not supported");
        }

        private static IFigureDto DeserializeJson(string json)
        {
            return JsonConvert.DeserializeObject<IFigureDto>(json, new FigureConverter());
        }
    }
}