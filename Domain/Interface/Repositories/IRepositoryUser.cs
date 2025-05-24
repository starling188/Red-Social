

using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interface.Repositories
{
    public interface IRepositoryUser : IGenericRepository<User>
    {
        Task<List<User>> SearchUsersByUserNameAsync(string partialUsername);

        Task<User> GetProfileWithDataAsync(string username);
        Task<User?> GetByCondition(Expression<Func<User, bool>> predicate);
        Task<User> GetByUsernameAsync(string username);
    }
}
