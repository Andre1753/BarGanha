using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarGanha.DAL.Mapeamentos
{
    public class OfertaMap : IEntityTypeConfiguration<Oferta>
    {
        public void Configure(EntityTypeBuilder<Oferta> builder)
        {
            builder.HasKey(o => o.OfertaId);
            builder.Property(o => o.ProdutoId).IsRequired();
            builder.Property(o => o.UsuarioId).IsRequired();

            builder.Property(o => o.Status);

            builder.HasOne(o => o.Usuario).WithMany(u => u.Ofertas).HasForeignKey(o => o.UsuarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Produto).WithMany(p => p.Ofertas).HasForeignKey(o => o.ProdutoId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Ofertas");
        }
    }
}
