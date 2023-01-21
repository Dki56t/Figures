using System;
using System.Linq;
using API.Model.Figures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace API.Converters;

public sealed class FigureConverter : JsonCreationConverter<IFigureDto>
{
    private const string TypePropertyName = "type";

    private static readonly JsonSerializer Serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    });

    private static readonly FigureDtoTypeMapItem[] TypeMap =
        typeof(IFigureDto).Assembly.GetTypes()
            .Where(t => typeof(IFigureDto).IsAssignableFrom(t))
            .Select(t => new FigureDtoTypeMapItem(t, t.Name.ToLowerInvariant().Replace("dto", string.Empty)))
            .ToArray();

    public override bool CanWrite => true;

    protected override IFigureDto Create(Type objectType, JObject jObject)
    {
        var type = jObject.Value<string>(TypePropertyName);
        if (string.IsNullOrWhiteSpace(type))
            throw new InvalidOperationException("Type property is not present in json");

        var map = TypeMap.SingleOrDefault(m => m.TypeName.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (map == null)
            throw new InvalidOperationException($"'{type}' type of figure is not defined or is not supported");

        return (IFigureDto) Activator.CreateInstance(map.Type);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var map = TypeMap.SingleOrDefault(m => m.Type == value.GetType());
        if (map == null)
            throw new InvalidOperationException($"'{value.GetType()}' type of figure is not supported");

        var property = new JProperty(TypePropertyName, map.TypeName);
        var jObject = new JObject(property);
        property.AddAfterSelf(JObject.FromObject(value, Serializer).Properties());

        serializer.Serialize(writer, jObject);
    }

    private sealed class FigureDtoTypeMapItem
    {
        public FigureDtoTypeMapItem(Type type, string typeName)
        {
            Type = type;
            TypeName = typeName;
        }

        public Type Type { get; }
        public string TypeName { get; }
    }
}