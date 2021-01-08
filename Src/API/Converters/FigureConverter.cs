using System;
using API.Model.Figures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Converters
{
    public sealed class FigureConverter : JsonCreationConverter<IFigureDto>
    {
        protected override IFigureDto Create(Type objectType, JObject jObject)
        {
            const string typePropertyName = "type";

            if ("triangle".Equals(jObject.Value<string>(typePropertyName), StringComparison.OrdinalIgnoreCase))
                return new TriangleDto();

            if ("circle".Equals(jObject.Value<string>(typePropertyName), StringComparison.OrdinalIgnoreCase))
                return new CircleDto();

            throw new InvalidOperationException("Type of figure is not defined or is not supported");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Writing of json is not supported by this converter");
        }
    }
}