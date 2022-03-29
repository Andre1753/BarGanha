using BarGanha.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class OfertaViewModel
    {
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        [Display(Name = "Produtos Ofertados")]
        public int ProdutoOfertadoId { get; set; }

        public Produto produto { get; set; }
    }
}
