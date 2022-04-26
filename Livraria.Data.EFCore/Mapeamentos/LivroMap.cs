using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapeamentos
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {  
            builder.ToTable("Livros");

            builder.HasIndex(t => t.Titulo).IsUnique().HasDatabaseName("Indice_Livros");

            builder.HasKey(k => k.Codigo);

            builder.Property(c => c.Codigo).HasColumnName("liv_codigo");
            builder.Property(t => t.Titulo).HasColumnName("liv_titulo").HasMaxLength(150).IsRequired();
            builder.Property(d => d.DataCadastro).HasColumnName("liv_datacadastro");
            builder.Property(p => p.Preco).HasColumnName("liv_preco").HasColumnType("decimal(5,2)");
        }
    }
}
