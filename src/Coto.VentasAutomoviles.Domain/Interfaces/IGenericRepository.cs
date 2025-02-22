using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Utilities;

namespace Coto.VentasAutomoviles.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{    
    Task<Result<T>> AddAsync(T entity);
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);    
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);    
}
