using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public bool Anunciar { get; set; }
        public string Descricao { get; set; }
        public string ImagemProduto { get; set; }

        public string UsuarioId { get; set; }
        public Usuario usuario { get; set; }

        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }
        public ICollection<Oferta> Ofertas { get; set; }

        public ProdutoOfertado produtoOfertado { get; set; }

    }
}