using API.Converters;
using Newtonsoft.Json;

namespace API.Model.Figures
{
    [JsonConverter(typeof(FigureConverter))]
    public interface IFigureDto
    {
    }
}