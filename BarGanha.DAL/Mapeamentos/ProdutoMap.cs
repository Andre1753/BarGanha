using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarGanha.DAL.Mapeamentos
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.ProdutoId);
            builder.Property(p => p.NomeProduto).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Preco).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Anunciar).IsRequired();
            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(144);
            builder.Property(p => p.ImagemProduto).IsRequired();
            builder.Property(p => p.CategoriaId).IsRequired();
            builder.Property(p => p.UsuarioId).IsRequired();

            builder.HasOne(p => p.usuario).WithMany(p => p.Produtos).HasForeignKey(p => p.UsuarioId);
            builder.HasOne(p => p.categoria).WithMany(p => p.Produtos).HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Produtos");
        }
    }
}
