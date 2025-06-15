
using Domain.Entities;
using Domain.Models.Publicacion;

namespace Domain.Interface.Service.Publication
{
    public interface IServicePublicaciones : IGenericService<SavePublicacionDto, Publicaciones>
    {
        Task AddpublicationWithFile(SavePublicacionDto dto);
    }
}
