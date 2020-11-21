using System.ComponentModel.DataAnnotations;

namespace BarGanha.BLL.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; }

        [Display(Name = "Preço do Produto")]
        public decimal Preco { get; set; }

        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }
        public string ImagemProduto { get; set; }
        public int CategoriaId { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario usuario { get; set; }
        public virtual Categoria categoria { get; set; }
    }
}