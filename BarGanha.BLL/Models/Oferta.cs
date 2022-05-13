using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Oferta
    {
        public int OfertaId { get; set; }
        public string UserOfertaId { get; set; }
        public Usuario UserOferta { get; set; }
        public string UserDonoId { get; set; }
        public Usuario UserDono { get; set; }
        public int Status { get; set; }
        public ICollection<ProdutoOfertado> produtosOfertados { get; set; }
        public ICollection<ProdutoSelecionado> produtosSelecionados { get; set; }

    }
}
