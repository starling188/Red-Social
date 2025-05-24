

using Domain.Entities;
using Domain.Interface.Repositories;
using System.Linq.Expressions;


namespace Infraestructure.Repositories.Mock
{
    public class MockRepositoryUser 
    {
        private readonly List<User> _user;

        public MockRepositoryUser(List<User> user)
        {
            _user = user;
        }

       
    }
}
