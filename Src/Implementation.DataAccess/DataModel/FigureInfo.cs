using System.ComponentModel.DataAnnotations;
using Core;

namespace Implementation.DataAccess.DataModel;

public class FigureInfo
{
    public long Id { get; set; }

    [Required] public IFigure Figure { get; set; }
}