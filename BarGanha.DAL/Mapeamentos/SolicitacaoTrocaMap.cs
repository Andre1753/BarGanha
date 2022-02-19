using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarGanha.DAL.Mapeamentos
{
    public class SolicitacaoTrocaMap : IEntityTypeConfiguration<SolicitacaoTroca>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoTroca> builder)
        {
            builder.HasKey(st => st.TrocaId);
            builder.Property(st => st.ProdutoId).IsRequired();
            builder.Property(st => st.ProdutoOfertadoId).IsRequired();

            builder.HasOne(st => st.Produto).WithOne().OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(st => st.ProdutoOfertado).WithMany(p => p.SolicitacoesRecebidas).HasForeignKey(st => st.ProdutoOfertadoId).OnDelete(DeleteBehavior.Restrict); ;

            builder.ToTable("SolicitacoesTrocas");
        }
    }
}
