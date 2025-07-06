
using Domain.Entities;
using Aplication.Dtos.Publicacion;

namespace Aplication.Interface.Publication
{
    public interface IServicePublicaciones : IGenericService<SavePublicacionDto, Publicaciones>
    {
        Task AddpublicationWithFile(SavePublicacionDto dto);
    }
}
