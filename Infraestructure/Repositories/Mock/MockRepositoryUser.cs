

using Domain.Entities;
using Domain.Interface.Repositories;
using System.Linq.Expressions;


namespace Infraestructure.Repositories.Mock
{
    public class MockRepositoryUser : IRepositoryUser
    {
        private readonly List<User> _user;

        public MockRepositoryUser(List<User> user)
        {
            _user = user;
        }

        public async Task Add(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El usuario no puede ser nulo");
            }

            _user.Add(entity);
            await Task.CompletedTask;

        }

        public Task AddList(List<User> entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByCondition(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
