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
            builder.HasKey(st => st.TrocaId);
            builder.Property(st => st.OfertaId).IsRequired();

            builder.HasOne(st => st.Oferta).WithOne().OnDelete(DeleteBehavior.Restrict); ;

            builder.ToTable("Trocas");
        }
    }
}
