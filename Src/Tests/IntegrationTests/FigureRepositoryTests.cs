using System.Threading.Tasks;
using Autofac;
using Core;
using Infrastructure.Repositories;
using Shouldly;
using Tests.IntegrationTests.Fixtures;
using Xunit;

namespace Tests.IntegrationTests;

public sealed class FigureRepositoryTests : IClassFixture<DataAccessModuleFixture>
{
    private readonly IFiguresRepository _repository;

    public FigureRepositoryTests(DataAccessModuleFixture fixture)
    {
        _repository = fixture.Container.Resolve<IFiguresRepository>();
    }

    [Fact]
    public async Task ShouldStoreTriangleAndThenGetItById()
    {
        var triangle = new Triangle(3, 5, 7);
        var triangleId = await _repository.StoreAsync(triangle).ConfigureAwait(false);

        var storedTriangle = await _repository.GetByIdAsync<Triangle>(triangleId).ConfigureAwait(false);

        storedTriangle.ShouldNotBeNull();
        storedTriangle.A.ShouldBe(triangle.A);
        storedTriangle.B.ShouldBe(triangle.B);
        storedTriangle.C.ShouldBe(triangle.C);
    }

    [Fact]
    public async Task ShouldStoreCircleAndThenGetItById()
    {
        var circle = new Circle(3);
        var circleId = await _repository.StoreAsync(circle).ConfigureAwait(false);

        var storedCircle = await _repository.GetByIdAsync<Circle>(circleId).ConfigureAwait(false);

        storedCircle.ShouldNotBeNull();
        storedCircle.Radius.ShouldBe(circle.Radius);
    }
}