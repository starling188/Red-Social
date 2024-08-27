

namespace Domain.Interface.Service
{
    public interface IGenericService <TModel, TEntity> where TEntity  : class
    {
        Task<TModel> GetById(int id);
        Task<List<TModel>> GetAll();
        Task Add(TModel entity);
        Task AddList(List<TModel> entity);
        Task Update(TModel entity);
        Task Delete(int id);
        Task Save();
    }
}
