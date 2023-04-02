namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(Guid id);
    }
}
