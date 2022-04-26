using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapeamentos
{
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autores");

            builder.HasIndex(t => t.CPF).IsUnique().HasDatabaseName("Indice_Autores");

            builder.HasKey(k => k.Codigo);

            builder.Property(c => c.Codigo).HasColumnName("aut_codigo");
            builder.Property(c => c.CPF).HasColumnName("aut_cpf").HasMaxLength(15).IsRequired();
            builder.Property(n => n.Nome).HasColumnName("aut_nome").HasMaxLength(100).IsRequired();
            builder.Property(d => d.DataNascimento).HasColumnName("aut_dtnascimento");
        }
    }
}
    
