using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarGanha.DAL.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.NomeCompleto).IsRequired().HasMaxLength(80);
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(30);
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.Logradouro).IsRequired().HasMaxLength(80);
            builder.Property(u => u.Bairro).IsRequired().HasMaxLength(80);
            builder.Property(u => u.Numero).IsRequired();
            builder.Property(u => u.CEP).IsRequired();
            builder.Property(u => u.Complemento);
            builder.Property(u => u.Cidade).IsRequired().HasMaxLength(80);
            builder.Property(u => u.Estado).IsRequired().HasMaxLength(80);

            builder.ToTable("Usuarios");
        }
    }
}
