using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Usuario : IdentityUser<string>
    {
        public string CPF { get; set; }
        public string NomeCompleto { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Oferta> Ofertas { get; set; }
    }

}
