using System;
using System.Collections.Generic;
using System.Text;

namespace BarGanha.BLL.Models
{
    public class ProdutoOfertado
    {
        public int Id { get; set; }
        public int OfertaId { get; set; }
        public Oferta oferta { get; set; }
        public int ProdId { get; set; }
        public Produto produto { get; set; }
    }
}
