

using Domain.Entities;
using Domain.Interface.Repositories;
using Infraestructure.Context;
using Infraestructure.Core;
using Microsoft.EntityFrameworkCore;
using static Infraestructure.Exeception.NotFoundExeception;

namespace Infraestructure.Repositories
{
    public class ComentariosRepository : GenericRepository<Comentarios>, IRepositoryComentarios
    {
        public ComentariosRepository(SocialRedContext context) : base(context) { }


        public override async Task Add(Comentarios entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El usuario no puede ser nulo");
            }

            await base.Add(entity);
        }

        public override async Task AddList(List<Comentarios> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), "Los usuarios agregados no pueden ser nulos");
            }

            await base.AddList(entities);
        }

        public override async Task Delete(int id)
        {
            var entidad = await GetById(id);
            if (entidad == null)
            {
                throw new NotFoundException($"el id: {id}, no se a encontrado");
            }

            entidad.IsActive = false;
            _context.Update(entidad);
        }


        public override async Task<List<Comentarios>> GetAll()
        {
            var user = await _context.Comentarios.Where(c => c.IsActive).ToListAsync();
            if (user == null)
            {
                throw new NotFoundException("no hay usuarios activados");
            }

            return user;
        }

        public override async Task<Comentarios> GetById(int id)
        {
            var user = await base.GetById(id);
            if (user == null)
            {
                throw new NotFoundException($" el Id: {id}, no se a encontrado");
            }

            return user;
        }

        public override Task Save()
        {
            return base.Save();
        }

        public override async Task Update(Comentarios entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El usuario no puede ser nulo");
            }

            var existingCliente = await GetById(entity.Id);
            if (existingCliente == null)
            {
                throw new NotFoundException($"No se puede actualizar el usuario con ID {entity.Id} porque no existe en la base de datos");
            }

            await base.Update(entity);
        }

    }
}
