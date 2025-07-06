
using AutoMapper;
using Domain.Entities;
using Aplication.Dtos.users;
using Aplication.Dtos.Login;
using Aplication.Dtos.perfilsmodel;
using Aplication.Dtos.Publicacion;


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

               
                con.CreateMap<User, PerfilUsuarioDto>().
                ForMember(dest => dest.CantidadSeguidores, opt => opt.MapFrom(src => src.Amistades.Count)). 
                ForMember(dest => dest.CantidadArchivosMultimedia, opt => opt.MapFrom(src => src.UserMediaFiles.Count)).
                ForMember(dest => dest.CantidadPublicaciones, opt => opt.MapFrom(src => src.Publicaciones.Count))
                .ForMember(dest => dest.RutasMultimedia, opt => opt.MapFrom(src => src.UserMediaFiles.Select(m => m.FilePath)))
                .ForMember(dest => dest.Publicaciones, opt => opt.Ignore()); // NUEVO: Ignorar porque lo mapearemos manualmente

                con.CreateMap<User, SearchUserDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FotoPerfil, opt => opt.MapFrom(src => src.FotoPerfil));


                con.CreateMap<SavePublicacionDto, Publicaciones>()
                
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "Sistema"))

                .ForMember(dest => dest.UsuarioID, opt => opt.MapFrom(src => src.UsuarioId));


                con.CreateMap<MediaFileViewModel, MediaFile>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "Sistema"))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.FileType))
                .ForMember(dest => dest.FilePath, opt => opt.Ignore()); // Lo asignas manualmente con la URL de Cloudinary


            });

            return config.CreateMapper();
        }
    }
}
