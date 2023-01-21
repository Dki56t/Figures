using System;
using System.Threading.Tasks;
using API.Model;
using API.Model.Figures;
using Core;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FiguresController : ControllerBase
{
    private readonly IFiguresRepository _figureRepository;

    public FiguresController(IFiguresRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StoreAsync([FromBody] IFigureDto figureDto)
    {
        if (figureDto == null)
            throw new ArgumentNullException(nameof(figureDto));

        IFigure figure = figureDto switch
        {
            CircleDto circleDto => new Circle(circleDto.Radius),
            TriangleDto triangleDto => new Triangle(triangleDto.A, triangleDto.B, triangleDto.C),
            _ => throw new InvalidOperationException(
                $"Controller does not support figures with the type {figureDto.GetType()}")
        };

        var id = await _figureRepository.StoreAsync(figure).ConfigureAwait(true);

        return CreatedAtAction("Get", new { id }, figureDto);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IFigureDto> GetAsync(long id)
    {
        var figure = await _figureRepository.GetByIdAsync<IFigure>(id).ConfigureAwait(true);

        return figure switch
        {
            Circle circle => new CircleDto
            {
                Radius = circle.Radius
            },
            Triangle triangle => new TriangleDto
            {
                A = triangle.A,
                B = triangle.B,
                C = triangle.C
            },
            _ => throw new NotSupportedException($"Type of figure {id} is not supported")
        };
    }

    [HttpGet("{id:long}/area")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetFigureAreaResultDto> GetAreaAsync(long id)
    {
        var figure = await _figureRepository.GetByIdAsync<IFigure>(id).ConfigureAwait(true);

        return new GetFigureAreaResultDto
        {
            Area = figure.GetArea()
        };
    }
}