using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();

    }
}
