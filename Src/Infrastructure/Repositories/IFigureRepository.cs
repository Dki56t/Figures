using System.Threading.Tasks;
using Core;

namespace Infrastructure.Repositories
{
    public interface IFigureRepository
    {
        Task<long> StoreAsync(IFigure figure);

        Task<T> GetByIdAsync<T>(long id) where T : IFigure;
    }
}