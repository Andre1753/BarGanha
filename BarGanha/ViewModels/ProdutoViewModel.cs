using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(144, ErrorMessage = "Use menos caracteres")]
        public string Descricao { get; set; }
        public IFormFile ImagemProduto { get; set; }
    }
}
