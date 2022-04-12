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
            builder.HasKey(st => st.OfertaId);
            builder.Property(st => st.ProdutoId).IsRequired();
            builder.Property(st => st.Status);


            builder.HasOne(st => st.Produto).WithMany(p => p.Ofertas).HasForeignKey(st => st.ProdutoId);

            builder.ToTable("Ofertas");
        }
    }
}
