using BarGanha.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarGanha.DAL.Mapeamentos
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(p => p.CategoriaId);
            builder.Property(p => p.NomeCategoria).IsRequired().HasMaxLength(40);

            builder.HasData(
                new Categoria
                {
                    CategoriaId = 1,
                    NomeCategoria = "Moveis",
                },

                new Categoria
                {
                    CategoriaId = 2,
                    NomeCategoria = "Eletronicos"
                }); 

            builder.ToTable("Categorias");
        }
    }
}
