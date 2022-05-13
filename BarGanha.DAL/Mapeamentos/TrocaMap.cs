using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarGanha.DAL.Mapeamentos
{
    public class TrocaMap : IEntityTypeConfiguration<Troca>
    {
        public void Configure(EntityTypeBuilder<Troca> builder)
        {
            builder.HasKey(t => t.TrocaId);
            builder.Property(t => t.OfertaId).IsRequired();
            builder.Property(t => t.UserDonoId).IsRequired();
            builder.Property(t => t.UserOfertaId).IsRequired();

            builder.HasOne(t => t.Oferta).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.UserDono).WithMany(u => u.Trocas).HasForeignKey(t => t.UserDonoId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(t => t.UserOferta).WithMany(u => u.TrocasEnviadas).HasForeignKey(t => t.UserOfertaId).OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("Trocas");
        }
    }
}
