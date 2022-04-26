using Livraria.Domain.Entidades;
using Livraria.Data.EFCore.Mapeamentos;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Data.EFCore
{
    public class LivrariaDBContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivroAutor> LivrosAutores { get; set; }

        public LivrariaDBContext(DbContextOptions<LivrariaDBContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new LivroAutorMap());
        }
    }
}
