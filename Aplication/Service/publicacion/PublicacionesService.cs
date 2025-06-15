

using Aplication.CloudinaryService;

using Aplication.Core;
using Aplication.Service.Subida;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Service.Perfil;
using Domain.Interface.Service.Publication;
using Domain.Models.perfilsmodel;
using Domain.Models.Publicacion;

namespace Aplication.Service.publicacion
{
    public class PublicacionesService : GenericService<SavePublicacionDto, Publicaciones> , IServicePublicaciones
    {
        private readonly IRepositoryPublicaciones _publicacionRepo;
        private readonly IMediafile _mediaFileService;
        private readonly CloudinaryServices _cloudinaryService;
        private readonly IMapper _mapper;


        public PublicacionesService(
       IRepositoryPublicaciones publicacionRepo,
                              IMediafile mediaFileService,
                              CloudinaryServices cloudinaryService,
                              IMapper mapper) : base (publicacionRepo, mapper)
        {
            _publicacionRepo = publicacionRepo;
            _mediaFileService = mediaFileService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task AddpublicationWithFile(SavePublicacionDto dto)
        {

            if (dto == null || dto.Archivos.Count == 0 )
            {
                throw new ArgumentException("La publicación debe contener al menos un archivo.");
            }

           
            var publicacion = _mapper.Map<Publicaciones>(dto);
            
         

            await _publicacionRepo.Add(publicacion);
            await _publicacionRepo.Save();


            foreach (var archivo in dto.Archivos) {


                string fileType = archivo.ContentType.StartsWith("image") ? "image" :
                    archivo.ContentType.StartsWith("video") ? "video" : "other" ;




                if (fileType == "other")
                {
                    throw new Exception("Tipo de archivo no soportado");
                }

               
                var mediaViewModel = new MediaFileViewModel
                {
                    File = archivo,
                    FileType = archivo.ContentType,
                    UserId = dto.UsuarioId,
                    PublicacionId = publicacion.Id
                };

                await _mediaFileService.UploadMediaFileAsync(mediaViewModel);

              


            }


            await _mediaFileService.Save();
            

        }





    }
}
