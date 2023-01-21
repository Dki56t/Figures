using System;
using System.Threading.Tasks;
using Core;
using Implementation.DataAccess.DataModel;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Implementation.DataAccess.Repositories;

public sealed class FigureRepository : IFiguresRepository
{
    private readonly ContextFactory _contextFactory;

    public FigureRepository(ContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<long> StoreAsync(IFigure figure)
    {
        if (figure == null)
            throw new ArgumentNullException(nameof(figure));

        var info = new FigureInfo
        {
            Figure = figure
        };

        await using var uow = _contextFactory.CreateContext();

        // ReSharper disable once MethodHasAsyncOverload - sync overload is preferred here as per docs
        uow.FigureInfos.Add(info);

        await uow.SaveChangesAsync().ConfigureAwait(false);

        return info.Id;
    }

    public async Task<T> GetByIdAsync<T>(long id) where T : class, IFigure
    {
        await using var uow = _contextFactory.CreateContext();

        var info = await uow.FigureInfos.FindAsync(id).ConfigureAwait(false);

        if (info?.Figure == null)
            throw new EntityNotFoundException($"There is no figure associated with id {id}");

        if (!(info.Figure is T figure))
            throw new InvalidOperationException(
                $"Figure with specified id is of type '{info.Figure.GetType()}' instead of '{typeof(T)}'");

        return figure;
    }
}