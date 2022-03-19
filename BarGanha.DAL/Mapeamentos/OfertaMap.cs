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
            builder.Property(st => st.ProdutoOfertadoId).IsRequired();
            builder.Property(st => st.Aprovado);

            builder.HasOne(st => st.Produto).WithOne().OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(st => st.ProdutoOfertado).WithMany(p => p.SolicitacoesRecebidas).HasForeignKey(st => st.ProdutoOfertadoId).OnDelete(DeleteBehavior.Restrict); ;

            builder.ToTable("Ofertas");
        }
    }
}
