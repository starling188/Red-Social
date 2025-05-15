
using AutoMapper;
using Domain.Entities;
using Domain.Models.User;

using Domain.Models;
using Domain.Models.perfilsmodel;


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

                con.CreateMap<MediaFileViewModel, MediaFile>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => $"{Path.GetFileNameWithoutExtension(src.File.FileName)}_{Guid.NewGuid()}")) // Generar nombre único
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => $"{Path.Combine("uploads", Path.GetFileName(src.File.FileName))}")); // Establecer la ruta
            });

            return config.CreateMapper();
        }
    }
}
