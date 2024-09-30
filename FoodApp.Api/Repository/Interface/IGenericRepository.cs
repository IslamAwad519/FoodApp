using FoodApp.Api.Data.Entities;
using System.Linq.Expressions;

namespace FoodApp.Api.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> GetAsyncToInclude(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);

        Task<int> SaveChangesAsync();
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

    }
}
