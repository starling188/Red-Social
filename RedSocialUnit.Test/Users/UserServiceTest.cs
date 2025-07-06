using Aplication.Service.user;
using Aplication.ValidationAcount;
using AutoMapper;

using Domain.Interface.Repositories;
using Aplication.Interface.EmailServices;

namespace RedSocialUnit.Test.Users
{
    public class UserServiceTest
    {

        private readonly IRepositoryUser _userRepoMock;
        private readonly IMapper _mapperMock;
        private readonly IRepositoryLogin _loginRepoMock;
        private readonly IEmailService _emailServiceMock;
        private readonly TokenService _tokenServiceMock;
        private readonly UserServices _userService;



        public UserServiceTest(IRepositoryUser use )
        {
           

           

        }

        

    




    }
}
