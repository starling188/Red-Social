

using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interface.Repositories
{
    public interface IRepositoryUser : IGenericRepository<User>
    {
        Task<User?> GetByCondition(Expression<Func<User, bool>> predicate);

    }
}
