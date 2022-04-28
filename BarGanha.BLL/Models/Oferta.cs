using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Oferta
    {
        public int OfertaId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int Status { get; set; }
        public ICollection<ProdutoOfertado> produtosOfertados { get; set; }

    }
}
