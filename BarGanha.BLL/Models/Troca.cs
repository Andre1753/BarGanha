using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Troca
    {
        public int TrocaId { get; set; }
        public int OfertaId { get; set; }
        public bool Liberado { get; set; }
        public Oferta Oferta { get; set; }
        public string UserDonoId { get; set; }
        public Usuario UserDono { get; set; }
        public string UserOfertaId { get; set; }
        public Usuario UserOferta { get; set; }

    }
}
