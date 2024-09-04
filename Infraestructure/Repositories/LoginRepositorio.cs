

using Domain.Entities;
using Domain.Interface.Repositories;
using Infraestructure.Context;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace Infraestructure.Repositories
{
    public class LoginRepositorio : IRepositoryLogin

    {
        private readonly SocialRedContext _context;
        public LoginRepositorio(SocialRedContext con )
        {
            _context = con;
        }

        public async Task<User> AutenticarUsuario(string correo, string contraseña)
        {
            try
            {
                // Buscar el usuario por correo
                var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Correo == correo);

                // Verificar si se encontró un usuario
                if (usuario != null)
                {
                    // Verificar si la contraseña ingresada coincide con la contraseña almacenada
                    if (await VerificarContraseñaAsync(contraseña, usuario.Password))
                    {
                        if (usuario.EstadoActivacion)
                        {
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                // Si no se encontró ningún usuario o la contraseña no coincide, devolver null
                return null;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí (opcional)
                Console.WriteLine($"Error al autenticar usuario: {ex.Message}");
                return null;
            }
        }


        private async Task<bool> VerificarContraseñaAsync(string contraseña, string hashAlmacenado)
        {
            // Hashear la contraseña ingresada
            string hashIngresado = await HashearContraseñaAsync(contraseña);

            // Comparar los hashes
            return hashIngresado == hashAlmacenado;
        }

        public async Task<string> HashearContraseñaAsync(string contraseña)
        {
            // Crear un objeto de tipo SHA256 para hashear la contraseña
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Obtener el hash de la contraseña
                byte[] bytes = await Task.Run(() => sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña)));

                // Convertir el hash a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<bool> ReestablecerPassword(string username , string newPassword)
        {

            try
            {
                var usuario = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

                if (usuario != null)
                {

                    usuario.Password = await HashearContraseñaAsync(newPassword);
                    _context.Users.Update(usuario);
                    await _context.SaveChangesAsync();
                    return true;

                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex) { 
                
                Console.WriteLine($"error al restablecer la contrasena {ex.Message}");
                return false;
            }

        }

        
    }
}
