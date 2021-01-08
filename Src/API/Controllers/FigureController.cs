using System;
using System.Threading.Tasks;
using API.Model;
using API.Model.Figures;
using Core;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FigureController : ControllerBase
    {
        private readonly IFigureRepository _figureRepository;

        public FigureController(IFigureRepository figureRepository)
        {
            _figureRepository = figureRepository;
        }

        [HttpPost]
        public async Task<FigureCreationResultDto> StoreFigureAsync([FromBody] IFigureDto figureDto)
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

            return new FigureCreationResultDto
            {
                Id = id
            };
        }

        [HttpGet("{id}")]
        public async Task<GetFigureAreaResultDto> GetFigureAreaAsync(long id)
        {
            var figure = await _figureRepository.GetByIdAsync<IFigure>(id).ConfigureAwait(true);

            return new GetFigureAreaResultDto
            {
                Area = figure.GetArea()
            };
        }
    }
}