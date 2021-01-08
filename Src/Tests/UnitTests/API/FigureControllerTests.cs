using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Model.Figures;
using Core;
using Infrastructure.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Tests.UnitTests.API
{
    public sealed class FigureControllerTests
    {
        private const double Precision = 0.000000001;

        [Fact]
        public async Task ShouldStorePassedFigure()
        {
            const long figureId = 42;

            var repository = Substitute.For<IFigureRepository>();
            repository.StoreAsync(Arg.Any<IFigure>()).Returns(Task.FromResult(figureId));

            var controller = new FigureController(repository);

            var result = await controller.StoreFigureAsync(new TriangleDto
            {
                A = 3,
                B = 4,
                C = 5
            }).ConfigureAwait(false);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(figureId);

            await repository.Received(1).StoreAsync(Arg.Is<Triangle>(t =>
                    Math.Abs(t.A - 3) < Precision && Math.Abs(t.B - 4) < Precision &&
                    Math.Abs(t.C - 5) < Precision))
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldReturnFiguresArea()
        {
            var repository = Substitute.For<IFigureRepository>();
            repository.GetByIdAsync<IFigure>(Arg.Any<long>()).Returns(new Triangle(3, 4, 5));

            var controller = new FigureController(repository);

            var result = await controller.GetFigureAreaAsync(42).ConfigureAwait(false);

            result.ShouldNotBeNull();
            Math.Abs(result.Area - 6).ShouldBeLessThan(Precision);
        }
    }
}