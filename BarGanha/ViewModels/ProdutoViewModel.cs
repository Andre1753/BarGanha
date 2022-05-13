using BarGanha.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class ProdutoViewModel
    {
        public int? ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Produto é obrigatório")]
        [StringLength(30, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Nome")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "Preço estimado do produto é obrigatório")]
        [Display(Name = "Preço estimado")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Descrição do Produto é obrigatório")]
        [StringLength(144, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A imagem do Produto é obrigatório")]
        [Display(Name = "Imagem")]
        public IFormFile ImagemProduto { get; set; }


        [Required(ErrorMessage = "Selecione uma categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Estado { get; set; }
    }
}
