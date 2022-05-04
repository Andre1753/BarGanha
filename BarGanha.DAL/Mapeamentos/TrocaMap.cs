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
            builder.Property(t => t.UsuarioId).IsRequired();

            builder.HasOne(t => t.Oferta).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Usuario).WithMany(u => u.Trocas).HasForeignKey(t => t.UsuarioId);
            
            builder.ToTable("Trocas");
        }
    }
}
