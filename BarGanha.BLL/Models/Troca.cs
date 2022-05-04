using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Troca
    {
        public int TrocaId { get; set; }
        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
