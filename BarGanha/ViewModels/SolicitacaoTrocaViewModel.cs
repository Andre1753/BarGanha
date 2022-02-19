using BarGanha.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class SolicitacaoTrocaViewModel
    {
        [Display(Name = "Produto")]
        public string ProdutoId { get; set; }

        [Display(Name = "Produtos Ofertados")]
        public decimal ProdutoOfertadoId { get; set; }
    }
}
