using System;
using System.Collections.Generic;
using System.Text;

namespace BarGanha.BLL.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
        public ICollection<Produto> Produtos { get; set; }

    }
}
