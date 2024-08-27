

using AutoMapper;
using Domain.Interface.Repositories;
using Domain.Interface.Service;

namespace Aplication.Core
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repo , IMapper mapp)
        {
            _mapper = mapp;
            _repository = repo;
        }

        public virtual async Task Add(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.Add(entity);
        }

        public Task AddList(List<TModel> entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Save()
        {
            await _repository.Save();
        }

        public Task Update(TModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
