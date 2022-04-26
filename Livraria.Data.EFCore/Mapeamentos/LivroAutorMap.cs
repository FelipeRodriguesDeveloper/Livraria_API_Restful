using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapeamentos
{
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable("Livros_autores");

            builder.HasKey(k => new { k.CodigoLivro, k.CodigoAutor });

            builder.Property(k => k.CodigoLivro).HasColumnName("liv_codigo");
            builder.Property(k => k.CodigoAutor).HasColumnName("aut_codigo");

            //Relacionamento MUITOS para MUITOS:
            builder.HasOne(l => l.Livro).WithMany(la => la.LivrosAutores)
                   .HasForeignKey(fk => fk.CodigoLivro);

            builder.HasOne(a => a.Autor).WithMany(la => la.LivrosAutores)
                   .HasForeignKey(fk => fk.CodigoAutor);
        }
    }
}
