


using Domain.Entities;
using AutoMapper;
using Domain.Interface.Service;
using Infraestructure.Repositories.Mock;
using Aplication.Service;
using Aplication.Dtos;
using Domain.Models.User;
using Domain.Interface.Repositories;


namespace RedSocialUnit.Test.Users
{
    public class UserServiceTest
    {

        private readonly List<User> _users;

        private readonly IServiceUser _UserService;
        private readonly IMapper _mapper;
        private readonly IRepositoryLogin _log;

        public UserServiceTest()
        {
            _users = new List<User>();

            _mapper = Automaper.Configure();
            
            

            var userRepo = new MockRepositoryUser(_users);

           // _UserService = new UserServices(userRepo, _mapper, _log);
             
        }

        [Fact]

        public async Task Add_ShouldAddUserToList()
        {
            // Arrange
            var model = new SaveUserModel
            {
                Nombre = "Test User",
                Correo = "testuser@example.com",
                Apellido= "matias"
                
            };

            // Act
            await _UserService.Add(model);

            // Assert
            Assert.Single(_users); // Verifica que se haya añadido un usuario
            var user = _users[0];
            Assert.Equal("Test User", user.Nombre);
            Assert.Equal("testuser@example.com", user.Correo);
        }




    }
}
