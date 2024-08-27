using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infraestructure.Context
{
    public class SocialRedContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Amistades> Amistades { get; set; }

        public SocialRedContext(DbContextOptions<SocialRedContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ConfiguracionUser

            // Configuración de la entidad User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasMaxLength(50);
                entity.Property(e => e.Apellido).HasMaxLength(50);
                entity.Property(e => e.Telefono).HasMaxLength(15);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.EstadoActivacion);
            });

            #endregion


            #region ConfiguracionPublicaciones

            // Configuración de la entidad Publicaciones
            modelBuilder.Entity<Publicaciones>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Contenido).HasMaxLength(500);
                entity.Property(e => e.Imagen).HasMaxLength(255);
                entity.Property(e => e.EnlaceVideo).HasMaxLength(255);
                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Publicaciones)
                    .HasForeignKey(e => e.UsuarioID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion


            #region ConfiguracionComentarios
            // Configuración de la entidad Comentarios
            modelBuilder.Entity<Comentarios>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Contenido).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.Publicacion)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(e => e.PublicacionID)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Comentarios)
                    .HasForeignKey(e => e.UsuarioID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region ConfiguracionAmistades

            // Configuración de la entidad Amistades
            modelBuilder.Entity<Amistades>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Usuario1)
                    .WithMany(u => u.Amistades)
                    .HasForeignKey(e => e.UsuarioID1)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Usuario2)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioID2)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

        }
    }
}
