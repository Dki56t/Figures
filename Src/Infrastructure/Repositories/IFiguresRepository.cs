using System.Threading.Tasks;
using Core;

namespace Infrastructure.Repositories;

public interface IFiguresRepository
{
    Task<long> StoreAsync(IFigure figure);

    Task<T> GetByIdAsync<T>(long id) where T : class, IFigure;
}