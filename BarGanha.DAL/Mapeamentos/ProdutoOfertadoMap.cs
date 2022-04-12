using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarGanha.DAL.Mapeamentos
{
    public class ProdutoOfertadoMap : IEntityTypeConfiguration<ProdutoOfertado>
    {
        public void Configure(EntityTypeBuilder<ProdutoOfertado> builder)
        {
            builder.HasKey(pO => pO.Id);
            builder.Property(pO => pO.ProdId).IsRequired();
            builder.Property(pO => pO.OfertaId).IsRequired();

            builder.HasOne(pO => pO.produto).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pO => pO.oferta).WithMany(o => o.produtosOfertados).HasForeignKey(pO => pO.OfertaId);

            builder.ToTable("ProdutosOfertados");
        }
    }
}
