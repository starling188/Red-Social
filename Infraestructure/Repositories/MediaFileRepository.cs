
using Domain.Entities;
using Domain.Interface.Repositories;
using Infraestructure.Context;
using Infraestructure.Core;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MediaFileRepository : GenericRepository<MediaFile> , IRepositoryMediaFile
    {
        public MediaFileRepository(SocialRedContext context) : base(context) { }


        public override async Task Add(MediaFile entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El archivo no puede ser nulo");
            }

            // Establecemos metadatos antes de guardar
            await base.Add(entity);
            
        }

        public override Task Save()
        {
            return base.Save();
        }





    }

}
