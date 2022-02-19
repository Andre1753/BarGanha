using BarGanha.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "O campo Nome do Produto é obrigatório")]
        [StringLength(30, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "O campo Preço do Produto é obrigatório")]
        [Display(Name = "Preço do Produto")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo Descrição do Produto é obrigatório")]
        [StringLength(144, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem do Produto")]
        public IFormFile ImagemProduto { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}
