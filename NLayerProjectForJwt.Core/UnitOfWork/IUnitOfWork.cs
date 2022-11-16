using NLayerProjectForJwt.Core.Repositories;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task CommitAsync();
        void Commit();
    }
}
