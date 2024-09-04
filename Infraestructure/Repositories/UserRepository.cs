

using Domain.Entities;
using Domain.Interface.Repositories;
using Infraestructure.Context;
using Infraestructure.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Infraestructure.Exeception.NotFoundExeception;


namespace Infraestructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IRepositoryUser
    {
        

        public UserRepository(SocialRedContext context): base(context) {  }

        public override async Task Add(User entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El usuario no puede ser nulo");
            }

            await base.Add(entity);
        }

        public override async Task AddList(List<User> entities)
        {
            if(entities == null)
            {
                throw new ArgumentNullException(nameof(entities),"Los usuarios agregados no pueden ser nulos");
            }

            await base.AddList(entities);
        }

        public override async Task Delete(int id)
        {
            var entidad = await GetById(id);
            if(entidad == null)
            {
                throw new NotFoundException($"el id: {id}, no se a encontrado");
            }

            entidad.IsActive = false;
            _context.Update(entidad);
           
        }


        public override async Task<List<User>> GetAll()
        {
            var user = await _context.Users.Where(c  => c.IsActive).ToListAsync();
            if(user == null)
            {
                throw new NotFoundException("no hay usuarios activados");
            }

            return user;
        }

        public override async Task<User> GetById(int id)
        {
            var user = await base.GetById(id);
            if(user == null)
            {
                throw new NotFoundException($" el Id: {id}, no se a encontrado");
            }

            return user;
        }

        public override Task Save()
        {
            return base.Save();
        }

        public override async Task Update(User entity)
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

            _context.Entry(existingCliente).CurrentValues.SetValues(entity);
        }


        //Metodos adicionales

        public async Task<User?> GetByCondition(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .AsNoTracking() // No rastrea el cambio para consultas de solo lectura
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
