using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarGanha.DAL.Mapeamentos
{
    class ProdutoSelecionadoMap: IEntityTypeConfiguration<ProdutoSelecionado>
    {
        public void Configure(EntityTypeBuilder<ProdutoSelecionado> builder)
        {
            builder.HasKey(pS => pS.Id);
            builder.Property(pS => pS.ProdutoId).IsRequired();
            builder.Property(pS => pS.OfertaId).IsRequired();

            builder.HasOne(pS => pS.produto).WithMany(p => p.produtosSelecionados).HasForeignKey(pO => pO.ProdutoId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(pS => pS.oferta).WithMany(o => o.produtosSelecionados).HasForeignKey(pO => pO.OfertaId).OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("ProdutosSelecionados");
        }
    }
}
