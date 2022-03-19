using System.ComponentModel.DataAnnotations;

namespace BarGanha.BLL.Models
{
    public class Troca
    {
        public int TrocaId { get; set; }
        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }
    }
}
