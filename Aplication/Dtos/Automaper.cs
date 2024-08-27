
using AutoMapper;
using Domain.Entities;
using Domain.Models.User;
using WebRed.Models;

namespace Aplication.Dtos
{
    public class Automaper
    {

        public static IMapper Configure()
        {
            var config = new MapperConfiguration(con => {


                con.CreateMap<SaveUserModel, User>();
                con.CreateMap<InicioModel, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Contrasena.ToString())); // Asegúrate de que el tipo de `Contrasena` se convierta al tipo de `Password`

            });

            return config.CreateMapper();
        }
    }
}
