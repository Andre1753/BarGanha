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
            builder.Property(o => o.UserDonoId).IsRequired();
            builder.Property(o => o.UserOfertaId).IsRequired();

            builder.Property(o => o.Status);

            builder.HasOne(o => o.UserDono).WithMany(u => u.Ofertas).HasForeignKey(o => o.UserDonoId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.UserOferta).WithMany(u => u.OfertasEnviadas).HasForeignKey(o => o.UserOfertaId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Ofertas");
        }
    }
}
